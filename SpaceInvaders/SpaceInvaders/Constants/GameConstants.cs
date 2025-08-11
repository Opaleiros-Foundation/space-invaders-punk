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
    }
}