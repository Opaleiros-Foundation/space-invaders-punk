using CommunityToolkit.Mvvm.ComponentModel;

namespace SpaceInvaders.Models;

public abstract partial class Alien : Actor
{
    [ObservableProperty]
    private int _scoreValue;

    [ObservableProperty]
    private Weapon _weapon;

    // Parameterless constructor for Entity Framework Core
    protected Alien() : base() 
    {
        _scoreValue = 0;
        _weapon = new Weapon(0, 0.0, ""); // Initialize with default values
    }

    public Alien(string name, string spritePath, int health, int scoreValue, Weapon weapon, double width, double height)
        : base(name, spritePath, health, width, height)
    {
        _scoreValue = scoreValue;
        _weapon = weapon;
    }
}