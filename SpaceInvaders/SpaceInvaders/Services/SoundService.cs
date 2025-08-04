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
                var process = new Process
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
                process.Start();
                // dont wait for the process to finish
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
}
