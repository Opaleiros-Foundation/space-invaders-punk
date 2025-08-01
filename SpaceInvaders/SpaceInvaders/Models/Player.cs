using CommunityToolkit.Mvvm.ComponentModel;
using SpaceInvaders.Constants;

namespace SpaceInvaders.Models;

public partial class Player : Actor
{
    [ObservableProperty]
    private int _score;

    [ObservableProperty]
    private Weapon _weapon;

    public Player(string name, int health, Weapon weapon) 
        : base(name, SpritePaths.Player, health)
    {
        _score = 0;
        _weapon = weapon;
    }
}
