using SpaceInvaders.Models.Interfaces;

namespace SpaceInvaders.Models;

public class Projectile : Actor, IProjectile
{
    public int Speed { get; private set; }
    public int Damage { get; private set; }

    public Projectile(string name, string spritePath, int health, int speed, int damage, double width, double height)
        : base(name, spritePath, health, width, height)
    {
        Speed = speed;
        Damage = damage;
    }


    public void Move()
    {
        Y -= Speed;
    }

    public bool CheckBounds(double topScreenY)
    {
        if (Y <= topScreenY)
        {
            IsVisible = false;
            return true;
        }
        return false;
    }

    public bool CheckCollision(Actor other)
    {
        // Simple AABB collision detection
        return X < other.X + other.Width &&
               X + Width > other.X &&
               Y < other.Y + other.Height &&
               Y + Height > other.Y;
    }
}
