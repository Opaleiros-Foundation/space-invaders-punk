using SpaceInvaders.Constants;

namespace SpaceInvaders.Models;

public class Shield : Actor
{
    public int MaxHealth { get; private set; }

    public Shield(string name, int health, double width, double height)
        : base(name, SpritePaths.Barrier, health, width, height)
    {
        MaxHealth = health;
    }
}
