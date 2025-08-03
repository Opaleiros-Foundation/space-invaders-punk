using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpaceInvaders.Interfaces.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpaceInvaders.Presentation;

public partial class EndGameViewModel : ObservableObject
{
    private readonly INavigator _navigator;

    [ObservableProperty]
    private string _finalScoreText;

    public EndGameViewModel(INavigator navigator, string finalScore)
    {
        _navigator = navigator;
        FinalScoreText = $"FINAL SCORE: {finalScore}";

        PlayAgainCommand = new AsyncRelayCommand(PlayAgain);
        GoToMainMenuCommand = new AsyncRelayCommand(GoToMainMenu);
    }

    public ICommand PlayAgainCommand { get; }
    public ICommand GoToMainMenuCommand { get; }

    private async Task PlayAgain()
    {
        await _navigator.NavigateViewModelAsync<GameStartPageViewModel>(this);
    }

    private async Task GoToMainMenu()
    {
        await _navigator.NavigateViewModelAsync<MainViewModel>(this);
    }
}
