using System;
using System.Collections.Generic;
using System.Linq;
using SpaceInvaders.Interfaces.Factories;
using SpaceInvaders.Models;

namespace SpaceInvaders.Factories.Strategies
{
    /// <summary>
    /// Implements <see cref="IWaveFormationStrategy"/> to create alien waves with a random formation.
    /// </summary>
    public class RandomFormationStrategy : IWaveFormationStrategy
    {
        private readonly Random _random = new Random();

        /// <summary>
        /// Creates a new wave of aliens with a random number of rows (3 to 6) and random alien types.
        /// Excludes <see cref="AlienType.Type4"/> from random generation.
        /// </summary>
        /// <param name="level">The current game level (used for context, but doesn't directly influence randomness here).</param>
        /// <returns>A shuffled list of <see cref="AlienType"/> representing the alien wave.</returns>
        public List<AlienType> CreateWave(int level)
        {
            int totalRows = _random.Next(3, 7); // 3 to 6 rows

            // Get all alien types, but exclude Type4 as requested.
            var availableAlienTypes = Enum.GetValues(typeof(AlienType))
                .Cast<AlienType>()
                .Where(t => t != AlienType.Type4)
                .ToList();

            var wave = new List<AlienType>();

            // Ensure at least one of each type
            wave.AddRange(availableAlienTypes);

            int remainingRows = totalRows - availableAlienTypes.Count;

            for (var i = 0; i < remainingRows; i++)
            {
                int randomIndex = _random.Next(availableAlienTypes.Count);
                wave.Add(availableAlienTypes[randomIndex]);
            }

            // Shuffle the wave to randomize the order of rows
            return wave.OrderBy(x => _random.Next()).ToList();
        }
    }
}
