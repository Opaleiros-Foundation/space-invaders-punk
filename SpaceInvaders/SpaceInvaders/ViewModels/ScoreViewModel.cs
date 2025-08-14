using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceInvaders.Interfaces.Services;
using SpaceInvaders.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;

namespace SpaceInvaders.Presentation;

/// <summary>
/// ViewModel for the Score page, responsible for loading and displaying high scores.
/// </summary>
public partial class ScoreViewModel : ObservableObject
{
    private readonly IScoreService _scoreService;

    /// <summary>
    /// Gets or sets the observable collection of scores to be displayed.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<ScoreDisplayItem> _scores;

    private const int PageSize = 25;
    private int _currentPage = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScoreViewModel"/> class.
    /// </summary>
    /// <param name="scoreService">The score service for retrieving scores.</param>
    public ScoreViewModel(IScoreService scoreService)
    {
        _scoreService = scoreService;
        _scores = new ObservableCollection<ScoreDisplayItem>();
        LoadScoresCommand = new AsyncRelayCommand(LoadScoresAsync);
    }

    /// <summary>
    /// Gets the asynchronous command to load scores.
    /// </summary>
    public IAsyncRelayCommand LoadScoresCommand { get; }

    /// <summary>
    /// Asynchronously loads scores from the score service, orders them, and populates the Scores collection.
    /// </summary>
    private async Task LoadScoresAsync()
    {
        Scores.Clear();
        _currentPage = 1;

        var newScores = await _scoreService.GetScoresByPageAsync(_currentPage, PageSize);

        var rankStart = (_currentPage - 1) * PageSize + 1;
        foreach (var score in newScores)
        {
            Scores.Add(new ScoreDisplayItem
            {
                Rank = rankStart++,
                PlayerName = score.Player?.Name ?? "Unknown Player",
                PlayerScore = score.PlayerScore,
                DateAchievedFormatted = score.DateAchieved.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
            });
        }
    }

    /// <summary>
    /// Called when the ViewModel is navigated to, triggers the loading of scores.
    /// </summary>
    public async Task OnNavigatedTo()
    {
        await LoadScoresCommand.ExecuteAsync(null);
    }
}
