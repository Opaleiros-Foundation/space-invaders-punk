using SpaceInvaders.Constants;

namespace SpaceInvaders.Presentation;

public partial class MainViewModel : ObservableObject
{
    private INavigator _navigator;
    
    public MainViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator
    )
    {
        _navigator = navigator;
        
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
        var player = new Player("Player1", 100, new Weapon(10, 0.5, SpritePaths.Projectile));
        await _navigator.NavigateViewModelAsync<GameStartPageViewModel>(this, data: player);
    }
    
    private void ExitApp()
    {
        Application.Current.Exit();
    }
}
