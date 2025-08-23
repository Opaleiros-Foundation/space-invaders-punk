using System.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SpaceInvaders.Interfaces.Services;
using SpaceInvaders.Constants;

#if !__MACCATALYST__ && !WINDOWS && !ANDROID && !IOS
using NAudio.Wave;
#endif

namespace SpaceInvaders.Services;

/// <summary>
/// Provides services for playing sounds within the application.
/// </summary>
public class SoundService : ISoundService
{
#if !__MACCATALYST__ && !WINDOWS && !ANDROID && !IOS
    private readonly List<IWavePlayer> _activePlayers = new();
#endif
    private class ActiveSound
    {
        public Process Process { get; }
        public SoundPriority Priority { get; }
        public DateTime StartTime { get; }

        public ActiveSound(Process process, SoundPriority priority)
        {
            Process = process;
            Priority = priority;
            StartTime = DateTime.Now;
        }
    }

    private readonly List<ActiveSound> _activeSounds = new();

    /// <summary>
    /// Gets or sets the global volume for sounds played by this service.
    /// </summary>
    public float Volume { get; set; } = 1.0f; // Default volume to 1.0 (max)

    private const int MAX_CONCURRENT_SOUNDS = 5; // Example limit

    /// <summary>
    /// Plays a sound from the specified path.
    /// </summary>
    /// <param name="soundPath">The path to the sound file (e.g., "ms-appx:///Assets/sounds/my_sound.mp3").</param>
    /// <param name="priority">The priority of the sound.</param>
    public void PlaySound(string soundPath, SoundPriority priority = SoundPriority.Medium)
    {
        try
        {
            var localPath = soundPath.Replace("ms-appx:///", "");
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, localPath);

            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"[SoundService] File not found: {fullPath}");
                return;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "ffplay",
                        Arguments = $"-nodisp -autoexit -loglevel quiet \"{fullPath}\"", // Play audio, no display, auto-exit, quiet logging
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    },
                    EnableRaisingEvents = true
                };

                process.OutputDataReceived += (sender, data) =>
                {
                    if (!string.IsNullOrWhiteSpace(data.Data))
                    {
                        Console.WriteLine($"[SoundService][ffplay-out] {data.Data}");
                    }
                };

                process.ErrorDataReceived += (sender, data) =>
                {
                    if (!string.IsNullOrWhiteSpace(data.Data))
                    {
                        Console.WriteLine($"[SoundService][ffplay-err] {data.Data}");
                    }
                };

                process.Exited += (sender, e) =>
                {
                    if (sender is Process p)
                    {
                        lock (_activeSounds)
                        {
                            var activeSoundToRemove = _activeSounds.FirstOrDefault(s => s.Process == p);
                            if (activeSoundToRemove != null)
                            {
                                _activeSounds.Remove(activeSoundToRemove);
                            }
                        }
                        p.Dispose();
                    }
                };

                lock (_activeSounds)
                {
                    // Remove any exited processes from the list
                    _activeSounds.RemoveAll(s => s.Process.HasExited);

                    // If max concurrent sounds reached, stop the lowest priority/oldest sound
                    if (_activeSounds.Count >= MAX_CONCURRENT_SOUNDS)
                    {
                        var lowestPrioritySound = _activeSounds
                            .OrderBy(s => s.Priority)
                            .ThenBy(s => s.StartTime)
                            .FirstOrDefault();

                        if (lowestPrioritySound != null && priority > lowestPrioritySound.Priority)
                        {
                            // Stop the lowest priority sound to make room for the new, higher priority sound
                            try
                            {
                                if (!lowestPrioritySound.Process.HasExited)
                                {
                                    lowestPrioritySound.Process.Kill();
                                    lowestPrioritySound.Process.WaitForExit(1000);
                                }
                                lowestPrioritySound.Process.Dispose();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[SoundService] Error stopping lowest priority sound: {ex.Message}");
                            }
                            _activeSounds.Remove(lowestPrioritySound);
                        }
                        else if (lowestPrioritySound != null && priority <= lowestPrioritySound.Priority)
                        {
                            // If new sound has lower or equal priority, don't play it
                            Console.WriteLine($"[SoundService] Not playing sound: {fullPath} due to priority/limit.");
                            return;
                        }
                    }

                    var activeSound = new ActiveSound(process, priority);
                    _activeSounds.Add(activeSound);
                }
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
#if !__MACCATALYST__ && !WINDOWS && !ANDROID && !IOS
            else
            {
                Console.WriteLine($"[SoundService] Playing sound using NAudio: {fullPath}");
                var audioFile = new AudioFileReader(fullPath)
                {
                    Volume = Volume
                };
                var outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);

                outputDevice.PlaybackStopped += (sender, args) =>
                {
                    outputDevice.Dispose();
                    audioFile.Dispose();
                    _activePlayers.Remove(outputDevice);
                    Console.WriteLine("[SoundService] Playback stopped and resources disposed.");
                };

                _activePlayers.Add(outputDevice);
                outputDevice.Play();
            }
#endif
            Console.WriteLine($"[SoundService] Playing sound: {fullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SoundService] Error playing sound: {ex.Message}");
        }
    }

    /// <summary>
    /// Stops all currently active sound playback processes.
    /// </summary>
    public void StopAllSounds()
    {
        lock (_activeSounds)
        {
            foreach (var activeSound in _activeSounds)
            {
                try
                {
                    if (!activeSound.Process.HasExited)
                    {
                        activeSound.Process.Kill(); // Terminate the process
                        activeSound.Process.WaitForExit(1000); // Wait for it to exit gracefully (1 second timeout)
                    }
                    activeSound.Process.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[SoundService] Error stopping process: {ex.Message}");
                }
            }
            _activeSounds.Clear(); // Clear the list after stopping all processes
        }
    }
}
