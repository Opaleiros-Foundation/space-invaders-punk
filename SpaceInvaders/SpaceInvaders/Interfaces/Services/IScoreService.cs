using System.Collections.Generic;
using System.Threading.Tasks;
using SpaceInvaders.Models;

namespace SpaceInvaders.Interfaces.Services;

public interface IScoreService
{
    Task<Score> AddScoreAsync(Score score);
    Task<List<Score>> GetTopScoresAsync(int count);
    Task<List<Score>> GetAllScoresAsync();
}
