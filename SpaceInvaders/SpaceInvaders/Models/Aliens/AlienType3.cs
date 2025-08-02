
using SpaceInvaders.Constants;

namespace SpaceInvaders.Models.Aliens;

public class AlienType3 : Alien
{
    public AlienType3() 
        : base(
            name: "Alien Type 3", 
            spritePath: SpritePaths.AlienType3, 
            health: 150, 
            scoreValue: 30,
            weapon: new Weapon(25, 1.0, SpritePaths.Projectile)
            )
    {
    }
}
