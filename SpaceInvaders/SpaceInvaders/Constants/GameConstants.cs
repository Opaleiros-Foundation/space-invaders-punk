namespace SpaceInvaders.Constants
{
    public static class GameConstants
    {
        // Alien Wave Generation
        public const int WaveStartX = 100;
        public const int WaveStartY = 50;
        public const int AlienXOffset = 40;
        public const int AlienYOffset = 40;
        public const int AliensPerRow = 12;
        public const int AlienWidth = 64;

        // Alien Movement
        public const double InitialAlienSpeed = 2.0;
        public const double AlienSpeedIncrement = 0.25;
        public const double AlienVerticalStepDivisor = 90.0;

        // Player
        public const double PlayerSpeed = 4.0;
        public const double PlayerWidth = 64;

        // Game Area
        public const int ScreenMargin = 50;

        // Game Constants (from GameStartPage.xaml.cs)

        // Shield
        public const int ShieldWidth = 32;
        public const int ShieldHeight = 32;
        public const int NumberOfShields = 4;
        public const int ShieldHealth = 100;
        public const int ShieldSpacingDivisor = 5;
        public const int ShieldYOffsetFromBottom = 200;

        // Projectile
        public const int ProjectileImageWidth = 16;
        public const int ProjectileImageHeight = 16;
        public const int ProjectileInitialYOffset = 30;

        // Player Image
        public const int PlayerImageWidth = 32;
        public const int PlayerImageHeight = 32;
        public const int PlayerYOffsetFromBottom = 50;

        // Alien Image
        public const int AlienImageWidth = 32;
        public const int AlienImageHeight = 32;

        // Game Timer
        public const int GameTimerIntervalMs = 45;

        // Sound
        public const int EnemyDeathSoundCooldownMs = 50;

        // General UI
        public const int CenteringDivisor = 2;
        public const int ScreenBoundaryMin = 0;

        // Game Constants (from GameStartPageViewModel.cs)

        // Game Initialization
        public const double InitialGameWidth = 800;
        public const double InitialGameHeight = 600;
        public const int GameLoopIntervalMs = 16;

        // Player Score/Lives
        public const int ExtraLifeThreshold = 1000;
        public const int MaxPlayerLives = 6;

        // Sound
        public const int PlayerShootCooldownMs = 100;
    }
}