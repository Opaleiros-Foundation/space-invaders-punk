using System.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SpaceInvaders.Interfaces.Services;

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
    private readonly List<Process> _activeProcesses = new();

    /// <summary>
    /// Gets or sets the global volume for sounds played by this service.
    /// </summary>
    public float Volume { get; set; } = 1.0f; // Default volume to 1.0 (max)

    /// <summary>
    /// Plays a sound from the specified path.
    /// </summary>
    /// <param name="soundPath">The path to the sound file (e.g., "ms-appx:///Assets/sounds/my_sound.mp3").</param>
    public void PlaySound(string soundPath)
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
                        lock (_activeProcesses)
                        {
                            _activeProcesses.Remove(p);
                        }
                        p.Dispose();
                    }
                };

                lock (_activeProcesses)
                {
                    _activeProcesses.Add(process);
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
        lock (_activeProcesses)
        {
            foreach (var process in _activeProcesses)
            {
                try
                {
                    if (!process.HasExited)
                    {
                        process.Kill(); // Terminate the process
                        process.WaitForExit(1000); // Wait for it to exit gracefully (1 second timeout)
                    }
                    process.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[SoundService] Error stopping process: {ex.Message}");
                }
            }
            _activeProcesses.Clear(); // Clear the list after stopping all processes
        }
    }
}
