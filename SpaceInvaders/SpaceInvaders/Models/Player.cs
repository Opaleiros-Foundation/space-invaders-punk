using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SpaceInvaders.Constants;

namespace SpaceInvaders.Models;

public partial class Player : Actor
{
    [ObservableProperty]
    private int _score;

    [ObservableProperty]
    private Weapon _weapon;
    
    [ObservableProperty]
    private ObservableCollection<Projectile> _projectiles;

    [ObservableProperty]
    private bool _canShoot;

    public Player(string name, int health, Weapon weapon, double width, double height) 
        : base(name, SpritePaths.Player, health, width, height)
    {
        _score = 0;
        _weapon = weapon;
        _projectiles = new ObservableCollection<Projectile>();
        _canShoot = true;
    }

    public void Shoot()
    {
        if (!CanShoot)
        {
            return;
        }

        var projectile = new Projectile(
            "Laser",
            Weapon.ProjectileSpritePath,
            1, // Health
            10, // Speed
            Weapon.Damage,
            16, // Width
            32 // Height
        );
        projectile.X = this.X;
        projectile.Y = this.Y;
        
        Projectiles.Add(projectile);
        CanShoot = false;
    }
}
