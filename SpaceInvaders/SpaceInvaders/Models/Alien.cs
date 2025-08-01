using CommunityToolkit.Mvvm.ComponentModel;

namespace SpaceInvaders.Models;

public partial class Alien : Actor
{
    [ObservableProperty]
    private int _scoreValue;

    [ObservableProperty]
    private Weapon _weapon;

    public Alien(string name, string spritePath, int health, int scoreValue, Weapon weapon)
        : base(name, spritePath, health)
    {
        _scoreValue = scoreValue;
        _weapon = weapon;
    }
}