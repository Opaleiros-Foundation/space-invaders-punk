using CommunityToolkit.Mvvm.ComponentModel;

namespace SpaceInvaders.Models;

public abstract partial class Actor : ObservableObject
{
    public int Id { get; set; } // Primary Key

    private string _name;

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    [ObservableProperty]
    private double _x;

    [ObservableProperty]
    private double _y;

    [ObservableProperty]
    private double _width;

    [ObservableProperty]
    private double _height;

    [ObservableProperty]
    private string _spritePath;

    [ObservableProperty]
    private int _health;

    [ObservableProperty]
    private bool _isVisible;

    // Parameterless constructor for Entity Framework Core
    protected Actor() { }

    protected Actor(string name, string spritePath, int health, double width = 0, double height = 0)
    {
        _name = name;
        _spritePath = spritePath;
        _health = health;
        _x = 0;
        _y = 0;
        _width = width;
        _height = height;
        _isVisible = true;
    }

    public bool CheckCollision(Actor other)
    {
        // Simple AABB collision detection
        return X < other.X + other.Width &&
               X + Width > other.X &&
               Y < other.Y + other.Height &&
               Y + Height > other.Y;
    }
}