using CommunityToolkit.Mvvm.ComponentModel;

namespace SpaceInvaders.Presentation;

public partial class GameStartPageViewModel : ObservableObject
{
    private readonly INavigator _navigator;

    [ObservableProperty]
    private double _playerX;

    [ObservableProperty]
    private double _playerY;

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
