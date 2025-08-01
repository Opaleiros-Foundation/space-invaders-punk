using CommunityToolkit.Mvvm.ComponentModel;

namespace SpaceInvaders.Models;

public abstract partial class Actor : ObservableObject
{
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private double _x;

    [ObservableProperty]
    private double _y;

    [ObservableProperty]
    private string _spritePath;

    [ObservableProperty]
    private int _health;

    [ObservableProperty]
    private bool _isVisible;

    protected Actor(string name, string spritePath, int health)
    {
        _name = name;
        _spritePath = spritePath;
        _health = health;
        _x = 0;
        _y = 0;
        _isVisible = true;
    }
}