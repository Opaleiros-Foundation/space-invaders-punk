using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceInvaders.Constants;
using SpaceInvaders.Factories;
using SpaceInvaders.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.UI.Xaml;
using Windows.System;

namespace SpaceInvaders.Presentation;

public partial class GameStartPageViewModel : ObservableObject
{
    private readonly INavigator _navigator;

    [ObservableProperty]
    private Player _player;

    [ObservableProperty]
    private ObservableCollection<Alien> _aliens;
    private readonly DispatcherTimer _gameTimer;
    private const double AlienSpeed = 2.0;
    private bool _movingRight = true;
    public double ScreenWidth { get; set; }

    public GameStartPageViewModel(INavigator navigator)
    {
        _navigator = navigator;
        GoToMain = new AsyncRelayCommand(GoToMainView);
        FirePlayerWeaponCommand = new RelayCommand(FirePlayerWeapon);

        Player = new Player("Player1", 100, new Weapon(10, 0.5, SpritePaths.Projectile));
        Aliens = new ObservableCollection<Alien>();

        // Generate aliens
        const int startX = 100;
        const int startY = 50;
        const int xOffset = 86;
        const int yOffsetBetweenRows = 70;

        // Row 1: Type 3 aliens
        for (var i = 0; i < 2; i++)
        {
            var alien = AlienFactory.CreateAlien(AlienType.Type3);
            alien.X = startX + (i * xOffset);
            alien.Y = startY + 5;
            Aliens.Add(alien);
        }

        // Row 2: Type 2 aliens
        for (var i = 0; i < 2; i++)
        {
            var alien = AlienFactory.CreateAlien(AlienType.Type2);
            alien.X = startX + (i * xOffset);
            alien.Y = startY + yOffsetBetweenRows + 10;
            Aliens.Add(alien);
        }

        // Row 3: Type 1 aliens
        for (var i = 0; i < 2; i++)
        {
            var alien = AlienFactory.CreateAlien(AlienType.Type1);
            alien.X = startX + (i * xOffset);
            alien.Y = startY + yOffsetBetweenRows + 80;
            Aliens.Add(alien);
        }

        _gameTimer = new DispatcherTimer();
        _gameTimer.Interval = TimeSpan.FromMilliseconds(16); 
        _gameTimer.Tick += GameTimer_Tick;
        _gameTimer.Start();
    }

    private void GameTimer_Tick(object sender, object e)
    {
        foreach (var alien in Aliens)
        {
            if (_movingRight)
            {
                alien.X += AlienSpeed;
            }
            else
            {
                alien.X -= AlienSpeed;
            }
        }

        if (!Aliens.Any()) return;

        var rightmostAlien = Aliens.Max(a => a.X);
        var leftmostAlien = Aliens.Min(a => a.X);

        if (rightmostAlien > 700)
        {
            _movingRight = false;
            foreach (var alien in Aliens)
            {
                alien.Y += 20;
            }
        }
        else if (leftmostAlien < 50)
        {
            _movingRight = true;
            foreach (var alien in Aliens)
            {
                alien.Y += 20;
            }
        }
    }

    public void HandleKeyDown(VirtualKey key)
    {
        const double playerSpeed = 15.0;
        const double playerWidth = 64; // Player's width

        switch (key)
        {
            case VirtualKey.Left:
            case VirtualKey.A:
                if (Player.X - playerSpeed > 0)
                {
                    Player.X -= playerSpeed;
                }
                break;
            case VirtualKey.Right:
            case VirtualKey.D:
                if (Player.X + playerSpeed + playerWidth < ScreenWidth)
                {
                    Player.X += playerSpeed;
                }
                break;
            case VirtualKey.Space:
                FirePlayerWeapon();
                break;
        }
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
