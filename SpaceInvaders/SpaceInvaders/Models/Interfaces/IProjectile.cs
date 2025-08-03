
namespace SpaceInvaders.Models.Interfaces;

public interface IProjectile
{
    int Speed { get; }
    
    int Damage { get; }
    
    void Move();
}
