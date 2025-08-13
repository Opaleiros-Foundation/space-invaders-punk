namespace SpaceInvaders.Presentation;

/// <summary>
/// ViewModel for the Controllers page, handling navigation back to the main view.
/// </summary>
public class ControllersViewModel
{
    private readonly INavigator _navigator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ControllersViewModel"/> class.
    /// </summary>
    /// <param name="navigator">The navigation service.</param>
    /// <param name="player">The player instance (though not directly used in this ViewModel, it's part of the constructor signature).</param>
    public ControllersViewModel(INavigator navigator, Player player)
    {
        _navigator = navigator;
        GoToMain = new AsyncRelayCommand(GoToMainView);
    }

    /// <summary>
    /// Gets the command to navigate back to the main view.
    /// </summary>
    public ICommand GoToMain { get; }

    /// <summary>
    /// Navigates back to the main view asynchronously.
    /// </summary>
    private async Task GoToMainView()
    {
        await _navigator.NavigateBackAsync(this);
    }
}
