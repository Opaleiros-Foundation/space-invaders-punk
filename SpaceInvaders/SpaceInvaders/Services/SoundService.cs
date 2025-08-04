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

public class SoundService : ISoundService
{
#if !__MACCATALYST__ && !WINDOWS && !ANDROID && !IOS
    private readonly List<IWavePlayer> _activePlayers = new();
#endif

    private Process _mpg123Process; // To keep track of the mpg123 process

    public float Volume { get; set; } = 1.0f; // Default volume to 1.0 (max)

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
                // Stop any existing mpg123 process
                if (_mpg123Process != null && !_mpg123Process.HasExited)
                {
                    _mpg123Process.Kill();
                    _mpg123Process.Dispose();
                }

                _mpg123Process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "mpg123",
                        Arguments = $"-f 16384 \"{fullPath}\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    }
                };
                _mpg123Process.Start();
                // dont wait for the process to finish
            }
#if !__MACCATALYST__ && !WINDOWS && !ANDROID && !IOS
            else
            {
                // Stop all currently playing sounds to prevent overlay
                foreach (var player in _activePlayers.ToArray())
                {
                    player.Stop();
                    player.Dispose();
                }
                _activePlayers.Clear();

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
}
