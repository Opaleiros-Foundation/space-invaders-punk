namespace SpaceInvaders.Constants;

/// <summary>
/// Defines constant paths to various sound files used in the game.
/// </summary>
public static class SoundPaths
{
    /// <summary>
    /// A list of paths to player shooting sound effects.
    /// </summary>
    public static readonly List<string> PlayerShoot = new()
    {
        "ms-appx:///Assets/sounds/player/shoot/sound1.mp3",
        // "ms-appx:///Assets/sounds/player/shoot/sound2.mp3",
        // "ms-appx:///Assets/sounds/player/shoot/sound3.mp3",
        // "ms-appx:///Assets/sounds/player/shoot/sound4.mp3",
        // "ms-appx:///Assets/sounds/player/shoot/sound5.mp3",
        // "ms-appx:///Assets/sounds/player/shoot/sound6.mp3",
        // "ms-appx:///Assets/sounds/player/shoot/sound7.mp3"
    };

    /// <summary>
    /// A list of paths to enemy death sound effects.
    /// </summary>
    public static readonly List<string> EnemyDie = new()
    {
        "ms-appx:///Assets/sounds/enemies/die/sound1.mp3",
        // "ms-appx:///Assets/sounds/enemies/die/sound2.mp3",
        // "ms-appx:///Assets/sounds/enemies/die/sound3.mp3",
        // "ms-appx:///Assets/sounds/enemies/die/sound4.mp3",
        // "ms-appx:///Assets/sounds/enemies/die/sound5.mp3",
        // "ms-appx:///Assets/sounds/enemies/die/sound6.mp3",
        // "ms-appx:///Assets/sounds/enemies/die/sound7.mp3",
        // "ms-appx:///Assets/sounds/enemies/die/sound8.mp3"
    };

    /// <summary>
    /// The path to the extra life sound effect.
    /// </summary>
    public const string ExtraLife = "ms-appx:///Assets/sounds/musics/extra_life.mp3";

    /// <summary>
    /// The path to the explosion sound effect.
    /// </summary>
    public const string Explosion = "ms-appx:///Assets/sounds/enemies/die/sound1.mp3";
}
