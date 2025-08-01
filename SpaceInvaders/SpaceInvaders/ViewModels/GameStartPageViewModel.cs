using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceInvaders.Constants;
using SpaceInvaders.Factories;
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

        Player = new Player("Player1", 100, new Weapon(10, 0.5, SpritePaths.Projectile));
        Alien = AlienFactory.CreateAlien(AlienType.Type1);
        Alien.X = 200; // Posição X para teste
        Alien.Y = 50;  // Posição Y para teste
    }

    public ICommand GoToMain { get; }
    public ICommand FirePlayerWeaponCommand { get; }

    private async Task GoToMainView()
    {
        await _navigator.NavigateBackAsync(this);
    }

    private void FirePlayerWeapon()
    {
        Player.FireWeapon();
    }
}
