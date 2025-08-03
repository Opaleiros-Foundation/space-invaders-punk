using SpaceInvaders.Models.Interfaces;

namespace SpaceInvaders.Models;

public class Projectile : Actor, IProjectile
{
    public int Speed { get; private set; }
    public int Damage { get; private set; }

    public Projectile(string name, string spritePath, int health, int speed, int damage)
        : base(name, spritePath, health)
    {
        Speed = speed;
        Damage = damage;
    }

    public void Move()
    {
        Y -= Speed;
    }
}
