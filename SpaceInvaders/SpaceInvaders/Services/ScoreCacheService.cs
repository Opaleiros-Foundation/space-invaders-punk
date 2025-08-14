using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceInvaders.Interfaces.Services;
using SpaceInvaders.Models;

namespace SpaceInvaders.Services;

/// <summary>
/// Provides caching for game scores to improve performance and reduce database load.
/// </summary>
public class ScoreCacheService : IScoreCacheService
{
    private readonly IScoreService _scoreService;
    private List<Score> _cachedScores = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    /// <summary>
    /// Gets a value indicating whether the cache is currently loading scores.
    /// </summary>
    public bool IsLoading { get; private set; }
    /// <summary>
    /// Gets a value indicating whether the cache has been populated with scores.
    /// </summary>
    public bool IsCacheReady { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScoreCacheService"/> class.
    /// </summary>
    /// <param name="scoreService">The score service used to fetch data from the database.</param>
    public ScoreCacheService(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    /// <summary>
    /// Retrieves the currently cached list of scores.
    /// </summary>
    /// <returns>A list of cached <see cref="Score"/> objects.</returns>
    public List<Score> GetScores()
    {
        return _cachedScores;
    }

    /// <summary>
    /// Asynchronously preloads scores into the cache. This method is idempotent and thread-safe.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
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

    /// <summary>
    /// Invalidates the current cache, marking it as not ready and clearing stored scores.
    /// </summary>
    public void InvalidateCache()
    {
        IsCacheReady = false;
        _cachedScores.Clear();
    }
}
