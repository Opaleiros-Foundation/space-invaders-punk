namespace SpaceInvaders.Presentation;

/// <summary>
/// ViewModel for the application shell, responsible for overall navigation and initialization.
/// </summary>
public class ShellViewModel
{
  private readonly INavigator _navigator;

  /// <summary>
  /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
  /// </summary>
  /// <param name="navigator">The navigation service.</param>
  public ShellViewModel(
    INavigator navigator)
  {
    _navigator = navigator;
    // Add code here to initialize or attach event handlers to singleton services
  }
}