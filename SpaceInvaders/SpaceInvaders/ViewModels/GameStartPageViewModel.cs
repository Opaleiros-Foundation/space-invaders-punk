using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceInvaders.Constants;
using SpaceInvaders.Models;
using System.Windows.Input;

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
        FirePlayerWeaponCommand = new RelayCommand(FirePlayerWeapon);

        _player = new Player("Player1", 100, new Weapon(10, 0.5, SpritePaths.Projectile));
        _alien = new Alien("Alien1", SpritePaths.AlienType1, 50, 100, new Weapon(5, 1.0, SpritePaths.Projectile));
    }

    public ICommand GoToMain { get; }
    public ICommand FirePlayerWeaponCommand { get; }

    private async Task GoToMainView()
    {
        await _navigator.NavigateBackAsync(this);
    }

    private void FirePlayerWeapon()
    {
        _player.FireWeapon();
    }
}
