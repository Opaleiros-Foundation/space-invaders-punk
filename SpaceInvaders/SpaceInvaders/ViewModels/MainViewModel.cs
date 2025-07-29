namespace SpaceInvaders.Presentation;

public partial class MainViewModel : ObservableObject
{
    private INavigator _navigator;

    [ObservableProperty] private string? name;

    public MainViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator
    )
    {
        _navigator = navigator;
        
        GoToControllers = new AsyncRelayCommand(GoToControllersView);
    }
    
    public ICommand GoToControllers { get; }

    private async Task GoToControllersView()
    {
        await _navigator.NavigateViewModelAsync<ControllersViewModel>(this, data: new Player(Name!));
    }
}
