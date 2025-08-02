using System;
using System.Collections.Generic;
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
        private Image _playerImage;

        public GameStartPage()
        {
            InitializeComponent();
            Loaded += GameStartPage_Loaded;
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (DataContext is not GameStartPageViewModel viewModel) return;
            
            CreatePlayerImage(viewModel);
            CreateAlienImages(viewModel);
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
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
            UpdatePlayerPosition();
            Focus(FocusState.Programmatic);
        }

        private void RootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdatePlayerPosition();
        }

        private void UpdatePlayerPosition()
        {
            if (DataContext is not GameStartPageViewModel viewModel || _playerImage is null) return;
            
            viewModel.Player.X = (RootGrid.ActualWidth / 2) - (_playerImage.Width / 2);
            viewModel.Player.Y = RootGrid.ActualHeight - _playerImage.Height - 20;
        }

        private void GameStartPage_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (DataContext is GameStartPageViewModel viewModel)
            {
                viewModel.HandleKeyDown(e.Key);
            }
        }
    }
}

