using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using System;
using Windows.Media.Playback;
using Windows.Media.Core;

namespace SpaceInvaders.Presentation;

public sealed partial class MainPage : Page
{
  private DispatcherTimer _musicLoopTimer;

  public MainPage()
  {
    this.InitializeComponent();
    this.Loaded += MainPage_Loaded;
    this.Unloaded += MainPage_Unloaded;
  }

  private void MainPage_Loaded(object sender, RoutedEventArgs e)
  {
      BackgroundMusicPlayer.MediaPlayer.Play();

      _musicLoopTimer = new DispatcherTimer();
      _musicLoopTimer.Interval = TimeSpan.FromSeconds(1);
      _musicLoopTimer.Tick += MusicLoopTimer_Tick;
      _musicLoopTimer.Start();
  }

  private void MainPage_Unloaded(object sender, RoutedEventArgs e)
  {
      BackgroundMusicPlayer.MediaPlayer.Pause();
      _musicLoopTimer.Stop();
      _musicLoopTimer.Tick -= MusicLoopTimer_Tick;
  }

  private void MusicLoopTimer_Tick(object sender, object e)
  {
      if (BackgroundMusicPlayer.MediaPlayer.PlaybackSession.NaturalDuration.TotalSeconds > 0 &&
          BackgroundMusicPlayer.MediaPlayer.PlaybackSession.Position.TotalSeconds >= BackgroundMusicPlayer.MediaPlayer.PlaybackSession.NaturalDuration.TotalSeconds - 1)
      {
          BackgroundMusicPlayer.MediaPlayer.Position = TimeSpan.Zero;
          BackgroundMusicPlayer.MediaPlayer.Play();
      }
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
}