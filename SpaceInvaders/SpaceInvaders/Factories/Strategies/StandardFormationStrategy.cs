using System;
using System.Collections.Generic;
using System.Linq;
using SpaceInvaders.Interfaces.Factories;
using SpaceInvaders.Models;

namespace SpaceInvaders.Factories.Strategies
{
    public class StandardFormationStrategy : IWaveFormationStrategy
    {
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
