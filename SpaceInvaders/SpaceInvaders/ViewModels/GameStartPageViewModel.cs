namespace SpaceInvaders.Presentation;

public class GameStartPageViewModel
{
    private readonly INavigator _navigator;

    public GameStartPageViewModel(INavigator navigator, Player player)
    {
        _navigator = navigator;
        GoToMain = new AsyncRelayCommand(GoToMainView);
    }

    public ICommand GoToMain { get; }

    private async Task GoToMainView()
    {
        await _navigator.NavigateBackAsync(this);
    }
}
