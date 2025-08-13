using SpaceInvaders.Constants;
using SpaceInvaders.Models.Interfaces;

namespace SpaceInvaders.Models;

/// <summary>
/// Represents a projectile fired by an actor in the game.
/// Inherits from <see cref="Actor"/> and implements <see cref="IProjectile"/>.
/// </summary>
public class Projectile : Actor, IProjectile
{
    /// <summary>
    /// Gets the speed of the projectile.
    /// </summary>
    public int Speed { get; private set; }
    /// <summary>
    /// Gets the damage inflicted by the projectile.
    /// </summary>
    public int Damage { get; private set; }
    
    /// <summary>
    /// Gets whether the projectile was fired by an enemy.
    /// </summary>
    public bool IsEnemy { get; }

    /// <summary>
    /// Gets or sets the foreign key for the player who fired this projectile.
    /// </summary>
    public int PlayerId { get; set; } // Foreign Key
    /// <summary>
    /// Gets or sets the navigation property to the player who fired this projectile.
    /// </summary>
    public Player Player { get; set; } // Navigation property

    /// <summary>
    /// Parameterless constructor for Entity Framework Core.
    /// </summary>
    public Projectile() : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Projectile"/> class with the specified parameters.
    /// </summary>
    /// <param name="isEnemy">Whether the projectile is from an enemy.</param>
    /// <param name="name">The name of the projectile.</param>
    /// <param name="spritePath">The path to the projectile's sprite.</param>
    /// <param name="health">The health of the projectile (e.g., how many hits it can take).</param>
    /// <param name="speed">The speed at which the projectile moves.</param>
    /// <param name="damage">The damage the projectile inflicts upon collision.</param>
    public Projectile(bool isEnemy, string name, string spritePath, int health, int speed, int damage)
        : base(name, spritePath, health, GameConstants.ProjectileImageWidth, GameConstants.ProjectileImageHeight)
    {
        IsEnemy = isEnemy;
        Speed = speed;
        Damage = damage;
    }

    /// <summary>
    /// Moves the projectile.
    /// </summary>
    public void Move()
    {
        if (IsEnemy)
        {
            Y += Speed; // Down
        }
        else
        {
            Y -= Speed; // Up
        }
    }

    /// <summary>
    /// Checks if the projectile has gone out of bounds.
    /// </summary>
    /// <param name="boundary">The screen boundary to check against.</param>
    /// <returns>True if out of bounds, false otherwise.</returns>
    public bool CheckBounds(double boundary)
    {
        if (IsEnemy)
        {
            if (Y >= boundary)
            {
                IsVisible = false;
                return true;
            }
        }
        else
        {
            if (Y <= boundary)
            {
                IsVisible = false;
                return true;
            }
        }
        return false;
    }
}
