using System.Collections.Generic;
using SpaceInvaders.Factories.Strategies;
using SpaceInvaders.Interfaces.Factories;
using SpaceInvaders.Models;

namespace SpaceInvaders.Factories
{
    public static class WaveFactory
    {
        private static readonly IWaveFormationStrategy StandardStrategy = new StandardFormationStrategy();
        private static readonly IWaveFormationStrategy RandomStrategy = new RandomFormationStrategy();

        public static List<AlienType> GenerateWave(int level)
        {
            // Use the standard formation for level 0 or any odd-numbered level.
            // Use the random formation for any even-numbered level greater than 0.
            if (level == 0 || level % 2 != 0)
            {
                return StandardStrategy.CreateWave(level);
            }
            else
            {
                return RandomStrategy.CreateWave(level);
            }
        }
    }
}
