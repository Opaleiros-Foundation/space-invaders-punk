namespace SpaceInvaders.Models;

/// <summary>
/// Represents the application configuration settings.
/// </summary>
public record AppConfig
{
  /// <summary>
  /// Gets the environment in which the application is running (e.g., "Development", "Production").
  /// </summary>
  public string? Environment { get; init; }
}