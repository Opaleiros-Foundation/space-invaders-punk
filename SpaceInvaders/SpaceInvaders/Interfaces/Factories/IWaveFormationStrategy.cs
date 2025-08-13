using System.Collections.Generic;
using SpaceInvaders.Models;

namespace SpaceInvaders.Interfaces.Factories
{
    /// <summary>
    /// Defines the strategy for alien wave formation.
    /// </summary>
    public interface IWaveFormationStrategy
    {
        /// <summary>
        /// Creates a new wave of aliens based on the current game level.
        /// </summary>
        /// <param name="level">The current game level.</param>
        /// <returns>A list of alien types that compose the wave.</returns>
        List<AlienType> CreateWave(int level);
    }
}
