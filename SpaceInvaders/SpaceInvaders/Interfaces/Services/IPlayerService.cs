using SpaceInvaders.Models;
using System.Threading.Tasks;

namespace SpaceInvaders.Interfaces.Services
{
    /// <summary>
    /// Defines the methods for the player management service.
    /// </summary>
    public interface IPlayerService
    {
        /// <summary>
        /// Asynchronously retrieves a player by their ID.
        /// </summary>
        /// <param name="id">The ID of the player.</param>
        /// <returns>A task that represents the asynchronous operation, containing the found player or null if not found.</returns>
        Task<Player> GetPlayerAsync(int id);
        /// <summary>
        /// Asynchronously adds a new player.
        /// </summary>
        /// <param name="player">The Player object to be added.</param>
        /// <returns>A task that represents the asynchronous operation, containing the added player.</returns>
        Task<Player> AddPlayerAsync(Player player);
        /// <summary>
        /// Asynchronously updates an existing player.
        /// </summary>
        /// <param name="player">The Player object to be updated.</param>
        /// <returns>A task that represents the asynchronous operation, containing the updated player.</returns>
        Task<Player> UpdatePlayerAsync(Player player);
        /// <summary>
        /// Asynchronously deletes a player by their ID.
        /// </summary>
        /// <param name="id">The ID of the player to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeletePlayerAsync(int id);
    }
}