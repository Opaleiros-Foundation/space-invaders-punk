using SpaceInvaders.Models;
using SpaceInvaders.Constants;

namespace SpaceInvaders.Services;

public class PlayerService
{
    public Player CurrentPlayer { get; private set; }

    public PlayerService()
    {
        // Inicializa o jogador com um nome padrão, que será atualizado depois
        CurrentPlayer = new Player("", 100, new Weapon(10, 0.5, SpritePaths.Projectile), 64, 64);
    }

    public void SetPlayerName(string name)
    {
        CurrentPlayer.Name = name;
    }

    public void ResetPlayer()
    {
        CurrentPlayer = new Player(CurrentPlayer.Name, 100, new Weapon(10, 0.5, SpritePaths.Projectile), 64, 64);
    }
}
