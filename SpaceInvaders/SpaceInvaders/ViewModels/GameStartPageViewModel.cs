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

/// <summary>
/// ViewModel for the main game screen, handling game logic, player and alien movements, and interactions.
/// </summary>
public partial class GameStartPageViewModel : ObservableObject
{
    private readonly INavigator _navigator;
    /// <summary>
    /// Gets the sound service for playing in-game sounds.
    /// </summary>
    public ISoundService SoundService { get; } // Expose SoundService

    /// <summary>
    /// Gets or sets the player instance.
    /// </summary>
    [ObservableProperty]
    private Player _player;

    /// <summary>
    /// Gets or sets the collection of aliens currently in the game.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Alien> _aliens;
    
    [ObservableProperty]
    private Alien _specialAlien;

    /// <summary>
    /// Gets or sets the formatted score text to display.
    /// </summary>
    [ObservableProperty]
    private string _scoreText;

    /// <summary>
    /// Gets or sets the formatted lives text to display.
    /// </summary>
    [ObservableProperty]
    private string _livesText;

    private readonly DispatcherTimer _gameTimer;
    private readonly DispatcherTimer _specialAlienTimer;
    private readonly DispatcherTimer _initialPauseTimer; // New timer for initial pause
    private double _alienSpeed;
    private double _specialAlienSpeed;
    private bool _movingRight = true;
    private bool _isSpecialAlienMovingRight;
    private bool _isMovingLeft;
    private bool _isMovingRight;
    private int _livesAwarded;
    private bool _initialAlienMovementPaused = true; // New flag for initial pause

    /// <summary>
    /// Gets or sets the current width of the game area.
    /// </summary>
    [ObservableProperty]
    private double _gameWidth;

    /// <summary>
    /// Gets or sets the current height of the game area.
    /// </summary>
    [ObservableProperty]
    private double _gameHeight;

    /// <summary>
    /// Gets or sets the current game level.
    /// </summary>
    [ObservableProperty]
    private int _level;

    private bool _canPlayShootSound = true;
    private readonly DispatcherTimer _shootSoundCooldownTimer;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameStartPageViewModel"/> class.
    /// </summary>
    /// <param name="navigator">The navigation service.</param>
    /// <param name="soundService">The sound service.</param>
    /// <param name="player">The player instance for the game.</param>
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
        _specialAlienSpeed = GameConstants.InitialAlienSpeed * 2; // Special alien is faster

        _gameTimer = new DispatcherTimer();
        _gameTimer.Interval = TimeSpan.FromMilliseconds(GameConstants.GameLoopIntervalMs); 
        _gameTimer.Tick += GameTimer_Tick;
        _gameTimer.Start();
        
        _specialAlienTimer = new DispatcherTimer();
        _specialAlienTimer.Tick += SpecialAlienTimer_Tick;
        SetSpecialAlienTimer();
        _specialAlienTimer.Start();

        // Initialize and start initial pause timer
        _initialPauseTimer = new DispatcherTimer();
        _initialPauseTimer.Interval = TimeSpan.FromSeconds(1); // Pause for 1 second
        _initialPauseTimer.Tick += (sender, e) =>
        {
            _initialAlienMovementPaused = false;
            _initialPauseTimer.Stop();
        };
        _initialPauseTimer.Start();

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
    
    private void SetSpecialAlienTimer()
    {
        _specialAlienTimer.Interval = TimeSpan.FromSeconds(new Random().Next(10, 21));
    }
    
    private void SpecialAlienTimer_Tick(object sender, object e)
    {
        if (SpecialAlien is null)
        {
            SpawnSpecialAlien();
        }
        SetSpecialAlienTimer(); // Reset timer for the next spawn
    }
    
    private void SpawnSpecialAlien()
    {
        SpecialAlien = AlienFactory.CreateAlien(Models.AlienType.Type4);
        _isSpecialAlienMovingRight = new Random().Next(0, 2) == 0; // Random direction
        SpecialAlien.Y = GameConstants.WaveStartY; // Top of the screen
        SpecialAlien.X = _isSpecialAlienMovingRight ? 0 : GameWidth - SpecialAlien.Width;
    }

    /// <summary>
    /// Generates a new wave of aliens based on the current game level.
    /// </summary>
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

    /// <summary>
    /// Handles the game loop tick, updating game state, movements, and checking for game over conditions.
    /// </summary>
    private async void GameTimer_Tick(object? sender, object? e)
    {
        // Game over conditions
        var aliensReachedBottom = Aliens.Any(alien => alien.Y >= GameHeight - GameConstants.ScreenMargin);
        if (Player.Lives <= 0 || aliensReachedBottom)
        {
            _gameTimer.Stop();
            _specialAlienTimer.Stop();
            _initialPauseTimer.Stop(); // Stop initial pause timer on game over
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
        
        // Special Alien Movement
        UpdateSpecialAlienPosition();

        // Alien Movement (only if not paused)
        if (!_initialAlienMovementPaused)
        {
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
    }
    
    private void UpdateSpecialAlienPosition()
    {
        if (SpecialAlien is null) return;

        if (_isSpecialAlienMovingRight)
        {
            SpecialAlien.X += _specialAlienSpeed;
            if (SpecialAlien.X > GameWidth)
            {
                SpecialAlien = null; // Disappears off the right edge
            }
        }
        else
        {
            SpecialAlien.X -= _specialAlienSpeed;
            if (SpecialAlien.X + SpecialAlien.Width < 0)
            {
                SpecialAlien = null; // Disappears off the left edge
            }
        }
    }

    /// <summary>
    /// Updates the player's position based on input.
    /// </summary>
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

    /// <summary>
    /// Handles key down events for player input.
    /// </summary>
    /// <param name="key">The virtual key that was pressed.</param>
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

    /// <summary>
    /// Handles key up events for player input.
    /// </summary>
    /// <param name="key">The virtual key that was released.</param>
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

    /// <summary>
    /// Gets the command to navigate to the main menu.
    /// </summary>
    public ICommand GoToMain { get; }
    /// <summary>
    /// Gets the command to fire the player's weapon.
    /// </summary>
    public ICommand FirePlayerWeaponCommand { get; }

    /// <summary>
    /// Navigates to the main view asynchronously.
    /// </summary>
    private async Task GoToMainView()
    {
        await _navigator.NavigateBackAsync(this);
    }

    /// <summary>
    /// Fires the player's weapon, plays a sound, and manages the shooting cooldown.
    /// </summary>
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
