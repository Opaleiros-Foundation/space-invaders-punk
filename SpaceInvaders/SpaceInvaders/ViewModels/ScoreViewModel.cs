using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceInvaders.Interfaces.Services;
using SpaceInvaders.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SpaceInvaders.Presentation;

public partial class ScoreViewModel : ObservableObject
{
    private readonly IScoreService _scoreService;

    [ObservableProperty]
    private ObservableCollection<Score> _scores;

    public ScoreViewModel(IScoreService scoreService)
    {
        _scoreService = scoreService;
        _scores = new ObservableCollection<Score>();
        LoadScoresCommand = new AsyncRelayCommand(LoadScoresAsync);
    }

    public IAsyncRelayCommand LoadScoresCommand { get; }

    private async Task LoadScoresAsync()
    {
        var allScores = await _scoreService.GetAllScoresAsync();
        Scores.Clear();
        foreach (var score in allScores)
        {
            Scores.Add(score);
        }
    }

    public async Task OnNavigatedTo()
    {
        await LoadScoresCommand.ExecuteAsync(null);
    }
}
