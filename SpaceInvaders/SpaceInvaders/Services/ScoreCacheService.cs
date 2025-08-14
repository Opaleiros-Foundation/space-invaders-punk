using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceInvaders.Interfaces.Services;
using SpaceInvaders.Models;

namespace SpaceInvaders.Services;

public class ScoreCacheService : IScoreCacheService
{
    private readonly IScoreService _scoreService;
    private List<Score> _cachedScores = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public bool IsLoading { get; private set; }
    public bool IsCacheReady { get; private set; }

    public ScoreCacheService(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    public List<Score> GetScores()
    {
        return _cachedScores;
    }

    public async Task PreloadScoresAsync()
    {
        if (IsCacheReady || IsLoading) return;

        await _semaphore.WaitAsync();
        try
        {
            if (IsCacheReady || IsLoading) return;

            IsLoading = true;
            var scores = await _scoreService.GetAllScoresAsync();
            _cachedScores = scores.OrderByDescending(s => s.PlayerScore).ToList();
            IsCacheReady = true;
        }
        finally
        {
            IsLoading = false;
            _semaphore.Release();
        }
    }

    public void InvalidateCache()
    {
        IsCacheReady = false;
        _cachedScores.Clear();
    }
}
