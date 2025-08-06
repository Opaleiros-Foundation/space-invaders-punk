using SpaceInvaders.Constants;
using Uno.Extensions.Navigation;
using SpaceInvaders.Services;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using SpaceInvaders.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Uno.Extensions.DependencyInjection;

namespace SpaceInvaders.Presentation;

    public partial class GameOverViewModel : ObservableObject
    {
        private readonly INavigator _navigator;
        private readonly IPlayerService _playerService;
        private readonly IScoreService _scoreService;
        public ICommand GoToMain { get; }
        public ICommand PlayAgain { get; }
        public ICommand SaveScoreCommand { get; }

        [ObservableProperty]
        private Player _player;

        [ObservableProperty]
        private string _scoreText;

        [ObservableProperty]
        private string _playerName;

        [ObservableProperty]
        private string _confirmationMessage;

        public GameOverViewModel(INavigator navigator, IPlayerService playerService, IScoreService scoreService, Player player)
        {
            _navigator = navigator;
            _playerService = playerService;
            _scoreService = scoreService;
            GoToMain = new AsyncRelayCommand(GoToMainView);
            PlayAgain = new AsyncRelayCommand(PlayAgainView);
            SaveScoreCommand = new AsyncRelayCommand(SaveScore);

            _player = player;
        }

        partial void OnPlayerChanged(Player value)
        {
            ScoreText = $"SCORE: {value.Score}";
        }

        private async Task GoToMainView()
        {
            await _navigator.NavigateViewModelAsync<MainViewModel>(this, qualifier: Qualifiers.ClearBackStack);
        }

        private async Task PlayAgainView()
        {
            var newPlayer = new Player("Player 1", 100, new Weapon(10, 1.0, SpritePaths.Projectile), 3, 0);
            await _navigator.NavigateViewModelAsync<GameStartPageViewModel>(this, data: newPlayer, qualifier: Qualifiers.ClearBackStack);
        }

        private async Task SaveScore()
        {
            _player.Name = PlayerName; // Update player name from input

            if (_player.Id == 0)
            {
                // New player, add to database
                await _playerService.AddPlayerAsync(_player);
            }
            else
            {
                // Existing player, update in database
                await _playerService.UpdatePlayerAsync(_player);
            }

            // Save the score
            var newScore = new Score
            {
                PlayerScore = _player.Score,
                DateAchieved = DateTime.UtcNow,
                PlayerId = _player.Id // Link to the player
            };
            await _scoreService.AddScoreAsync(newScore);

            ConfirmationMessage = $"Score de {_player.Score} salvo com sucesso para {_player.Name}!";
        }
    }
