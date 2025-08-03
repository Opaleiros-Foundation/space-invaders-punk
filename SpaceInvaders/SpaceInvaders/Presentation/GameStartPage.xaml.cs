using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.System;
using SpaceInvaders.Models;

namespace SpaceInvaders.Presentation
{
    public sealed partial class GameStartPage : Page
    {
        private readonly List<Image> _alienImages = new();
        private readonly List<Image> _projectileImages = new();
        private Image _playerImage;
        private DispatcherTimer _gameTimer;

        public GameStartPage()
        {
            InitializeComponent();
            Loaded += GameStartPage_Loaded;
            Unloaded += GameStartPage_Unloaded;
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (DataContext is not GameStartPageViewModel viewModel) return;

            CreatePlayerImage(viewModel);
            CreateAlienImages(viewModel);
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            viewModel.Player.Projectiles.CollectionChanged += Projectiles_CollectionChanged;
        }

        private void Projectiles_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
                            Height = 32,
                            Source = new BitmapImage(new Uri(projectile.SpritePath))
                        };
                        
                        var playerImageWidth = _playerImage?.Width ?? 64;
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
                Width = 64,
                Height = 64,
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
                    Width = 64,
                    Height = 64,
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

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(GameStartPageViewModel.Aliens)) return;
            if (sender is GameStartPageViewModel viewModel)
            {
                CreateAlienImages(viewModel);
            }
        }

        private void GameStartPage_Loaded(object sender, RoutedEventArgs e)
        {
            RootGrid.SizeChanged += RootGrid_SizeChanged;
            if (DataContext is GameStartPageViewModel viewModel)
            {
                viewModel.ScreenWidth = RootGrid.ActualWidth;
            }
            UpdatePlayerPosition();
            Focus(FocusState.Programmatic);

            _gameTimer = new DispatcherTimer();
            _gameTimer.Tick += GameTimer_Tick;
            _gameTimer.Interval = TimeSpan.FromMilliseconds(16); // Approx. 60 FPS
            _gameTimer.Start();
        }
        
        private void GameStartPage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_gameTimer == null) return;
            
            _gameTimer.Stop();
            _gameTimer.Tick -= GameTimer_Tick;
        }

        private void GameTimer_Tick(object sender, object e)
        {
            if (DataContext is not GameStartPageViewModel viewModel) return;

            var projectilesToRemove = new List<Projectile>();
            var imagesToRemove = new List<Image>();

            for (var i = _projectileImages.Count - 1; i >= 0; i--)
            {
                var projectile = viewModel.Player.Projectiles[i];
                var projectileImage = _projectileImages[i];

                projectile.Move();
                Canvas.SetTop(projectileImage, projectile.Y);

                if (projectile.Y < 0)
                {
                    projectilesToRemove.Add(projectile);
                    imagesToRemove.Add(projectileImage);
                }
            }

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
        }

        private void RootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DataContext is GameStartPageViewModel viewModel)
            {
                viewModel.ScreenWidth = e.NewSize.Width;
                UpdatePlayerPosition();
            }
        }

        private void UpdatePlayerPosition()
        {
            if (DataContext is not GameStartPageViewModel viewModel || _playerImage is null) return;

            viewModel.Player.X = (RootGrid.ActualWidth / 2) - (_playerImage.Width / 2);
            viewModel.Player.Y = RootGrid.ActualHeight - _playerImage.Height - 20;
        }

        private void GameStartPage_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (DataContext is not GameStartPageViewModel viewModel) return;
            
            if (e.Key == VirtualKey.Space)
            {
                viewModel.Player.Shoot();
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
    }
}

