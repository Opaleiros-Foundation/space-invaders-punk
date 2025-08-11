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
        private readonly List<Image> _shieldImages = new();
        private readonly List<Shield> _shields = new();
        private Image? _playerImage;
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
            _enemyDeathSoundCooldownTimer.Interval = TimeSpan.FromMilliseconds(50); // Cooldown period for enemy death sound
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
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            viewModel.Player.Projectiles.CollectionChanged += Projectiles_CollectionChanged;
            viewModel.Aliens.CollectionChanged += Aliens_CollectionChanged;
        }

        private void Aliens_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                if (DataContext is GameStartPageViewModel viewModel)
                {
                    CreateAlienImages(viewModel);
                }
            });
        }

        private void CreateShieldImages()
        {
            if (DataContext is not GameStartPageViewModel viewModel) return;

            var screenWidth = viewModel.GameWidth;
            var shieldWidth = 32;
            var spacing = (screenWidth - (4 * shieldWidth)) / 5;

            for (var i = 0; i < 4; i++)
            {
                var shield = new Shield("Shield", 100, shieldWidth, 32);
                var shieldImage = new Image
                {
                    Width = 32,
                    Height = 32,
                    Source = new BitmapImage(new Uri(shield.SpritePath))
                };

                shield.X = spacing * (i + 1) + (shield.Width * i);
                shield.Y = viewModel.GameHeight - 200;

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
                            Width = 16,
                            Height = 16,
                            Source = new BitmapImage(new Uri(projectile.SpritePath))
                        };
                        
                        double playerImageWidth = 64;
                        if (_playerImage != null)
                        {
                            playerImageWidth = _playerImage.Width;
                        }
                        projectile.X = projectile.X + (playerImageWidth / 2) - (projectileImage.Width / 2);
                        projectile.Y -= 30;

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
                Width = 32,
                Height = 32,
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
                    Width = 32,
                    Height = 32,
                    Source = new BitmapImage(new Uri(alien.SpritePath))
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

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(GameStartPageViewModel.Aliens)) return;
            if (sender is GameStartPageViewModel viewModel)
            {
                CreateAlienImages(viewModel);
            }
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
                _gameTimer.Interval = TimeSpan.FromMilliseconds(45); // Approx. 60 FPS
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
                projectile.CheckBounds(0); // Check if projectile is off-screen
                Canvas.SetTop(projectileImage, projectile.Y);

                if (!projectile.IsVisible)
                {
                    projectilesToRemove.Add(projectile);
                    imagesToRemove.Add(projectileImage);
                    continue; // Skip collision check if already off-screen
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

            viewModel.Player.X = (viewModel.GameWidth / 2) - (_playerImage.Width / 2);
            viewModel.Player.Y = viewModel.GameHeight - _playerImage.Height - 50;
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

