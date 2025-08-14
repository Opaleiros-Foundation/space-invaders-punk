using System.Collections.Generic;
using System.Threading.Tasks;
using SpaceInvaders.Models;

namespace SpaceInvaders.Interfaces.Services;

/// <summary>
/// Defines the methods for the score management service.
/// </summary>
public interface IScoreService
{
    /// <summary>
    /// Asynchronously adds a new score.
    /// </summary>
    /// <param name="score">The Score object to be added.</param>
    /// <returns>A task that represents the asynchronous operation, containing the added score.</returns>
    Task<Score> AddScoreAsync(Score score);
    /// <summary>
    /// Asynchronously retrieves the top scores.
    /// </summary>
    /// <param name="count">The number of top scores to return.</param>
    /// <returns>A task that represents the asynchronous operation, containing a list of the top scores.</returns>
    Task<List<Score>> GetTopScoresAsync(int count);
    /// <summary>
    /// Asynchronously retrieves all scores.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing a list of all scores.</returns>
    Task<List<Score>> GetAllScoresAsync();

    /// <summary>
    /// Asynchronously retrieves a paginated list of scores.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of scores per page.</param>
    /// <returns>A task that represents the asynchronous operation, containing a list of scores for the specified page.</returns>
    Task<List<Score>> GetScoresByPageAsync(int pageNumber, int pageSize);
}
