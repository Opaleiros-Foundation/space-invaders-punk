namespace SpaceInvaders.Constants
{
    /// <summary>
    /// Defines various constant values used throughout the game, including dimensions, speeds, and game mechanics.
    /// </summary>
    public static class GameConstants
    {
        // Alien Wave Generation
        /// <summary>
        /// The starting X-coordinate for alien wave generation.
        /// </summary>
        public const int WaveStartX = 100;
        /// <summary>
        /// The starting Y-coordinate for alien wave generation.
        /// </summary>
        public const int WaveStartY = 50;
        /// <summary>
        /// The horizontal offset between aliens in a wave.
        /// </summary>
        public const int AlienXOffset = 40;
        /// <summary>
        /// The vertical offset between alien rows in a wave.
        /// </summary>
        public const int AlienYOffset = 40;
        /// <summary>
        /// The maximum number of aliens in a single row.
        /// </summary>
        public const int AliensPerRow = 12;
        /// <summary>
        /// The default width of an alien.
        /// </summary>
        public const int AlienWidth = 64;

        // Alien Movement
        /// <summary>
        /// The initial speed of aliens.
        /// </summary>
        public const double InitialAlienSpeed = 2.0;
        /// <summary>
        /// The increment in alien speed after certain game events.
        /// </summary>
        public const double AlienSpeedIncrement = 0.25;
        /// <summary>
        /// Divisor used to calculate the vertical step of aliens.
        /// </summary>
        public const double AlienVerticalStepDivisor = 90.0;

        // Player
        /// <summary>
        /// The movement speed of the player.
        /// </summary>
        public const double PlayerSpeed = 4.0;
        /// <summary>
        /// The default width of the player character.
        /// </summary>
        public const double PlayerWidth = 64;

        // Game Area
        /// <summary>
        /// The margin from the screen edges for game elements.
        /// </summary>
        public const int ScreenMargin = 50;

        // Shield
        /// <summary>
        /// The default width of a shield segment.
        /// </summary>
        public const int ShieldWidth = 32;
        /// <summary>
        /// The default height of a shield segment.
        /// </summary>
        public const int ShieldHeight = 32;
        /// <summary>
        /// The total number of shields present in the game.
        /// </summary>
        public const int NumberOfShields = 4;
        /// <summary>
        /// The initial health of each shield segment.
        /// </summary>
        public const int ShieldHealth = 100;
        /// <summary>
        /// Divisor used to calculate the spacing between shields.
        /// </summary>
        public const int ShieldSpacingDivisor = 5;
        /// <summary>
        /// The vertical offset from the bottom of the screen for shield placement.
        /// </summary>
        public const int ShieldYOffsetFromBottom = 200;

        // Projectile
        /// <summary>
        /// The default width of a projectile image.
        /// </summary>
        public const int ProjectileImageWidth = 16;
        /// <summary>
        /// The default height of a projectile image.
        /// </summary>
        public const int ProjectileImageHeight = 16;
        /// <summary>
        /// The initial vertical offset for a newly spawned projectile from its origin.
        /// </summary>
        public const int ProjectileInitialYOffset = 30;

        // Player Image
        /// <summary>
        /// The default width of the player image.
        /// </summary>
        public const int PlayerImageWidth = 32;
        /// <summary>
        /// The default height of the player image.
        /// </summary>
        public const int PlayerImageHeight = 32;
        /// <summary>
        /// The vertical offset from the bottom of the screen for player placement.
        /// </summary>
        public const int PlayerYOffsetFromBottom = 50;

        // Alien Image
        /// <summary>
        /// The default width of an alien image.
        /// </summary>
        public const int AlienImageWidth = 32;
        /// <summary>
        /// The default height of an alien image.
        /// </summary>
        public const int AlienImageHeight = 32;

        // Game Timer
        /// <summary>
        /// The interval in milliseconds for the game timer.
        /// </summary>
        public const int GameTimerIntervalMs = 45;

        // Sound
        /// <summary>
        /// The cooldown period in milliseconds before the enemy death sound can be played again.
        /// </summary>
        public const int EnemyDeathSoundCooldownMs = 50;

        // General UI
        /// <summary>
        /// Divisor used for centering UI elements.
        /// </summary>
        public const int CenteringDivisor = 2;
        /// <summary>
        /// The minimum boundary for screen coordinates.
        /// </summary>
        public const int ScreenBoundaryMin = 0;

        // Game Initialization
        /// <summary>
        /// The initial width of the game window.
        /// </summary>
        public const double InitialGameWidth = 800;
        /// <summary>
        /// The initial height of the game window.
        /// </summary>
        public const double InitialGameHeight = 600;
        /// <summary>
        /// The interval in milliseconds for the main game loop.
        /// </summary>
        public const int GameLoopIntervalMs = 16;

        // Player Score/Lives
        /// <summary>
        /// The score threshold at which the player earns an extra life.
        /// </summary>
        public const int ExtraLifeThreshold = 1000;
        /// <summary>
        /// The maximum number of lives a player can have.
        /// </summary>
        public const int MaxPlayerLives = 6;

        // Sound
        /// <summary>
        /// The cooldown period in milliseconds before the player can shoot again.
        /// </summary>
        public const int PlayerShootCooldownMs = 100;
    }
}