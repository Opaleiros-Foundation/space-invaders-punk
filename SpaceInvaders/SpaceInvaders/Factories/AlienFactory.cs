
using SpaceInvaders.Models;
using SpaceInvaders.Models.Aliens;

namespace SpaceInvaders.Factories;

/// <summary>
/// A static factory class for creating different types of alien instances.
/// </summary>
public static class AlienFactory
{
    /// <summary>
    /// Creates an alien instance based on the specified alien type.
    /// </summary>
    /// <param name="alienType">The type of alien to create.</param>
    /// <returns>A new instance of the specified alien type.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if an unknown alien type is provided.</exception>
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
