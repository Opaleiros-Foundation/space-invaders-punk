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

    [ObservableProperty]
    private string _livesText;

    private readonly DispatcherTimer _gameTimer;
    private double _alienSpeed;
    private bool _movingRight = true;
    private bool _isMovingLeft;
    private bool _isMovingRight;
    private int _livesAwarded;

    [ObservableProperty]
    private double _gameWidth;

    [ObservableProperty]
    private double _gameHeight;

    [ObservableProperty]
    private int _level;

    private bool _canPlayShootSound = true;
    private readonly DispatcherTimer _shootSoundCooldownTimer;

    public GameStartPageViewModel(INavigator navigator, ISoundService soundService, Player player)
    {
        _navigator = navigator;
        SoundService = soundService; 
        GoToMain = new AsyncRelayCommand(GoToMainView);
        FirePlayerWeaponCommand = new RelayCommand(FirePlayerWeapon);

        _shootSoundCooldownTimer = new DispatcherTimer();
        _shootSoundCooldownTimer.Interval = TimeSpan.FromMilliseconds(GameConstants.PlayerShootCooldownMs); // Cooldown period for shoot sound
        _shootSoundCooldownTimer.Tick += (sender, e) =>
        {
            _canPlayShootSound = true;
            _shootSoundCooldownTimer.Stop();
        };

        Player = player;
        Aliens = new ObservableCollection<Alien>();
        ScoreText = $"SCORE: {Player.Score}";
        LivesText = $"LIVES: {Player.Lives}";
        _livesAwarded = 0;
        Level = 1;
        GameWidth = GameConstants.InitialGameWidth; // Initialize with default canvas width
        GameHeight = GameConstants.InitialGameHeight; // Initialize with default canvas height
        _alienSpeed = GameConstants.InitialAlienSpeed;

        _gameTimer = new DispatcherTimer();
        _gameTimer.Interval = TimeSpan.FromMilliseconds(GameConstants.GameLoopIntervalMs); 
        _gameTimer.Tick += GameTimer_Tick;
        _gameTimer.Start();

        Player.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Player.Score))
            {
                ScoreText = $"SCORE: {Player.Score}";
                // Check for extra life
                var potentialLives = Player.Score / GameConstants.ExtraLifeThreshold;
                if (potentialLives > _livesAwarded && Player.Lives < GameConstants.MaxPlayerLives)
                {
                    var livesToAdd = Math.Min(potentialLives - _livesAwarded, GameConstants.MaxPlayerLives - Player.Lives);
                    if (livesToAdd > 0)
                    {
                        Player.Lives += livesToAdd;
                        _livesAwarded = potentialLives;
                        SoundService.PlaySound(SoundPaths.ExtraLife);
                    }
                }
            }

            if (e.PropertyName == nameof(Player.Lives))
            {
                LivesText = $"LIVES: {Player.Lives}";
            }
        };
    }

    public void GenerateAliens()
    {
        Aliens.Clear();

        void CreateRow(AlienType type, int rowIndex)
        {
            for (var i = 0; i < GameConstants.AliensPerRow; i++)
            {
                var alien = AlienFactory.CreateAlien(type);
                alien.X = GameConstants.WaveStartX + (i * GameConstants.AlienXOffset);
                alien.Y = GameConstants.WaveStartY + (rowIndex * GameConstants.AlienYOffset);
                Aliens.Add(alien);
            }
        }

        // Get the wave pattern from the factory
        var wavePattern = WaveFactory.GenerateWave(Level);

        // Create the rows based on the pattern
        for (int i = 0; i < wavePattern.Count; i++)
        {
            CreateRow(wavePattern[i], i);
        }
    }

    private async void GameTimer_Tick(object? sender, object? e)
    {
        // Game over conditions
        var aliensReachedBottom = Aliens.Any(alien => alien.Y >= GameHeight - GameConstants.ScreenMargin);
        if (Player.Lives <= 0 || aliensReachedBottom)
        {
            _gameTimer.Stop();
            await _navigator.NavigateViewModelAsync<GameOverViewModel>(this, data: Player);
            return;
        }

        if (!Aliens.Any())
        {
            Level++;
            _alienSpeed += GameConstants.AlienSpeedIncrement; // Increase speed every level
            GenerateAliens();
            return;
        }

        // Player Movement
        UpdatePlayerPosition();

        // Alien Movement
        foreach (var alien in Aliens)
        {
            if (_movingRight)
            {
                alien.X += _alienSpeed;
            }
            else
            {
                alien.X -= _alienSpeed;
            }
        }

        if (!Aliens.Any()) return;

        var rightmostAlien = Aliens.Max(a => a.X);
        var leftmostAlien = Aliens.Min(a => a.X);

        if (GameWidth > 0 && rightmostAlien + GameConstants.AlienWidth > GameWidth - GameConstants.ScreenMargin)
        {
            _movingRight = false;
            foreach (var alien in Aliens)
            {
                alien.Y += GameHeight / GameConstants.AlienVerticalStepDivisor;
            }
        }
        else if (leftmostAlien < GameConstants.ScreenMargin)
        {
            _movingRight = true;
            foreach (var alien in Aliens)
            {
                alien.Y += GameHeight / GameConstants.AlienVerticalStepDivisor;
            }
        }
    }

    private void UpdatePlayerPosition()
    {
        if (_isMovingLeft && Player.X - GameConstants.PlayerSpeed > 0)
        {
            Player.X -= GameConstants.PlayerSpeed;
        }

        if (_isMovingRight && Player.X + GameConstants.PlayerSpeed + GameConstants.PlayerWidth < GameWidth)
        {
            Player.X += GameConstants.PlayerSpeed;
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
