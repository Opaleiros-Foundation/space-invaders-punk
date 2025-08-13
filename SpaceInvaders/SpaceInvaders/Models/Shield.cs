using SpaceInvaders.Constants;

namespace SpaceInvaders.Models;

/// <summary>
/// Represents a shield or barrier in the game.
/// Inherits from <see cref="Actor"/> and includes properties specific to shields.
/// </summary>
public class Shield : Actor
{
    /// <summary>
    /// Gets the maximum health of the shield.
    /// </summary>
    public int MaxHealth { get; private set; }

    /// <summary>
    /// Parameterless constructor for Entity Framework Core.
    /// </summary>
    public Shield() : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Shield"/> class with the specified parameters.
    /// </summary>
    /// <param name="name">The name of the shield.</param>
    /// <param name="health">The initial health of the shield.</param>
    /// <param name="width">The width of the shield.</param>
    /// <param name="height">The height of the shield.</param>
    public Shield(string name, int health, double width, double height)
        : base(name, SpritePaths.Barrier, health, width, height)
    {
        MaxHealth = health;
    }
}
