
using SpaceInvaders.Constants;

namespace SpaceInvaders.Models.Aliens;

public class AlienType4 : Alien
{
    public AlienType4() 
        : base(
            name: "Alien Type 4", 
            spritePath: SpritePaths.AlienType4, 
            health: 200, 
            scoreValue: 50,
            weapon: new Weapon(50, 1.0, SpritePaths.Projectile),
            width: 64,
            height: 64
            )
    {
    }
}
