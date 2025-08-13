
using SpaceInvaders.Constants;

namespace SpaceInvaders.Models.Aliens;

/// <summary>
/// Represents a specific type of alien, Alien Type 4.
/// Inherits from <see cref="Alien"/> and sets predefined properties for this alien type.
/// </summary>
public class AlienType4 : Alien
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AlienType4"/> class.
    /// Sets the name, sprite path, health, score value, weapon, width, and height for Alien Type 4.
    /// </summary>
        private static readonly Random s_random = new Random();

    /// <summary>
    /// Initializes a new instance of the <see cref="AlienType4"/> class.
    /// Sets the name, sprite path, health, score value, weapon, width, and height for Alien Type 4.
    /// The score value is a random number between 50 and 250.
    /// </summary>
    public AlienType4() 
        : base(
            name: "Alien Type 4", 
            spritePath: SpritePaths.AlienType4, 
            health: 200, 
            scoreValue: s_random.Next(50, 251),
            weapon: new Weapon(50, 1.0, SpritePaths.Projectile),
            width: 64,
            height: 64
            )
    {
    }
}
