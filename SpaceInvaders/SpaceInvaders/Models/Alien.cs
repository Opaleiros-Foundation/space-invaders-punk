using CommunityToolkit.Mvvm.ComponentModel;

namespace SpaceInvaders.Models;

/// <summary>
/// Represents an abstract base class for all alien types in the game.
/// Inherits from <see cref="Actor"/> and adds properties specific to aliens, such as score value and weapon.
/// </summary>
public abstract partial class Alien : Actor
{
    /// <summary>
    /// Gets or sets the score value awarded to the player when this alien is defeated.
    /// </summary>
    [ObservableProperty]
    private int _scoreValue;

    /// <summary>
    /// Gets or sets the weapon equipped by this alien.
    /// </summary>
    [ObservableProperty]
    private Weapon _weapon;

    /// <summary>
    /// Parameterless constructor for Entity Framework Core.
    /// Initializes a new instance of the <see cref="Alien"/> class with default values.
    /// </summary>
    protected Alien() : base() 
    {
        _scoreValue = 0;
        _weapon = new Weapon(0, 0.0, ""); // Initialize with default values
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Alien"/> class with the specified parameters.
    /// </summary>
    /// <param name="name">The name of the alien.</param>
    /// <param name="spritePath">The path to the alien's sprite.</param>
    /// <param name="health">The initial health of the alien.</param>
    /// <param name="scoreValue">The score value awarded when the alien is defeated.</param>
    /// <param name="weapon">The weapon equipped by the alien.</param>
    /// <param name="width">The width of the alien.</param>
    /// <param name="height">The height of the alien.</param>
    public Alien(string name, string spritePath, int health, int scoreValue, Weapon weapon, double width, double height)
        : base(name, spritePath, health, width, height)
    {
        _scoreValue = scoreValue;
        _weapon = weapon;
    }
}