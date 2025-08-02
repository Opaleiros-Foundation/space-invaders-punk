using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.System;
using Microsoft.UI.Dispatching;

namespace SpaceInvaders.Presentation;

public sealed partial class GameStartPage : Page
{
    private List<Image> alienImages = new List<Image>();

    public GameStartPage()
    {
        this.InitializeComponent();
        this.Loaded += GameStartPage_Loaded;
        this.DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        if (DataContext is GameStartPageViewModel viewModel)
        {
            CreateAlienImages(viewModel);
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }
    }

    private void CreateAlienImages(GameStartPageViewModel viewModel)
    {
        GameCanvas.Children.Clear(); 
        alienImages.Clear();

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
            alienImages.Add(image);

            // Subscribe to alien's position changes
            alien.PropertyChanged += (s, e) =>
            {
                this.DispatcherQueue.TryEnqueue(() =>
                {
                    if (e.PropertyName == nameof(alien.X))
                    {
                        Canvas.SetLeft(image, alien.X);
                    }
                    if (e.PropertyName == nameof(alien.Y))
                    {
                        Canvas.SetTop(image, alien.Y);
                    }
                    GameCanvas.InvalidateMeasure();
                    GameCanvas.UpdateLayout();
                });
            };
        }
    }

    private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(GameStartPageViewModel.Aliens))
        {
            if (sender is GameStartPageViewModel viewModel)
            {
                CreateAlienImages(viewModel);
            }
        }
    }

    private void GameStartPage_Loaded(object sender, RoutedEventArgs e)
    {
        RootGrid.SizeChanged += RootGrid_SizeChanged;
        UpdatePlayerPosition();
        this.Focus(FocusState.Programmatic);
    }

    private void RootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        UpdatePlayerPosition();
    }

    private void UpdatePlayerPosition()
    {
        if (this.DataContext is GameStartPageViewModel viewModel)
        {
            if (PlayerImage.ActualWidth == 0 || PlayerImage.ActualHeight == 0)
            {
                PlayerImage.Measure(new Windows.Foundation.Size(double.PositiveInfinity, double.PositiveInfinity));
                PlayerImage.Arrange(new Windows.Foundation.Rect(0, 0, PlayerImage.DesiredSize.Width, PlayerImage.DesiredSize.Height));
            }

            viewModel.Player.X = (RootGrid.ActualWidth / 2) - (PlayerImage.ActualWidth / 2);
            viewModel.Player.Y = RootGrid.ActualHeight - PlayerImage.ActualHeight - 20; // 20 pixels from bottom
        }
    }

    private void GameStartPage_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (this.DataContext is GameStartPageViewModel viewModel)
        {
            if (e.Key == VirtualKey.Space)
            {
                viewModel.FirePlayerWeaponCommand.Execute(null);
            }
        }
    }
}

