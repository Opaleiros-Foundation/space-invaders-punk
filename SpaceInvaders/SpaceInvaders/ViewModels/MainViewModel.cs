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
        await _navigator.NavigateViewModelAsync<GameStartPage>(this);
    }
    
    private void ExitApp()
    {
        Application.Current.Exit();
    }
}
