using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SpaceInvaders.Constants;

namespace SpaceInvaders.Models;

/// <summary>
/// Represents the player character in the game.
/// Inherits from <see cref="Actor"/> and includes properties specific to the player,
/// such as score, lives, weapon, and projectiles.
/// </summary>
public partial class Player : Actor
{
    /// <summary>
    /// Gets or sets the player's current score.
    /// </summary>
    [ObservableProperty]
    private int _score;

    /// <summary>
    /// Gets or sets the number of lives the player has.
    /// </summary>
    [ObservableProperty]
    private int _lives;

    /// <summary>
    /// Gets or sets the player's equipped weapon.
    /// </summary>
    [ObservableProperty]
    private Weapon _weapon;
    
    /// <summary>
    /// Gets or sets the collection of projectiles fired by the player.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Projectile> _projectiles;

    /// <summary>
    /// Gets or sets the collection of scores associated with this player.
    /// </summary>
    public ICollection<Score> Scores { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the player can currently shoot.
    /// </summary>
    [ObservableProperty]
    private bool _canShoot;

    /// <summary>
    /// Parameterless constructor for Entity Framework Core.
    /// Initializes a new instance of the <see cref="Player"/> class with default values.
    /// </summary>
    public Player() : base("", "", 0, 0, 0)
    {
        _score = 0;
        _lives = 3;
        _weapon = new Weapon(0, 0.0, ""); // Initialize with default values
        _projectiles = new ObservableCollection<Projectile>();
        _canShoot = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Player"/> class with the specified parameters.
    /// </summary>
    /// <param name="name">The name of the player.</param>
    /// <param name="health">The initial health of the player.</param>
    /// <param name="weapon">The weapon equipped by the player.</param>
    /// <param name="width">The width of the player.</param>
    /// <param name="height">The height of the player.</param>
    public Player(string name, int health, Weapon weapon, double width, double height) 
        : base(name, SpritePaths.Player, health, width, height)
    {
        _score = 0;
        _lives = 3;
        _weapon = weapon;
        _projectiles = new ObservableCollection<Projectile>();
        _canShoot = true;
    }

    /// <summary>
    /// Fires a projectile from the player's current position if the player can shoot.
    /// </summary>
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
            16 // Height
        );
        projectile.X = this.X;
        projectile.Y = this.Y;
        
        Projectiles.Add(projectile);
        CanShoot = false;
    }
}
