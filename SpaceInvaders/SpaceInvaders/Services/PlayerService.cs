using Microsoft.EntityFrameworkCore;
using SpaceInvaders.Data;
using SpaceInvaders.Interfaces.Services;
using SpaceInvaders.Models;
using System.Threading.Tasks;

namespace SpaceInvaders.Services
{
    /// <summary>
    /// Provides services for managing player data in the database.
    /// </summary>
    public class PlayerService : IPlayerService
    {
        private readonly SpaceInvadersDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public PlayerService(SpaceInvadersDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a player by their ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the player to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation, containing the player if found, otherwise null.</returns>
        public async Task<Player> GetPlayerAsync(int id)
        {
            return await _context.Players.FindAsync(id);
        }

        /// <summary>
        /// Adds a new player to the database asynchronously.
        /// </summary>
        /// <param name="player">The player object to add.</param>
        /// <returns>A task that represents the asynchronous operation, containing the added player.</returns>
        public async Task<Player> AddPlayerAsync(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        /// <summary>
        /// Updates an existing player in the database asynchronously.
        /// </summary>
        /// <param name="player">The player object to update.</param>
        /// <returns>A task that represents the asynchronous operation, containing the updated player.</returns>
        public async Task<Player> UpdatePlayerAsync(Player player)
        {
            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return player;
        }

        /// <summary>
        /// Deletes a player from the database by their ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the player to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
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