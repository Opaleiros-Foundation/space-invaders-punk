
using SpaceInvaders.Constants;

namespace SpaceInvaders.Models.Aliens;

/// <summary>
/// Represents a specific type of alien, Alien Type 2.
/// Inherits from <see cref="Alien"/> and sets predefined properties for this alien type.
/// </summary>
public class AlienType2 : Alien
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AlienType2"/> class.
    /// Sets the name, sprite path, health, score value, weapon, width, and height for Alien Type 2.
    /// </summary>
    public AlienType2() 
        : base(
            name: "Alien Type 2", 
            spritePath: SpritePaths.AlienType2, 
            health: 120, 
            scoreValue: 20,
            weapon: new Weapon(15, 1.0, SpritePaths.Projectile),
            width: 64,
            height: 64
            )
    {
    }
}
