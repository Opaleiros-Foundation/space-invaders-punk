
using SpaceInvaders.Models;
using SpaceInvaders.Models.Aliens;

namespace SpaceInvaders.Factories;

public static class AlienFactory
{
    public static Alien CreateAlien(AlienType alienType)
    {
        return alienType switch
        {
            AlienType.Type1 => new AlienType1(),
            AlienType.Type2 => new AlienType2(),
            AlienType.Type3 => new AlienType3(),
            AlienType.Type4 => new AlienType4(),
            _ => throw new ArgumentOutOfRangeException(nameof(alienType), alienType, null)
        };
    }
}
