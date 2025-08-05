using SpaceInvaders.Constants;
using Uno.Extensions.Navigation;

using Uno.Extensions.Navigation;

namespace SpaceInvaders.Presentation;

public partial class GameOverViewModel : ObservableObject
{
    private readonly INavigator _navigator;
    public ICommand GoToMain { get; }
    public ICommand PlayAgain { get; }

    [ObservableProperty]
    private Player _player;

    [ObservableProperty]
    private string _scoreText;

    public GameOverViewModel(INavigator navigator)
    {
        _navigator = navigator;
        GoToMain = new AsyncRelayCommand(GoToMainView);
        PlayAgain = new AsyncRelayCommand(PlayAgainView);
    }

    partial void OnPlayerChanged(Player value)
    {
        ScoreText = $"SCORE: {value.Score}";
    }

    private async Task GoToMainView()
    {
        await _navigator.NavigateViewModelAsync<MainViewModel>(this, qualifier: Qualifiers.ClearBackStack);
    }

    private async Task PlayAgainView()
    {
        var newPlayer = new Player("Player1", 100, new Weapon(10, 0.5, SpritePaths.Projectile), 64, 64);
        await _navigator.NavigateViewModelAsync<GameStartPageViewModel>(this, data: newPlayer, qualifier: Qualifiers.ClearBackStack);
    }
}
