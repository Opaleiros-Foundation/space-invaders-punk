using System;
using System.Collections.Generic;
using System.Linq;
using SpaceInvaders.Interfaces.Factories;
using SpaceInvaders.Models;

namespace SpaceInvaders.Factories.Strategies
{
    /// <summary>
    /// Implements <see cref="IWaveFormationStrategy"/> to create alien waves with a standard, predefined formation.
    /// The number of rows increases with the game level.
    /// </summary>
    public class StandardFormationStrategy : IWaveFormationStrategy
    {
        /// <summary>
        /// Creates a new wave of aliens based on a standard formation pattern.
        /// The number of rows is determined by the current game level, capped at 6 rows.
        /// </summary>
        /// <param name="level">The current game level.</param>
        /// <returns>A list of <see cref="AlienType"/> representing the alien wave in a standard formation.</returns>
        public List<AlienType> CreateWave(int level)
        {
            // Level 1 starts with 3 rows.
            // Every 2 levels (at 3, 5, 7...) add a row.
            int totalRows = 3 + ((level - 1) / 2);
            
            // Cap at 6 rows.
            totalRows = Math.Min(totalRows, 6);

            // Defines the base pattern of rows to be added
            var rowPattern = new List<AlienType>
            {
                AlienType.Type3, // Row 0
                AlienType.Type2, // Row 1
                AlienType.Type1, // Row 2
                AlienType.Type2, // Row 3 (added at level 3)
                AlienType.Type1, // Row 4 (added at level 5)
                AlienType.Type1  // Row 5 (added at level 7)
            };

            // Return the sub-list corresponding to the current level's number of rows
            return rowPattern.Take(totalRows).ToList();
        }
    }
}
