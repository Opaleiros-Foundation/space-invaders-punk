using CommunityToolkit.Mvvm.ComponentModel;

namespace SpaceInvaders.Models;

public partial class Player : ObservableObject
{
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private double _x;

    [ObservableProperty]
    private double _y;

    [ObservableProperty]
    private int _score;

    public Player(string name)
    {
        _name = name;
        _x = 0;
        _y = 0;
        _score = 0;
    }
}
