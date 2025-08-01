namespace SpaceInvaders.Models;

public class Weapon
{
    public int Damage { get; set; }
    public double FireRate { get; set; }
    public string ProjectileSpritePath { get; set; }

    public Weapon(int damage, double fireRate, string projectileSpritePath)
    {
        Damage = damage;
        FireRate = fireRate;
        ProjectileSpritePath = projectileSpritePath;
    }
}