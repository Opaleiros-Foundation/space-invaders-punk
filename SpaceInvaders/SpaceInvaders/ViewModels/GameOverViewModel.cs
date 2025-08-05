using Uno.Extensions.Navigation;

using Uno.Extensions.Navigation;

namespace SpaceInvaders.Presentation;

public partial class GameOverViewModel : ObservableObject
{
    private readonly INavigator _navigator;
    public ICommand GoToMain { get; }

    public GameOverViewModel(INavigator navigator, Player player)
    {
        _navigator = navigator;
        GoToMain = new AsyncRelayCommand(GoToMainView);
    }

    private async Task GoToMainView()
    {
        await _navigator.NavigateViewModelAsync<MainViewModel>(this, qualifier: Qualifiers.ClearBackStack);
    }
}
