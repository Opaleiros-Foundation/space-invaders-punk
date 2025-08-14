using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Dispatching;
using SpaceInvaders.Interfaces.Services;
using SpaceInvaders.Models;
using SpaceInvaders.Utils;

namespace SpaceInvaders.Presentation;

public partial class ScoreViewModel : ObservableObject
{
    private readonly IScoreService _scoreService;
    private readonly IScoreCacheService _scoreCacheService;
    private const int PageSize = 25;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsLoaded))]
    private bool _isLoading;

    public bool IsLoaded => !_isLoading;

    [ObservableProperty]
    private IncrementalLoadingCollection<ScoreDisplayItem> _scores;

    public List<int> SkeletonItems { get; } = Enumerable.Range(1, 7).ToList();

    public ScoreViewModel(IScoreService scoreService, IScoreCacheService scoreCacheService)
    {
        _scoreService = scoreService;
        _scoreCacheService = scoreCacheService;
        _scores = new IncrementalLoadingCollection<ScoreDisplayItem>(async (page) => new List<ScoreDisplayItem>(), DispatcherQueue.GetForCurrentThread());
    }

    public async Task OnNavigatedTo(DispatcherQueue dispatcher)
    {
        IsLoading = true;

        // Ensure the cache is populated, waiting if necessary.
        await _scoreCacheService.PreloadScoresAsync();
        var cachedScores = _scoreCacheService.GetScores();

        // Map the cached models to display items.
        var initialItems = cachedScores.Select((score, index) => new ScoreDisplayItem
        {
            Rank = index + 1,
            PlayerName = score.Player?.Name ?? "Unknown Player",
            PlayerScore = score.PlayerScore,
            DateAchievedFormatted = score.DateAchieved.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
        }).ToList();

        // The delegate for fetching subsequent pages.
        Func<int, Task<List<ScoreDisplayItem>>> fetchMoreFunc = async (page) =>
        {
            var newScores = await _scoreService.GetScoresByPageAsync(page, PageSize);
            if (newScores == null || !newScores.Any()) return new List<ScoreDisplayItem>();

            var rankStart = (page - 1) * PageSize + 1;
            return newScores.Select((score, index) => new ScoreDisplayItem
            {
                Rank = rankStart + index,
                PlayerName = score.Player?.Name ?? "Unknown Player",
                PlayerScore = score.PlayerScore,
                DateAchievedFormatted = score.DateAchieved.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
            }).ToList();
        };

        // Create the collection with the pre-loaded data.
        Scores = new IncrementalLoadingCollection<ScoreDisplayItem>(initialItems, fetchMoreFunc, dispatcher);
        OnPropertyChanged(nameof(Scores));

        IsLoading = false;
    }
}
