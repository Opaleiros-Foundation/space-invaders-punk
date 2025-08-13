using System.Collections.Generic;
using SpaceInvaders.Factories.Strategies;
using SpaceInvaders.Interfaces.Factories;
using SpaceInvaders.Models;

namespace SpaceInvaders.Factories
{
    /// <summary>
    /// A static factory class responsible for generating alien waves based on the game level.
    /// It uses different wave formation strategies (standard or random) depending on the level.
    /// </summary>
    public static class WaveFactory
    {
        private static readonly IWaveFormationStrategy StandardStrategy = new StandardFormationStrategy();
        private static readonly IWaveFormationStrategy RandomStrategy = new RandomFormationStrategy();

        /// <summary>
        /// Generates a list of alien types for a new wave based on the current game level.
        /// Uses a standard formation for level 0 or odd-numbered levels, and a random formation for even-numbered levels greater than 0.
        /// </summary>
        /// <param name="level">The current game level.</param>
        /// <returns>A list of <see cref="AlienType"/> representing the alien wave.</returns>
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
