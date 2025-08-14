using System.Collections.Generic;
using System.Threading.Tasks;
using SpaceInvaders.Models;

namespace SpaceInvaders.Interfaces.Services;

/// <summary>
/// Define a interface para um serviço de cache de pontuações.
/// </summary>
public interface IScoreCacheService
{
    /// <summary>
    /// Obtém um valor que indica se o cache está atualmente carregando as pontuações.
    /// </summary>
    bool IsLoading { get; }
    /// <summary>
    /// Obtém um valor que indica se o cache de pontuações está pronto para ser usado.
    /// </summary>
    bool IsCacheReady { get; }
    /// <summary>
    /// Obtém a lista de pontuações armazenadas em cache.
    /// </summary>
    /// <returns>Uma lista de objetos <see cref="Score"/>.</returns>
    List<Score> GetScores();
    /// <summary>
    /// Pré-carrega as pontuações no cache de forma assíncrona.
    /// </summary>
    /// <returns>Uma <see cref="Task"/> que representa a operação assíncrona.</returns>
    Task PreloadScoresAsync();
    /// <summary>
    /// Invalida o cache, forçando um novo carregamento na próxima vez que as pontuações forem solicitadas.
    /// </summary>
    void InvalidateCache();
}
