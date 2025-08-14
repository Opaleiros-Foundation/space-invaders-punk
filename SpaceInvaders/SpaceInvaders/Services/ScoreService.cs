using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpaceInvaders.Data;
using SpaceInvaders.Interfaces.Services;
using SpaceInvaders.Models;

namespace SpaceInvaders.Services;

public class ScoreService : IScoreService
{
    private readonly SpaceInvadersDbContext _context;

    public ScoreService(SpaceInvadersDbContext context)
    {
        _context = context;
    }

    public async Task<Score> AddScoreAsync(Score score)
    {
        _context.Scores.Add(score);
        await _context.SaveChangesAsync();
        return score;
    }

    public async Task<List<Score>> GetTopScoresAsync(int count)
    {
        return await _context.Scores
            .OrderByDescending(s => s.PlayerScore)
            .Take(count)
            .ToListAsync();
    }

    public async Task<List<Score>> GetAllScoresAsync()
    {
        return await _context.Scores.Include(s => s.Player).ToListAsync();
    }

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
