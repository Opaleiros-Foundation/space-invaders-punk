using SpaceInvaders.Constants;
using SpaceInvaders.Services;
using SpaceInvaders.Models;
using SpaceInvaders.Interfaces.Services;

namespace SpaceInvaders.Presentation;

/// <summary>
/// ViewModel for the main menu of the game, providing navigation to other sections.
/// </summary>
public partial class MainViewModel : ObservableObject
{
    private INavigator _navigator;
    private IPlayerService _playerService;
    private IScoreCacheService _scoreCacheService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    /// <param name="localizer">The string localizer service.</param>
    /// <param name="appInfo">Application configuration options.</param>
    /// <param name="navigator">The navigation service.</param>
    /// <param name="playerService">The player service.</param>
    /// <param name="scoreCacheService">The score cache service.</param>
    public MainViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        IPlayerService playerService,
        IScoreCacheService scoreCacheService
    )
    {
        _navigator = navigator;
        _playerService = playerService;
        _scoreCacheService = scoreCacheService;

        // Preload scores in the background
        _ = _scoreCacheService.PreloadScoresAsync();
        
        GoToControllers = new AsyncRelayCommand(GoToControllersView);
        GoToScore = new AsyncRelayCommand(GoToScoreView);
        GoToGameStart = new AsyncRelayCommand(GoToGameStartView);
        Exit = new RelayCommand(ExitApp);
    }
    
    /// <summary>
    /// Gets the command to navigate to the controllers page.
    /// </summary>
    public ICommand GoToControllers { get; }
    /// <summary>
    /// Gets the command to navigate to the score page.
    /// </summary>
    public ICommand GoToScore { get; }
    /// <summary>
    /// Gets the command to exit the application.
    /// </summary>
    public ICommand Exit { get; }
    /// <summary>
    /// Gets the command to navigate to the game start page.
    /// </summary>
    public ICommand GoToGameStart { get; }

    /// <summary>
    /// Navigates to the controllers view asynchronously.
    /// </summary>
    private async Task GoToControllersView()
    {
        await _navigator.NavigateViewModelAsync<ControllersViewModel>(this);
    }

    /// <summary>
    /// Navigates to the score view asynchronously.
    /// </summary>
    private async Task GoToScoreView()
    {
        await _navigator.NavigateViewModelAsync<ScoreViewModel>(this);
    }

    /// <summary>
    /// Navigates to the game start view asynchronously, initializing a new player.
    /// </summary>
    private async Task GoToGameStartView()
    {
        var newPlayer = new Player("Player 1", 100, new Weapon(10, 1.0, SpritePaths.Projectile), 32, 32);
        await _navigator.NavigateViewModelAsync<GameStartPageViewModel>(this, data: newPlayer);
    }
    
    /// <summary>
    /// Exits the application.
    /// </summary>
    private void ExitApp()
    {
        Application.Current.Exit();
    }
}
