using SpaceInvaders.Constants;
using Uno.Extensions.Navigation;
using SpaceInvaders.Services;

namespace SpaceInvaders.Presentation;

public partial class GameOverViewModel : ObservableObject
{
    private readonly INavigator _navigator;
    private readonly PlayerService _playerService;
    public ICommand GoToMain { get; }
    public ICommand PlayAgain { get; }

    [ObservableProperty]
    private Player _player;

    [ObservableProperty]
    private string _scoreText;

    public GameOverViewModel(INavigator navigator, PlayerService playerService)
    {
        _navigator = navigator;
        _playerService = playerService;
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
        _playerService.ResetPlayer();
        await _navigator.NavigateViewModelAsync<GameStartPageViewModel>(this, data: _playerService.CurrentPlayer, qualifier: Qualifiers.ClearBackStack);
    }
}
