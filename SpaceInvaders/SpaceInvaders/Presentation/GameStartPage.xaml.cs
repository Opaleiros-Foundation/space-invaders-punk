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


using Microsoft.UI.Xaml;

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
        if (this.DataContext is GameStartPageViewModel viewModel)
        {
            viewModel.PlayerX = (RootGrid.ActualWidth / 2) - (PlayerImage.ActualWidth / 2);
            viewModel.PlayerY = RootGrid.ActualHeight - PlayerImage.ActualHeight - 20; // 20 pixels from bottom
        }
    }
}

