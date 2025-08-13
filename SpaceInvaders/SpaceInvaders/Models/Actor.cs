using CommunityToolkit.Mvvm.ComponentModel;

namespace SpaceInvaders.Models;

/// <summary>
/// Represents a generic actor in the game, such as a player, enemy, or projectile.
/// Provides basic properties for positioning, size, health, and visibility,
/// as well as a method for collision detection.
/// </summary>
public abstract partial class Actor : ObservableObject
{
    /// <summary>
    /// Gets or sets the unique identifier of the actor.
    /// </summary>
    public int Id { get; set; } // Primary Key

    private string _name;

    /// <summary>
    /// Gets or sets the name of the actor.
    /// </summary>
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    /// <summary>
    /// Gets or sets the X-coordinate of the actor's position.
    /// </summary>
    [ObservableProperty]
    private double _x;

    /// <summary>
    /// Gets or sets the Y-coordinate of the actor's position.
    /// </summary>
    [ObservableProperty]
    private double _y;

    /// <summary>
    /// Gets or sets the width of the actor.
    /// </summary>
    [ObservableProperty]
    private double _width;

    /// <summary>
    /// Gets or sets the height of the actor.
    /// </summary>
    [ObservableProperty]
    private double _height;

    /// <summary>
    /// Gets or sets the path to the actor's sprite (image).
    /// </summary>
    [ObservableProperty]
    private string _spritePath;

    /// <summary>
    /// Gets or sets the current health of the actor.
    /// </summary>
    [ObservableProperty]
    private int _health;

    /// <summary>
    /// Gets or sets a value indicating whether the actor is visible.
    /// </summary>
    [ObservableProperty]
    private bool _isVisible;

    /// <summary>
    /// Parameterless constructor, required for Entity Framework Core.
    /// </summary>
    protected Actor() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Actor"/> class with the specified parameters.
    /// </summary>
    /// <param name="name">The name of the actor.</param>
    /// <param name="spritePath">The path to the actor's sprite.</param>
    /// <param name="health">The initial health of the actor.</param>
    /// <param name="width">The width of the actor (optional, defaults to 0).</param>
    /// <param name="height">The height of the actor (optional, defaults to 0).</param>
    protected Actor(string name, string spritePath, int health, double width = 0, double height = 0)
    {
        _name = name;
        _spritePath = spritePath;
        _health = health;
        _x = 0;
        _y = 0;
        _width = width;
        _height = height;
        _isVisible = true;
    }

    /// <summary>
    /// Checks if this actor collides with another actor using AABB (Axis-Aligned Bounding Box) collision detection.
    /// </summary>
    /// <param name="other">The other actor to check for collision.</param>
    /// <returns>
    /// <c>true</c> if there is a collision between the two actors; otherwise, <c>false</c>.
    /// </returns>
    public bool CheckCollision(Actor other)
    {
        // Simple AABB collision detection
        return X < other.X + other.Width &&
               X + Width > other.X &&
               Y < other.Y + other.Height &&
               Y + Height > other.Y;
    }
}