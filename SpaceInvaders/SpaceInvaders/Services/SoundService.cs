using System;
using System.Collections.Generic;
using Windows.Media.Core;
using Windows.Media.Playback;
using SpaceInvaders.Interfaces.Services;
using Windows.ApplicationModel; // Add this using statement

namespace SpaceInvaders.Services;

public class SoundService : ISoundService
{
    private readonly List<MediaPlayer> _mediaPlayers = new();

    public void PlaySound(string soundPath)
    {
        Console.WriteLine($"[SoundService] PlaySound method called for: {soundPath}");
        try
        {
            var mediaPlayer = new MediaPlayer();
            mediaPlayer.Volume = 1.0; // Ensure volume is not zero
            Console.WriteLine("[SoundService] New MediaPlayer instance created.");

            Uri uri;
            if (soundPath.StartsWith("ms-appx:///"))
            {
                // Resolve ms-appx:/// URI to a local file path
                var relativePath = soundPath.Replace("ms-appx:///", "");
                var fullPath = System.IO.Path.Combine(Package.Current.InstalledLocation.Path, relativePath);
                uri = new Uri(fullPath);
                Console.WriteLine($"[SoundService] Resolved ms-appx URI to: {fullPath}");
            }
            else
            {
                uri = new Uri(soundPath);
            }
            
            mediaPlayer.Source = MediaSource.CreateFromUri(uri);

            mediaPlayer.MediaOpened += (sender, args) =>
            {
                Console.WriteLine("[SoundService] MediaOpened event fired.");
            };
            mediaPlayer.MediaEnded += (sender, args) =>
            {
                Console.WriteLine("[SoundService] MediaEnded event fired. Removing MediaPlayer.");
                _mediaPlayers.Remove(mediaPlayer);
            };
            mediaPlayer.MediaFailed += (sender, args) =>
            {
                Console.WriteLine($"[SoundService] Media failed: {args.ErrorMessage}");
                _mediaPlayers.Remove(mediaPlayer);
            };

            _mediaPlayers.Add(mediaPlayer);
            Console.WriteLine($"[SoundService] MediaPlayer CurrentState before Play(): {mediaPlayer.CurrentState}");
            mediaPlayer.Play();
            Console.WriteLine($"[SoundService] MediaPlayer CurrentState after Play(): {mediaPlayer.CurrentState}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SoundService] Error playing sound: {ex.Message}");
        }
    }
}
