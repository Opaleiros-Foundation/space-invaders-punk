using NAudio.Wave;
using System.IO;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using SpaceInvaders.Interfaces.Services;

namespace SpaceInvaders.Services;

public class SoundService : ISoundService
{
    private readonly List<IWavePlayer> _activePlayers = new();

    public void PlaySound(string soundPath)
    {
        try
        {
            // Converter ms-appx para caminho local
            var localPath = soundPath.Replace("ms-appx:///", "");
            var fullPath = Path.Combine(Package.Current.InstalledLocation.Path, localPath);
            
            // Verificar se o arquivo existe
            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"[SoundService] File not found: {fullPath}");
                return;
            }

            var audioFile = new AudioFileReader(fullPath);
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
            Console.WriteLine($"[SoundService] Playing sound: {fullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SoundService] Error playing sound: {ex.Message}");
        }
    }
}
