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


using Microsoft.UI.Xaml.Controls;
using Windows.System;

namespace SpaceInvaders.Presentation;

public sealed partial class GameStartPage : Page
{
    public GameStartPage()
    {
        this.InitializeComponent();
        this.Loaded += GameStartPage_Loaded;
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

