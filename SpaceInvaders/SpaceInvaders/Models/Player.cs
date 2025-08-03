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

    public Player(string name, int health, Weapon weapon) 
        : base(name, SpritePaths.Player, health)
    {
        _score = 0;
        _weapon = weapon;
        _projectiles = new ObservableCollection<Projectile>();
    }

    public void Shoot()
    {
        var projectile = new Projectile(
            "Laser",
            Weapon.ProjectileSpritePath,
            1, // Health
            10, // Speed
            Weapon.Damage
        );
        projectile.X = this.X;
        projectile.Y = this.Y;
        
        Projectiles.Add(projectile);
    }
}
