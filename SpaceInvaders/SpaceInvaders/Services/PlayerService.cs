using Microsoft.EntityFrameworkCore;
using SpaceInvaders.Data;
using SpaceInvaders.Interfaces.Services;
using SpaceInvaders.Models;
using System.Threading.Tasks;

namespace SpaceInvaders.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly SpaceInvadersDbContext _context;

        public PlayerService(SpaceInvadersDbContext context)
        {
            _context = context;
        }

        public async Task<Player> GetPlayerAsync(int id)
        {
            return await _context.Players.FindAsync(id);
        }

        public async Task<Player> AddPlayerAsync(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<Player> UpdatePlayerAsync(Player player)
        {
            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task DeletePlayerAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
        }
    }
}