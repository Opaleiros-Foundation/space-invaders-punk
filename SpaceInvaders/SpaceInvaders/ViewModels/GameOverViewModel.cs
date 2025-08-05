using SpaceInvaders.Constants;
using Uno.Extensions.Navigation;
using SpaceInvaders.Services;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Threading.Tasks;

namespace SpaceInvaders.Presentation;

public partial class GameOverViewModel : ObservableObject
{
    private readonly INavigator _navigator;
    private readonly PlayerService _playerService;
    public ICommand GoToMain { get; }
    public ICommand PlayAgain { get; }
    public ICommand SaveScoreCommand { get; }

    [ObservableProperty]
    private Player _player;

    [ObservableProperty]
    private string _scoreText;

    [ObservableProperty]
    private string _playerName;

    public  GameOverViewModel(INavigator navigator, PlayerService playerService)
    {
        _navigator = navigator;
        _playerService = playerService;
        GoToMain = new AsyncRelayCommand(GoToMainView);
        PlayAgain = new AsyncRelayCommand(PlayAgainView);
        SaveScoreCommand = new AsyncRelayCommand(SaveScore);

        PlayerName = _playerService.CurrentPlayer.Name; // Pre-fill with current player name if available
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

    private async Task SaveScore()
    {
        _playerService.SetPlayerName(PlayerName);
        // TODO: Implement actual score saving logic here
        // For now, just navigate back to main menu or show a confirmation
        await _navigator.NavigateViewModelAsync<MainViewModel>(this, qualifier: Qualifiers.ClearBackStack);
    }
}
