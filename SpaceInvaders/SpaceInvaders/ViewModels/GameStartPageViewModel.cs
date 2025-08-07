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
using SpaceInvaders.Interfaces.Services;

namespace SpaceInvaders.Presentation;

public partial class GameStartPageViewModel : ObservableObject
{
    private readonly INavigator _navigator;
    public ISoundService SoundService { get; } // Expose SoundService

    [ObservableProperty]
    private Player _player;

    [ObservableProperty]
    private ObservableCollection<Alien> _aliens;

    [ObservableProperty]
    private string _scoreText;

    private readonly DispatcherTimer _gameTimer;
    private const double AlienSpeed = 2.0;
    private bool _movingRight = true;
    private bool _isMovingLeft;
    private bool _isMovingRight;
    public double ScreenWidth { get; set; }

    private bool _canPlayShootSound = true;
    private readonly DispatcherTimer _shootSoundCooldownTimer;

    public GameStartPageViewModel(INavigator navigator, ISoundService soundService, Player player)
    {
        _navigator = navigator;
        SoundService = soundService; // Assign to the public property
        GoToMain = new AsyncRelayCommand(GoToMainView);
        FirePlayerWeaponCommand = new RelayCommand(FirePlayerWeapon);

        _shootSoundCooldownTimer = new DispatcherTimer();
        _shootSoundCooldownTimer.Interval = TimeSpan.FromMilliseconds(100); // Cooldown period for shoot sound
        _shootSoundCooldownTimer.Tick += (sender, e) =>
        {
            _canPlayShootSound = true;
            _shootSoundCooldownTimer.Stop();
        };

        Player = player;
        Aliens = new ObservableCollection<Alien>();
        ScoreText = $"SCORE: {Player.Score}";

        // Generate aliens
        const int startX = 100;
        const int startY = 50;
        const int xOffset = 86;
        const int yOffsetBetweenRows = 70;

        // Row 1: Type 3 aliens
        for (var i = 0; i < 8; i++)
        {
            var alien = AlienFactory.CreateAlien(AlienType.Type3);
            alien.X = startX + (i * xOffset);
            alien.Y = startY + 5;
            Aliens.Add(alien);
        }

        // Row 2: Type 2 aliens
        for (var i = 0; i < 8; i++)
        {
            var alien = AlienFactory.CreateAlien(AlienType.Type2);
            alien.X = startX + (i * xOffset);
            alien.Y = startY + yOffsetBetweenRows + 10;
            Aliens.Add(alien);
        }

        // Row 3: Type 1 aliens
        for (var i = 0; i < 8; i++)
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

        Player.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Player.Score))
            {
                ScoreText = $"SCORE: {Player.Score}";
            }
        };
    }

    private async void GameTimer_Tick(object? sender, object? e)
    {
        // Game over conditions
        var aliensReachedBottom = Aliens.Any(alien => alien.Y >= 550);
        if (Player.Lives <= 0 || aliensReachedBottom || Player.Score >= 500 || !Aliens.Any())
        {
            _gameTimer.Stop();
            await _navigator.NavigateViewModelAsync<GameOverViewModel>(this, data: Player);
            return;
        }

        // Player Movement
        UpdatePlayerPosition();

        // Alien Movement
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
                alien.Y += 10;
            }
        }
        else if (leftmostAlien < 50)
        {
            _movingRight = true;
            foreach (var alien in Aliens)
            {
                alien.Y += 10;
            }
        }
    }

    private void UpdatePlayerPosition()
    {
        const double playerSpeed = 4.0;
        const double playerWidth = 64;

        if (_isMovingLeft && Player.X - playerSpeed > 0)
        {
            Player.X -= playerSpeed;
        }

        if (_isMovingRight && Player.X + playerSpeed + playerWidth < ScreenWidth)
        {
            Player.X += playerSpeed;
        }
    }

    public void HandleKeyDown(VirtualKey key)
    {
        switch (key)
        {
            case VirtualKey.Left:
            case VirtualKey.A:
                _isMovingLeft = true;
                break;
            case VirtualKey.Right:
            case VirtualKey.D:
                _isMovingRight = true;
                break;
            case VirtualKey.Space:
                FirePlayerWeapon();
                break;
        }
    }

    public void HandleKeyUp(VirtualKey key)
    {
        switch (key)
        {
            case VirtualKey.Left:
            case VirtualKey.A:
                _isMovingLeft = false;
                break;
            case VirtualKey.Right:
            case VirtualKey.D:
                _isMovingRight = false;
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
        if (!_canPlayShootSound) return;

        var random = new Random();
        var soundIndex = random.Next(SoundPaths.PlayerShoot.Count);
        var soundPath = SoundPaths.PlayerShoot[soundIndex];
        SoundService.PlaySound(soundPath);
        
        _canPlayShootSound = false;
        _shootSoundCooldownTimer.Start();
        
        Player.Shoot();
    }
}
