
using SpaceInvaders.Constants;

namespace SpaceInvaders.Models.Aliens;

public class AlienType1 : Alien
{
    public AlienType1() 
        : base(
            name: "Alien Type 1", 
            spritePath: SpritePaths.AlienType1, 
            health: 100, 
            scoreValue: 10,
            weapon: new Weapon(10, 1.0, SpritePaths.Projectile)
            )
    {
    }
}
