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
        _shootSoundCooldownTimer.Interval = TimeSpan.FromMilliseconds(100); // Cooldown period for shoot sound
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
        GameWidth = 800; // Initialize with default canvas width
        GameHeight = 600; // Initialize with default canvas height
        _alienSpeed = 2.0;

        _gameTimer = new DispatcherTimer();
        _gameTimer.Interval = TimeSpan.FromMilliseconds(16); 
        _gameTimer.Tick += GameTimer_Tick;
        _gameTimer.Start();

        Player.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Player.Score))
            {
                ScoreText = $"SCORE: {Player.Score}";
                // Check for extra life
                var potentialLives = Player.Score / 1000;
                if (potentialLives > _livesAwarded && Player.Lives < 6)
                {
                    var livesToAdd = Math.Min(potentialLives - _livesAwarded, 6 - Player.Lives);
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

        const int startX = 100;
        const int startY = 50;
        const int xOffset = 40;
        const int yOffsetBetweenRows = 40;
        const int aliensPerRow = 12;

        void CreateRow(AlienType type, int rowIndex)
        {
            for (var i = 0; i < aliensPerRow; i++)
            {
                var alien = AlienFactory.CreateAlien(type);
                alien.X = startX + (i * xOffset);
                alien.Y = startY + (rowIndex * yOffsetBetweenRows);
                Aliens.Add(alien);
            }
        }

        // Level 1 starts with 3 rows.
        // Every 2 levels (at 3, 5, 7...) add a row.
        int totalRows = 3 + ((Level - 1) / 2);
        
        // Cap at 6 rows.
        totalRows = Math.Min(totalRows, 6);

        // Defines the pattern of rows to be added
        var rowPattern = new List<AlienType>
        {
            AlienType.Type3, // Row 0
            AlienType.Type2, // Row 1
            AlienType.Type1, // Row 2
            AlienType.Type2, // Row 3 (added at level 3)
            AlienType.Type1, // Row 4 (added at level 5)
            AlienType.Type1  // Row 5 (added at level 7)
        };

        // Create the rows based on the calculated total
        for (int i = 0; i < totalRows; i++)
        {
            CreateRow(rowPattern[i], i);
        }
    }

    private async void GameTimer_Tick(object? sender, object? e)
    {
        // Game over conditions
        var aliensReachedBottom = Aliens.Any(alien => alien.Y >= GameHeight - 50);
        if (Player.Lives <= 0 || aliensReachedBottom)
        {
            _gameTimer.Stop();
            await _navigator.NavigateViewModelAsync<GameOverViewModel>(this, data: Player);
            return;
        }

        if (!Aliens.Any())
        {
            Level++;
            _alienSpeed += 0.25; // Increase speed every level
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

        if (GameWidth > 0 && rightmostAlien + 64 > GameWidth - 50)
        {
            _movingRight = false;
            foreach (var alien in Aliens)
            {
                alien.Y += GameHeight / 90.0;
            }
        }
        else if (leftmostAlien < 50)
        {
            _movingRight = true;
            foreach (var alien in Aliens)
            {
                alien.Y += GameHeight / 90.0;
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

        if (_isMovingRight && Player.X + playerSpeed + playerWidth < GameWidth)
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
