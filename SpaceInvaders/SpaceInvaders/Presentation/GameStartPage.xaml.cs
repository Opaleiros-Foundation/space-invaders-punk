using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.System;
using SpaceInvaders.Models;
using SpaceInvaders.Constants;
using SpaceInvaders.Interfaces.Services;

namespace SpaceInvaders.Presentation
{
    public sealed partial class GameStartPage : Page
    {
        private readonly List<Image> _alienImages = new();
        private readonly List<Image> _projectileImages = new();
        private readonly List<Image> _enemyProjectileImages = new();
        private readonly List<Image> _shieldImages = new();
        private readonly List<Shield> _shields = new();
        private Image? _playerImage;
        private Image? _specialAlienImage;

        /// <summary>
        /// Represents the image control for the special alien.
        /// </summary>
        private DispatcherTimer? _gameTimer;
        private ISoundService? _soundService;

        private bool _canPlayEnemyDeathSound = true;
        private readonly DispatcherTimer _enemyDeathSoundCooldownTimer;

        public GameStartPage()
        {
            InitializeComponent();
            Loaded += GameStartPage_Loaded;
            Unloaded += GameStartPage_Unloaded;
            DataContextChanged += OnDataContextChanged;

            _enemyDeathSoundCooldownTimer = new DispatcherTimer();
            _enemyDeathSoundCooldownTimer.Interval = TimeSpan.FromMilliseconds(GameConstants.EnemyDeathSoundCooldownMs); // Cooldown period for enemy death sound
            _enemyDeathSoundCooldownTimer.Tick += (sender, e) =>
            {
                _canPlayEnemyDeathSound = true;
                _enemyDeathSoundCooldownTimer.Stop();
            };
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (DataContext is not GameStartPageViewModel viewModel) return;

            _soundService = viewModel.SoundService; // Get the sound service from the ViewModel

            CreatePlayerImage(viewModel);
            CreateAlienImages(viewModel);
            viewModel.PropertyChanged += ViewModel_Aliens_PropertyChanged; // Renamed handler
            viewModel.PropertyChanged += ViewModel_SpecialAlien_PropertyChanged; // New handler for special alien
            viewModel.Player.Projectiles.CollectionChanged += Projectiles_CollectionChanged;
            viewModel.EnemyProjectiles.CollectionChanged += EnemyProjectiles_CollectionChanged;
            viewModel.Aliens.CollectionChanged += Aliens_CollectionChanged;
        }

        private void EnemyProjectiles_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (Projectile projectile in e.NewItems)
                    {
                        var projectileImage = new Image
                        {
                            Width = GameConstants.ProjectileImageWidth,
                            Height = GameConstants.ProjectileImageHeight,
                            Source = new BitmapImage(new Uri(projectile.SpritePath))
                        };
                        
                        projectile.X -= (projectileImage.Width / 2);

                        Canvas.SetLeft(projectileImage, projectile.X);
                        Canvas.SetTop(projectileImage, projectile.Y);

                        GameCanvas.Children.Add(projectileImage);
                        _enemyProjectileImages.Add(projectileImage);
                    }
                }
            });
        }

        private void Aliens_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                if (DataContext is not GameStartPageViewModel viewModel) return;

                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (Alien alien in e.OldItems)
                    {
                        var imageToRemove = _alienImages.FirstOrDefault(img => img.Tag == alien);
                        if (imageToRemove != null)
                        {
                            GameCanvas.Children.Remove(imageToRemove);
                            _alienImages.Remove(imageToRemove);
                        }
                    }
                }
                else // Handles Reset (from Clear) and Add
                {
                    CreateAlienImages(viewModel);
                }
            });
        }

        private void CreateShieldImages()
        {
            if (DataContext is not GameStartPageViewModel viewModel) return;

            var screenWidth = viewModel.GameWidth;
            var spacing = (screenWidth - (GameConstants.NumberOfShields * GameConstants.ShieldWidth)) / GameConstants.ShieldSpacingDivisor;

            for (var i = 0; i < GameConstants.NumberOfShields; i++)
            {
                var shield = new Shield("Shield", GameConstants.ShieldHealth, GameConstants.ShieldWidth, GameConstants.ShieldHeight);
                var shieldImage = new Image
                {
                    Width = GameConstants.ShieldWidth,
                    Height = GameConstants.ShieldHeight,
                    Source = new BitmapImage(new Uri(shield.SpritePath))
                };

                shield.X = spacing * (i + 1) + (shield.Width * i);
                shield.Y = viewModel.GameHeight - GameConstants.ShieldYOffsetFromBottom;

                Canvas.SetLeft(shieldImage, shield.X);
                Canvas.SetTop(shieldImage, shield.Y);

                GameCanvas.Children.Add(shieldImage);
                _shieldImages.Add(shieldImage);
                _shields.Add(shield);
            }
        }

        private void Projectiles_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (Projectile projectile in e.NewItems)
                    {
                        var projectileImage = new Image
                        {
                            Width = GameConstants.ProjectileImageWidth,
                            Height = GameConstants.ProjectileImageHeight,
                            Source = new BitmapImage(new Uri(projectile.SpritePath))
                        };
                        
                        double playerImageWidth = GameConstants.PlayerWidth;
                        if (_playerImage != null)
                        {
                            playerImageWidth = _playerImage.Width;
                        }
                        projectile.X = projectile.X + (playerImageWidth / GameConstants.CenteringDivisor) - (projectileImage.Width / GameConstants.CenteringDivisor);
                        projectile.Y -= GameConstants.ProjectileInitialYOffset;

                        Canvas.SetLeft(projectileImage, projectile.X);
                        Canvas.SetTop(projectileImage, projectile.Y);

                        GameCanvas.Children.Add(projectileImage);
                        _projectileImages.Add(projectileImage);
                    }
                }
            });
        }

        private void CreatePlayerImage(GameStartPageViewModel viewModel)
        {
            if (_playerImage != null) return;

            _playerImage = new Image
            {
                Width = GameConstants.PlayerImageWidth,
                Height = GameConstants.PlayerImageHeight,
                Source = new BitmapImage(new Uri(viewModel.Player.SpritePath))
            };

            viewModel.Player.PropertyChanged += (s, e) =>
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    switch (e.PropertyName)
                    {
                        case nameof(Player.X):
                            Canvas.SetLeft(_playerImage, viewModel.Player.X);
                            break;
                        case nameof(Player.Y):
                            Canvas.SetTop(_playerImage, viewModel.Player.Y);
                            break;
                    }
                });
            };

            GameCanvas.Children.Add(_playerImage);
        }

        private void CreateAlienImages(GameStartPageViewModel viewModel)
        {
            foreach (var alienImage in _alienImages)
            {
                GameCanvas.Children.Remove(alienImage);
            }
            _alienImages.Clear();

            foreach (var alien in viewModel.Aliens)
            {
                var image = new Image
                {
                    Width = GameConstants.AlienImageWidth,
                    Height = GameConstants.AlienImageHeight,
                    Source = new BitmapImage(new Uri(alien.SpritePath)),
                    Tag = alien
                };

                Canvas.SetLeft(image, alien.X);
                Canvas.SetTop(image, alien.Y);

                GameCanvas.Children.Add(image);
                _alienImages.Add(image);

                alien.PropertyChanged += (s, e) =>
                {
                    DispatcherQueue.TryEnqueue(() =>
                    {
                        switch (e.PropertyName)
                        {
                            case nameof(Alien.X):
                                Canvas.SetLeft(image, alien.X);
                                break;
                            case nameof(Alien.Y):
                                Canvas.SetTop(image, alien.Y);
                                break;
                        }
                    });
                };
            }
        }

        /// <summary>
        /// Creates and adds the image control for the special alien to the game canvas.
        /// Subscribes to the special alien's property changes to update its position.
        /// </summary>
        /// <param name="viewModel">The GameStartPageViewModel instance.</param>
        private void CreateSpecialAlienImage(GameStartPageViewModel viewModel)
        {
            if (viewModel.SpecialAlien == null) return;

            _specialAlienImage = new Image
            {
                Width = GameConstants.AlienImageWidth, // Assuming same size as regular aliens
                Height = GameConstants.AlienImageHeight,
                Source = new BitmapImage(new Uri(viewModel.SpecialAlien.SpritePath)),
                Tag = viewModel.SpecialAlien // Tag for easy reference
            };

            Canvas.SetLeft(_specialAlienImage, viewModel.SpecialAlien.X);
            Canvas.SetTop(_specialAlienImage, viewModel.SpecialAlien.Y);

            GameCanvas.Children.Add(_specialAlienImage);

            // Subscribe to property changes for the special alien
            viewModel.SpecialAlien.PropertyChanged += (s, e) =>
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    if (_specialAlienImage == null) return; // Image might have been removed

                    switch (e.PropertyName)
                    {
                        case nameof(Alien.X):
                            Canvas.SetLeft(_specialAlienImage, viewModel.SpecialAlien.X);
                            break;
                        case nameof(Alien.Y):
                            Canvas.SetTop(_specialAlienImage, viewModel.SpecialAlien.Y);
                            break;
                    }
                });
            };
        }

        /// <summary>
        /// Handles property changed events from the ViewModel, specifically for the Aliens collection.
        /// Recreates alien images when the Aliens collection changes.
        /// </summary>
        /// <param name="sender">The source of the event (GameStartPageViewModel).</param>
        /// <param name="e">The PropertyChangedEventArgs instance containing event data.</param>
        private void ViewModel_Aliens_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(GameStartPageViewModel.Aliens)) return;
            if (sender is GameStartPageViewModel viewModel)
            {
                CreateAlienImages(viewModel);
            }
        }

        /// <summary>
        /// Handles property changed events from the ViewModel, specifically for the SpecialAlien property.
        /// Creates or removes the special alien image based on the SpecialAlien property's value.
        /// </summary>
        /// <param name="sender">The source of the event (GameStartPageViewModel).</param>
        /// <param name="e">The PropertyChangedEventArgs instance containing event data.</param>
        private void ViewModel_SpecialAlien_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(GameStartPageViewModel.SpecialAlien)) return;
            if (sender is not GameStartPageViewModel viewModel) return;

            DispatcherQueue.TryEnqueue(() =>
            {
                if (viewModel.SpecialAlien != null)
                {
                    // Special alien appeared, create its image
                    CreateSpecialAlienImage(viewModel);
                }
                else
                {
                    // Special alien disappeared, remove its image
                    if (_specialAlienImage != null)
                    {
                        GameCanvas.Children.Remove(_specialAlienImage);
                        _specialAlienImage = null;
                    }
                }
            });
        }

        private void GameStartPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus(FocusState.Programmatic);

            DispatcherQueue.TryEnqueue(() =>
            {
                if (DataContext is GameStartPageViewModel viewModel)
                {
                    UpdatePlayerPosition();
                    CreateShieldImages();
                    viewModel.GenerateAliens(); // Call GenerateAliens here
                }

                _gameTimer = new DispatcherTimer();
                _gameTimer.Tick += GameTimer_Tick;
                _gameTimer.Interval = TimeSpan.FromMilliseconds(GameConstants.GameTimerIntervalMs); // Approx. 60 FPS
                _gameTimer.Start();
            });
        }
        
        private void GameStartPage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_gameTimer == null) return;
            
            _gameTimer.Stop();
            _gameTimer.Tick -= GameTimer_Tick;
        }

        private void GameTimer_Tick(object? sender, object? e)
        {
            if (DataContext is not GameStartPageViewModel viewModel) return;

            var projectilesToRemove = new List<Projectile>();
            var imagesToRemove = new List<Image>();

            for (var i = _projectileImages.Count - 1; i >= 0; i--)
            {
                var projectile = viewModel.Player.Projectiles[i];
                var projectileImage = _projectileImages[i];

                projectile.Move();
                projectile.CheckBounds(GameConstants.ScreenBoundaryMin); // Check if projectile is off-screen
                Canvas.SetTop(projectileImage, projectile.Y);

                if (!projectile.IsVisible)
                {
                    projectilesToRemove.Add(projectile);
                    imagesToRemove.Add(projectileImage);
                    continue; // Skip collision check if already off-screen
                }

                // Collision detection with special alien
                if (viewModel.SpecialAlien != null && projectile.CheckCollision(viewModel.SpecialAlien))
                {
                    projectile.IsVisible = false;
                    viewModel.Player.Score += viewModel.SpecialAlien.ScoreValue; // Update score with variable value
                    _soundService?.PlaySound(SoundPaths.Explosion); // Play explosion sound for special alien
                    viewModel.SpecialAlien = null; // Remove special alien from game by setting to null
                    
                    projectilesToRemove.Add(projectile);
                    imagesToRemove.Add(projectileImage);
                    continue; // Projectile hit special alien, no need to check other aliens
                }

                // Collision detection with aliens
                for (var j = viewModel.Aliens.Count - 1; j >= 0; j--)
                {
                    var alien = viewModel.Aliens[j];
                    if (projectile.CheckCollision(alien))
                    {
                        projectile.IsVisible = false;
                        alien.IsVisible = false;
                        viewModel.Player.Score += alien.ScoreValue; // Update score

                        // Play enemy death sound with cooldown
                        if (_canPlayEnemyDeathSound && _soundService != null)
                        {
                            var random = new Random();
                            var soundIndex = random.Next(SoundPaths.EnemyDie.Count);
                            var soundPath = SoundPaths.EnemyDie[soundIndex];
                            _soundService.PlaySound(soundPath);
                            _canPlayEnemyDeathSound = false;
                            _enemyDeathSoundCooldownTimer.Start();
                        }

                        projectilesToRemove.Add(projectile);
                        imagesToRemove.Add(projectileImage);
                        break; // Projectile hit an alien, no need to check other aliens
                    }
                }

                // Collision detection with shields
                for (var j = _shields.Count - 1; j >= 0; j--)
                {
                    var shield = _shields[j];
                    var shieldImage = _shieldImages[j];

                    if (projectile.CheckCollision(shield))
                    {
                        projectile.IsVisible = false;
                        shield.Health -= projectile.Damage;
                        shieldImage.Opacity = (double)shield.Health / shield.MaxHealth;
                        if (shield.Health <= 0)
                        {
                            shield.IsVisible = false;
                        }
                        projectilesToRemove.Add(projectile);
                        imagesToRemove.Add(projectileImage);
                        break; // Projectile hit a shield, no need to check other shields
                    }
                }
            }

            // Remove projectiles
            foreach (var projectile in projectilesToRemove)
            {
                viewModel.Player.Projectiles.Remove(projectile);
            }

            for (var i = imagesToRemove.Count - 1; i >= 0; i--)
            {
                var image = imagesToRemove[i];
                GameCanvas.Children.Remove(image);
                _projectileImages.Remove(image);
            }

            var enemyProjectilesToRemove = new List<Projectile>();
            var enemyImagesToRemove = new List<Image>();

            for (var i = _enemyProjectileImages.Count - 1; i >= 0; i--)
            {
                var projectile = viewModel.EnemyProjectiles[i];
                var projectileImage = _enemyProjectileImages[i];

                projectile.Move(); // Moves downwards because speed is positive
                projectile.CheckBounds(viewModel.GameHeight); // Check if off-screen at the bottom
                Canvas.SetTop(projectileImage, projectile.Y);

                if (!projectile.IsVisible)
                {
                    enemyProjectilesToRemove.Add(projectile);
                    enemyImagesToRemove.Add(projectileImage);
                    continue;
                }

                // Collision with player
                if (projectile.CheckCollision(viewModel.Player))
                {
                    projectile.IsVisible = false;
                    viewModel.Player.Lives--;
                    _soundService?.PlaySound(SoundPaths.Explosion);
                    enemyProjectilesToRemove.Add(projectile);
                    enemyImagesToRemove.Add(projectileImage);
                    continue;
                }

                // Collision with shields
                for (var j = _shields.Count - 1; j >= 0; j--)
                {
                    var shield = _shields[j];
                    var shieldImage = _shieldImages[j];

                    if (projectile.CheckCollision(shield))
                    {
                        projectile.IsVisible = false;
                        shield.Health -= projectile.Damage;
                        shieldImage.Opacity = (double)shield.Health / shield.MaxHealth;
                        if (shield.Health <= 0)
                        {
                            shield.IsVisible = false;
                        }
                        enemyProjectilesToRemove.Add(projectile);
                        enemyImagesToRemove.Add(projectileImage);
                        break;
                    }
                }
            }

            // Remove enemy projectiles
            foreach (var projectile in enemyProjectilesToRemove)
            {
                viewModel.EnemyProjectiles.Remove(projectile);
            }

            for (var i = enemyImagesToRemove.Count - 1; i >= 0; i--)
            {
                var image = enemyImagesToRemove[i];
                GameCanvas.Children.Remove(image);
                _enemyProjectileImages.Remove(image);
            }

            // Remove aliens that are no longer visible
            var aliensToRemove = new List<Alien>();
            var alienImagesToRemove = new List<Image>();

            for (var i = _alienImages.Count - 1; i >= 0; i--)
            {
                var alien = viewModel.Aliens[i];
                var alienImage = _alienImages[i];

                if (!alien.IsVisible)
                {
                    aliensToRemove.Add(alien);
                    alienImagesToRemove.Add(alienImage);
                }
            }

            // Collision detection with shields
            for (var i = viewModel.Aliens.Count - 1; i >= 0; i--)
            {
                var alien = viewModel.Aliens[i];
                for (var j = _shields.Count - 1; j >= 0; j--)
                {
                    var shield = _shields[j];
                    var shieldImage = _shieldImages[j];
                    if (alien.CheckCollision(shield))
                    {
                        shield.Health -= alien.Health;
                        shieldImage.Opacity = (double)shield.Health / shield.MaxHealth;
                        if (shield.Health <= 0)
                        {
                            shield.IsVisible = false;
                        }
                        alien.IsVisible = false;
                        break; // Alien hit a shield, no need to check other shields
                    }
                }
            }

            foreach (var alien in aliensToRemove)
            {
                viewModel.Aliens.Remove(alien);
            }

            for (var i = alienImagesToRemove.Count - 1; i >= 0; i--)
            {
                var image = alienImagesToRemove[i];
                GameCanvas.Children.Remove(image);
                _alienImages.Remove(image);
            }

            // Reset CanShoot if no projectiles are left
            if (!viewModel.Player.Projectiles.Any())
            {
                viewModel.Player.CanShoot = true;
            }

            // Remove shields that are no longer visible
            var shieldsToRemove = new List<Shield>();
            var shieldImagesToRemove = new List<Image>();

            for (var i = _shieldImages.Count - 1; i >= 0; i--)
            {
                var shield = _shields[i];
                var shieldImage = _shieldImages[i];

                if (!shield.IsVisible)
                {
                    shieldsToRemove.Add(shield);
                    shieldImagesToRemove.Add(shieldImage);
                }
            }

            foreach (var shield in shieldsToRemove)
            {
                _shields.Remove(shield);
            }

            for (var i = shieldImagesToRemove.Count - 1; i >= 0; i--)
            {
                var image = shieldImagesToRemove[i];
                GameCanvas.Children.Remove(image);
                _shieldImages.Remove(image);
            }
            
            // Check for collisions between player and aliens
            for (var i = viewModel.Aliens.Count - 1; i >= 0; i--)
            {
                var alien = viewModel.Aliens[i];
                if (viewModel.Player.CheckCollision(alien))
                {
                    viewModel.Player.Lives--; // Decrement player's lives
                    alien.IsVisible = false; // Mark alien for removal
                }
            }
        }
        
        private void UpdatePlayerPosition()
        {
            if (DataContext is not GameStartPageViewModel viewModel || _playerImage is null) return;

            viewModel.Player.X = (viewModel.GameWidth / GameConstants.CenteringDivisor) - (_playerImage.Width / GameConstants.CenteringDivisor);
            viewModel.Player.Y = viewModel.GameHeight - _playerImage.Height - GameConstants.PlayerYOffsetFromBottom;
        }

        private void GameStartPage_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (DataContext is not GameStartPageViewModel viewModel) return;
            
            if (e.Key == VirtualKey.Space)
            {
                viewModel.FirePlayerWeaponCommand.Execute(null);
            }
            else
            {
                viewModel.HandleKeyDown(e.Key);
            }
        }

        private void GameStartPage_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (DataContext is GameStartPageViewModel viewModel)
            {
                viewModel.HandleKeyUp(e.Key);
            }
        }

        private void GameCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DataContext is GameStartPageViewModel viewModel)
            {
                viewModel.GameWidth = e.NewSize.Width;
                viewModel.GameHeight = e.NewSize.Height;
            }
        }
    }
}

