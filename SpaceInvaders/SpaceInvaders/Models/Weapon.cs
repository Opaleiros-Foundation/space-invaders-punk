namespace SpaceInvaders.Models;

public class Weapon
{
    public int Id { get; set; } // Primary Key
    public int Damage { get; set; }
    public double FireRate { get; set; }
    public string ProjectileSpritePath { get; set; }

    public int PlayerId { get; set; } // Foreign Key
    public Player Player { get; set; } // Navigation property

    // Parameterless constructor for Entity Framework Core
    public Weapon() { }

    public Weapon(int damage, double fireRate, string projectileSpritePath)
    {
        Damage = damage;
        FireRate = fireRate;
        ProjectileSpritePath = projectileSpritePath;
    }
}