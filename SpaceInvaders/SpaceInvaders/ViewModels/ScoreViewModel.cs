using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceInvaders.Interfaces.Services;
using SpaceInvaders.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;

namespace SpaceInvaders.Presentation;

public partial class ScoreViewModel : ObservableObject
{
    private readonly IScoreService _scoreService;

    [ObservableProperty]
    private ObservableCollection<ScoreDisplayItem> _scores;

    public ScoreViewModel(IScoreService scoreService)
    {
        _scoreService = scoreService;
        _scores = new ObservableCollection<ScoreDisplayItem>();
        LoadScoresCommand = new AsyncRelayCommand(LoadScoresAsync);
    }

    public IAsyncRelayCommand LoadScoresCommand { get; }

    private async Task LoadScoresAsync()
    {
        var allScores = (await _scoreService.GetAllScoresAsync())
            .OrderByDescending(s => s.PlayerScore)
            .ToList();

        Scores.Clear();
        for (int i = 0; i < allScores.Count; i++)
        {
            var score = allScores[i];
            Scores.Add(new ScoreDisplayItem
            {
                Rank = i + 1,
                PlayerName = score.Player?.Name ?? "Unknown Player",
                PlayerScore = score.PlayerScore,
                DateAchievedFormatted = score.DateAchieved.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
            });
        }
    }

    public async Task OnNavigatedTo()
    {
        await LoadScoresCommand.ExecuteAsync(null);
    }
}
