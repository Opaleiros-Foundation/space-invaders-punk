using SpaceInvaders.Constants;
using SpaceInvaders.Services;

namespace SpaceInvaders.Presentation;

public partial class MainViewModel : ObservableObject
{
    private INavigator _navigator;
    private PlayerService _playerService;
    
    public MainViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        PlayerService playerService
    )
    {
        _navigator = navigator;
        _playerService = playerService;
        
        GoToControllers = new AsyncRelayCommand(GoToControllersView);
        GoToScore = new AsyncRelayCommand(GoToScoreView);
        GoToGameStart = new AsyncRelayCommand(GoToGameStartView);
        Exit = new RelayCommand(ExitApp);
    }
    
    public ICommand GoToControllers { get; }
    public ICommand GoToScore { get; }
    public ICommand Exit { get; }
    public ICommand GoToGameStart { get; }

    private async Task GoToControllersView()
    {
        await _navigator.NavigateViewModelAsync<ControllersViewModel>(this);
    }

    private async Task GoToScoreView()
    {
        await _navigator.NavigateViewModelAsync<ScoreViewModel>(this);
    }

    private async Task GoToGameStartView()
    {
        _playerService.ResetPlayer();
        await _navigator.NavigateViewModelAsync<GameStartPageViewModel>(this, data: _playerService.CurrentPlayer);
    }
    
    private void ExitApp()
    {
        Application.Current.Exit();
    }
}
