using CommunityToolkit.Mvvm.ComponentModel;

namespace SpaceInvaders.Models;

public abstract partial class Alien : Actor
{
    [ObservableProperty]
    private int _scoreValue;

    [ObservableProperty]
    private Weapon _weapon;

    public Alien(string name, string spritePath, int health, int scoreValue, Weapon weapon, double width, double height)
        : base(name, spritePath, health, width, height)
    {
        _scoreValue = scoreValue;
        _weapon = weapon;
    }
}