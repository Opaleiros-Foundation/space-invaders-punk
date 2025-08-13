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
    /// <param name="name">The name of the projectile.</param>
    /// <param name="spritePath">The path to the projectile's sprite.</param>
    /// <param name="health">The health of the projectile (e.g., how many hits it can take).</param>
    /// <param name="speed">The speed at which the projectile moves.</param>
    /// <param name="damage">The damage the projectile inflicts upon collision.</param>
    /// <param name="width">The width of the projectile.</param>
    /// <param name="height">The height of the projectile.</param>
    public Projectile(string name, string spritePath, int health, int speed, int damage, double width, double height)
        : base(name, spritePath, health, width, height)
    {
        Speed = speed;
        Damage = damage;
    }

    /// <summary>
    /// Moves the projectile upwards by its speed.
    /// </summary>
    public void Move()
    {
        Y -= Speed;
    }

    /// <summary>
    /// Checks if the projectile has gone out of bounds (above the top of the screen).
    /// If it has, sets its visibility to false.
    /// </summary>
    /// <param name="topScreenY">The Y-coordinate representing the top boundary of the screen.</param>
    /// <returns>
    /// <c>true</c> if the projectile is out of bounds; otherwise, <c>false</c>.
    /// </returns>
    public bool CheckBounds(double topScreenY)
    {
        if (Y <= topScreenY)
        {
            IsVisible = false;
            return true;
        }
        return false;
    }
}
