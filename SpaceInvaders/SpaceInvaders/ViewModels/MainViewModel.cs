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
        Exit = new RelayCommand(ExitApp);
    }
    
    public ICommand GoToControllers { get; }
    public ICommand GoToScore { get; }
    public ICommand Exit { get; }

    private async Task GoToControllersView()
    {
        await _navigator.NavigateViewModelAsync<ControllersViewModel>(this);
    }

    private async Task GoToScoreView()
    {
        await _navigator.NavigateViewModelAsync<ScoreViewModel>(this);
    }

    private void ExitApp()
    {
        Application.Current.Exit();
    }
}
