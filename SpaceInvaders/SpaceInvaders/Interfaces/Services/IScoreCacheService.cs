using System.Collections.Generic;
using System.Threading.Tasks;
using SpaceInvaders.Models;

namespace SpaceInvaders.Interfaces.Services;

public interface IScoreCacheService
{
    bool IsLoading { get; }
    bool IsCacheReady { get; }
    List<Score> GetScores();
    Task PreloadScoresAsync();
    void InvalidateCache();
}
