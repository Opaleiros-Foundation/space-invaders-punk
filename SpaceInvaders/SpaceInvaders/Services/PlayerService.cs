using SpaceInvaders.Models;
using SpaceInvaders.Constants;

namespace SpaceInvaders.Services;

public class PlayerService
{
    public Player CurrentPlayer { get; private set; }

    public PlayerService()
    {
        // Inicializa o jogador com valores padrão ou carrega de algum lugar
        CurrentPlayer = new Player("Player1", 100, new Weapon(10, 0.5, SpritePaths.Projectile), 64, 64);
    }

    public void ResetPlayer()
    {
        CurrentPlayer = new Player("Player1", 100, new Weapon(10, 0.5, SpritePaths.Projectile), 64, 64);
    }
}
