
using SpaceInvaders.Constants;

namespace SpaceInvaders.Models.Aliens;

/// <summary>
/// Represents a specific type of alien, Alien Type 3.
/// Inherits from <see cref="Alien"/> and sets predefined properties for this alien type.
/// </summary>
public class AlienType3 : Alien
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AlienType3"/> class.
    /// Sets the name, sprite path, health, score value, weapon, width, and height for Alien Type 3.
    /// </summary>
    public AlienType3() 
        : base(
            name: "Alien Type 3", 
            spritePath: SpritePaths.AlienType3, 
            health: 150, 
            scoreValue: 40,
            weapon: new Weapon(25, 1.0, SpritePaths.Projectile),
            width: 64,
            height: 64
            )
    {
    }
}
