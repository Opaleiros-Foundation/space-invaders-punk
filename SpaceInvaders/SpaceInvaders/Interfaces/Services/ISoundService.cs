namespace SpaceInvaders.Interfaces.Services;
using SpaceInvaders.Constants;

/// <summary>
/// Defines the methods for the sound playback service.
/// </summary>
public interface ISoundService
{
    /// <summary>
    /// Plays a sound from the specified path.
    /// </summary>
    /// <param name="soundPath">The path to the sound file to be played.</param>
    /// <param name="priority">The priority of the sound.</param>
    void PlaySound(string soundPath, SoundPriority priority = SoundPriority.Medium);

    
}
