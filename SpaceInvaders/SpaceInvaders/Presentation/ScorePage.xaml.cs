using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace SpaceInvaders.Presentation;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ScorePage : Page
{
    public ScoreViewModel ViewModel { get; }

    public ScorePage()
    {
        ViewModel = App.Host.Services.GetRequiredService<ScoreViewModel>();
        this.InitializeComponent();
        this.DataContext = ViewModel;
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        await ViewModel.OnNavigatedTo();
    }

    private void BackToMainMenu_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (Frame.CanGoBack)
        {
            Frame.GoBack();
        }
        else
        {
            // Fallback if cannot go back, e.g., navigate to a specific page
            Frame.Navigate(typeof(MainPage));
        }
    }
}

