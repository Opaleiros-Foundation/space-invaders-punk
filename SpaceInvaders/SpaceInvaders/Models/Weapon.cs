namespace SpaceInvaders.Models;

public class Weapon
{
    public int Damage { get; set; }
    public double FireRate { get; set; }
    public string ProjectileType { get; set; }

    public Weapon(int damage, double fireRate, string projectileType)
    {
        Damage = damage;
        FireRate = fireRate;
        ProjectileType = projectileType;
    }
}