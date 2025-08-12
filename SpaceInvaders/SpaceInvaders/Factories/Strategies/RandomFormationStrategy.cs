using System;
using System.Collections.Generic;
using System.Linq;
using SpaceInvaders.Interfaces.Factories;
using SpaceInvaders.Models;

namespace SpaceInvaders.Factories.Strategies
{
    public class RandomFormationStrategy : IWaveFormationStrategy
    {
        private readonly Random _random = new Random();

        public List<AlienType> CreateWave(int level)
        {
            int totalRows = _random.Next(3, 7); // 3 to 6 rows

            // Get all alien types, but exclude Type4 as requested.
            var availableAlienTypes = Enum.GetValues(typeof(AlienType))
                                          .Cast<AlienType>()
                                          .Where(t => t != AlienType.Type4)
                                          .ToList();

            var wave = new List<AlienType>();

            for (int i = 0; i < totalRows; i++)
            {
                int randomIndex = _random.Next(availableAlienTypes.Count);
                wave.Add(availableAlienTypes[randomIndex]);
            }

            return wave;
        }
    }
}
