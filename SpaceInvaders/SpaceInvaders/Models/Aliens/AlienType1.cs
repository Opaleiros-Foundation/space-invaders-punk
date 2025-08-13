
using SpaceInvaders.Constants;

namespace SpaceInvaders.Models.Aliens;

/// <summary>
/// Represents a specific type of alien, Alien Type 1.
/// Inherits from <see cref="Alien"/> and sets predefined properties for this alien type.
/// </summary>
public class AlienType1 : Alien
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AlienType1"/> class.
    /// Sets the name, sprite path, health, score value, weapon, width, and height for Alien Type 1.
    /// </summary>
    public AlienType1() 
        : base(
            name: "Alien Type 1", 
            spritePath: SpritePaths.AlienType1, 
            health: 100, 
            scoreValue: 10,
            weapon: new Weapon(10, 1.0, SpritePaths.Projectile),
            width: 64,
            height: 64
            )
    {
    }
}
