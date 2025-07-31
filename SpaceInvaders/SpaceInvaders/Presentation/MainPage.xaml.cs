using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using System;
using Windows.Media.Playback;
using Windows.Media.Core;

namespace SpaceInvaders.Presentation;

public sealed partial class MainPage : Page
{
  public MainPage()
  {
    this.InitializeComponent();
    this.Loaded += MainPage_Loaded;
    this.Unloaded += MainPage_Unloaded;
  }

  private void MainPage_Loaded(object sender, RoutedEventArgs e)
  {
      BackgroundMusicPlayer.MediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
      BackgroundMusicPlayer.MediaPlayer.Play();
  }

  private void MainPage_Unloaded(object sender, RoutedEventArgs e)
  {
      BackgroundMusicPlayer.MediaPlayer.Pause();
      BackgroundMusicPlayer.MediaPlayer.MediaEnded -= MediaPlayer_MediaEnded;
  }

  private void MediaPlayer_MediaEnded(MediaPlayer sender, object args)
  {
      sender.Position = TimeSpan.Zero;
      sender.Play();
  }

  protected override void OnNavigatedTo(NavigationEventArgs e)
  {
      base.OnNavigatedTo(e);
      // Music playback is now handled by Loaded event
  }

  protected override void OnNavigatedFrom(NavigationEventArgs e)
  {
      base.OnNavigatedFrom(e);
      // Music playback is now handled by Unloaded event
  }

  private void VolumeButton_Click(object sender, RoutedEventArgs e)
  {
      BackgroundMusicPlayer.MediaPlayer.IsMuted = !BackgroundMusicPlayer.MediaPlayer.IsMuted;
  }
}