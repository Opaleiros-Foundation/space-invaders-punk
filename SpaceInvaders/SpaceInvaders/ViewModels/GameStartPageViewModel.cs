using CommunityToolkit.Mvvm.ComponentModel;
using SpaceInvaders.Constants;
using SpaceInvaders.Models;

namespace SpaceInvaders.Presentation;

public partial class GameStartPageViewModel : ObservableObject
{
    private readonly INavigator _navigator;

    [ObservableProperty]
    private Player _player;

    [ObservableProperty]
    private Alien _alien;

    public GameStartPageViewModel(INavigator navigator)
    {
        _navigator = navigator;
        GoToMain = new AsyncRelayCommand(GoToMainView);
        _player = new Player("Player1", 100, new Weapon(10, 0.5, "Laser"));
        _alien = new Alien("Alien1", SpritePaths.AlienType1, 50, 100, new Weapon(5, 1.0, "Bullet"));
    }

    public ICommand GoToMain { get; }

    private async Task GoToMainView()
    {
        await _navigator.NavigateBackAsync(this);
    }
}
