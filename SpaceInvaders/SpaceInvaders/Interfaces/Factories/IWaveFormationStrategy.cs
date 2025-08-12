using System.Collections.Generic;
using SpaceInvaders.Models;

namespace SpaceInvaders.Interfaces.Factories
{
    public interface IWaveFormationStrategy
    {
        List<AlienType> CreateWave(int level);
    }
}
