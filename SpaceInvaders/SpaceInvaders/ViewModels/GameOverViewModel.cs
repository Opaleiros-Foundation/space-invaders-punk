using SpaceInvaders.Constants;
using Uno.Extensions.Navigation;
using SpaceInvaders.Services;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using SpaceInvaders.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Uno.Extensions.DependencyInjection;
using System.Diagnostics;
using Microsoft.UI.Xaml;

namespace SpaceInvaders.Presentation;

/// <summary>
/// ViewModel for the Game Over screen, handling score display, saving, and navigation options.
/// </summary>
public partial class GameOverViewModel : ObservableObject
{
    private readonly INavigator _navigator;
    private readonly IPlayerService _playerService;
    private readonly IScoreService _scoreService;
    private readonly IScoreCacheService _scoreCacheService;

    /// <summary>
    /// Gets the command to navigate to the main menu.
    /// </summary>
    public ICommand GoToMain { get; }

    /// <summary>
    /// Gets the command to restart the game.
    /// </summary>
    public ICommand PlayAgain { get; }

    /// <summary>
    /// Gets the command to save the player's score.
    /// </summary>
    public ICommand SaveScoreCommand { get; }

    /// <summary>
    /// Gets or sets the player object associated with this game over state.
    /// </summary>
    [ObservableProperty]
    private Player _player;

    /// <summary>
    /// Gets or sets the formatted score text to display.
    /// </summary>
    [ObservableProperty]
    private string _scoreText;

    /// <summary>
    /// Gets or sets the player's name for saving the score.
    /// </summary>
    [ObservableProperty]
    private string _playerName;

    /// <summary>
    /// Gets or sets a confirmation message to display after saving the score.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ConfirmationVisibility))]
    private string _confirmationMessage;

    public Visibility ConfirmationVisibility => IsConfirmationVisible ? Visibility.Visible : Visibility.Collapsed;

    public bool IsConfirmationVisible => !string.IsNullOrEmpty(ConfirmationMessage);

    /// <summary>
    /// Initializes a new instance of the <see cref="GameOverViewModel"/> class.
    /// </summary>
    /// <param name="navigator">The navigation service.</param>
    /// <param name="playerService">The player service for managing player data.</param>
    /// <param name="scoreService">The score service for managing scores.</param>
    /// <param name="scoreCacheService">The score cache service.</param>
    /// <param name="player">The player object from the completed game.</param>
    public GameOverViewModel(INavigator navigator, IPlayerService playerService, IScoreService scoreService, IScoreCacheService scoreCacheService, Player player)
    {
        _navigator = navigator;
        _playerService = playerService;
        _scoreService = scoreService;
        _scoreCacheService = scoreCacheService;
        GoToMain = new AsyncRelayCommand(GoToMainView);
        PlayAgain = new AsyncRelayCommand(PlayAgainView);
        SaveScoreCommand = new AsyncRelayCommand(SaveScore);

        _player = player;
    }

    /// <summary>
    /// Called when the Player property changes, updates the displayed score text.
    /// </summary>
    /// <param name="value">The new Player value.</param>
    partial void OnPlayerChanged(Player value)
    {
        ScoreText = $"SCORE: {value.Score}";
    }

    /// <summary>
    /// Navigates to the main menu asynchronously.
    /// </summary>
    private async Task GoToMainView()
    {
        await _navigator.NavigateViewModelAsync<MainViewModel>(this, qualifier: Qualifiers.ClearBackStack);
    }

    /// <summary>
    /// Restarts the game by navigating to the game start page with a new player instance asynchronously.
    /// </summary>
    private async Task PlayAgainView()
    {
        var newPlayer = new Player("Player 1", 100, new Weapon(10, 1.0, SpritePaths.Projectile), 32, 32);
        await _navigator.NavigateViewModelAsync<GameStartPageViewModel>(this, data: newPlayer, qualifier: Qualifiers.ClearBackStack);
    }

    /// <summary>
    /// Saves the player's score asynchronously.
    /// Updates the player's name if provided, and adds a new score record.
    /// </summary>
    private async Task SaveScore()
    {
        Debug.WriteLine("[DEBUG] Attempting to save score...");
        try
        {
            _player.Name = PlayerName; // Update player name from input
            Debug.WriteLine($"[DEBUG] Player name set to: {PlayerName}");

            if (_player.Id == 0)
            {
                Debug.WriteLine("[DEBUG] New player detected. Adding to database...");
                await _playerService.AddPlayerAsync(_player);
                Debug.WriteLine($"[DEBUG] Player added with new ID: {_player.Id}");
            }
            else
            {
                Debug.WriteLine($"[DEBUG] Existing player detected (ID: {_player.Id}). Updating...");
                await _playerService.UpdatePlayerAsync(_player);
                Debug.WriteLine("[DEBUG] Player updated.");
            }

            // Save the score
            var newScore = new Score
            {
                PlayerScore = _player.Score,
                DateAchieved = DateTime.UtcNow,
                PlayerId = _player.Id // Link to the player
            };
            Debug.WriteLine($"[DEBUG] Creating new score record with PlayerId: {newScore.PlayerId} and Score: {newScore.PlayerScore}");
            await _scoreService.AddScoreAsync(newScore);
            Debug.WriteLine("[DEBUG] Score record saved to database.");

            // Invalidate the cache so the new score is reflected next time.
            _scoreCacheService.InvalidateCache();
            Debug.WriteLine("[DEBUG] Score cache invalidated.");

            ConfirmationMessage = $"Score de {_player.Score} salvo com sucesso para {_player.Name}!";
            Debug.WriteLine("[DEBUG] Confirmation message set.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[ERROR] Failed to save score: {ex.Message}");
            Debug.WriteLine(ex.StackTrace);
            ConfirmationMessage = "Error: Could not save score.";
        }
    }
}
