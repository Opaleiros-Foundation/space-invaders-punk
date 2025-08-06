using SpaceInvaders.Models;
using System.Threading.Tasks;

namespace SpaceInvaders.Interfaces.Services
{
    public interface IPlayerService
    {
        Task<Player> GetPlayerAsync(int id);
        Task<Player> AddPlayerAsync(Player player);
        Task<Player> UpdatePlayerAsync(Player player);
        Task DeletePlayerAsync(int id);
    }
}