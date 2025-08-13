
namespace SpaceInvaders.Models.Interfaces;

/// <summary>
/// Defines the contract for a projectile in the game.
/// </summary>
public interface IProjectile
{
    /// <summary>
    /// Gets the speed of the projectile.
    /// </summary>
    int Speed { get; }
    
    /// <summary>
    /// Gets the damage inflicted by the projectile.
    /// </summary>
    int Damage { get; }
    
    /// <summary>
    /// Moves the projectile.
    /// </summary>
    void Move();
}
