
using SpaceInvaders.Constants;

namespace SpaceInvaders.Models.Aliens;

public class AlienType2 : Alien
{
    public AlienType2() 
        : base(
            name: "Alien Type 2", 
            spritePath: SpritePaths.AlienType2, 
            health: 120, 
            scoreValue: 20,
            weapon: new Weapon(15, 1.0, SpritePaths.Projectile)
            )
    {
    }
}
