using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpaceInvaders.Data;
using SpaceInvaders.Interfaces.Services;
using SpaceInvaders.Models;

namespace SpaceInvaders.Services;

/// <summary>
/// Provides services for managing game scores in the database.
/// </summary>
public class ScoreService : IScoreService
{
    private readonly SpaceInvadersDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScoreService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public ScoreService(SpaceInvadersDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new score to the database asynchronously.
    /// </summary>
    /// <param name="score">The score object to add.</param>
    /// <returns>A task that represents the asynchronous operation, containing the added score.</returns>
    public async Task<Score> AddScoreAsync(Score score)
    {
        _context.Scores.Add(score);
        await _context.SaveChangesAsync();
        return score;
    }

    /// <summary>
    /// Retrieves a specified number of top scores from the database asynchronously.
    /// </summary>
    /// <param name="count">The maximum number of top scores to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing a list of top scores.</returns>
    public async Task<List<Score>> GetTopScoresAsync(int count)
    {
        return await _context.Scores
            .OrderByDescending(s => s.PlayerScore)
            .Take(count)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves all scores from the database asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing a list of all scores.</returns>
    public async Task<List<Score>> GetAllScoresAsync()
    {
        return await _context.Scores.Include(s => s.Player).ToListAsync();
    }

    /// <summary>
    /// Retrieves a paginated list of scores from the database asynchronously.
    /// </summary>
    /// <param name="pageNumber">The 1-based page number to retrieve.</param>
    /// <param name="pageSize">The number of scores per page.</param>
    /// <returns>A task that represents the asynchronous operation, containing a list of scores for the specified page.</returns>
    public async Task<List<Score>> GetScoresByPageAsync(int pageNumber, int pageSize)
    {
        return await _context.Scores
            .Include(s => s.Player)
            .OrderByDescending(s => s.PlayerScore)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
