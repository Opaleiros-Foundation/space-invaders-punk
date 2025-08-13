namespace SpaceInvaders.Models;

/// <summary>
/// Represents a weapon in the game, defining its damage, fire rate, and projectile sprite.
/// </summary>
public class Weapon
{
    /// <summary>
    /// Gets or sets the unique identifier for the weapon.
    /// </summary>
    public int Id { get; set; } // Primary Key
    /// <summary>
    /// Gets or sets the damage inflicted by the weapon.
    /// </summary>
    public int Damage { get; set; }
    /// <summary>
    /// Gets or sets the fire rate of the weapon (e.g., projectiles per second).
    /// </summary>
    public double FireRate { get; set; }
    /// <summary>
    /// Gets or sets the path to the sprite used for projectiles fired by this weapon.
    /// </summary>
    public string ProjectileSpritePath { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the player who owns this weapon.
    /// </summary>
    public int PlayerId { get; set; } // Foreign Key
    /// <summary>
    /// Gets or sets the navigation property to the player who owns this weapon.
    /// </summary>
    public Player Player { get; set; } // Navigation property

    /// <summary>
    /// Parameterless constructor for Entity Framework Core.
    /// </summary>
    public Weapon() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Weapon"/> class with the specified parameters.
    /// </summary>
    /// <param name="damage">The damage inflicted by the weapon.</param>
    /// <param name="fireRate">The fire rate of the weapon.</param>
    /// <param name="projectileSpritePath">The path to the projectile sprite used by this weapon.</param>
    public Weapon(int damage, double fireRate, string projectileSpritePath)
    {
        Damage = damage;
        FireRate = fireRate;
        ProjectileSpritePath = projectileSpritePath;
    }
}
