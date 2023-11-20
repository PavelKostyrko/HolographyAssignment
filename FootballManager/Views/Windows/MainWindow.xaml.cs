using FootballManager.ViewModels;
using FootballManager.ViewModels.Services.Interfaces;
using System.Windows;

namespace FootballManager.Views.Windows
{
    public partial class MainWindow : Window
    {
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;
        private readonly ITransferService _transferService;
        public MainWindow(IPlayerService playerService, ITeamService teamService, ITransferService transferService)
        {
            _playerService = playerService;
            _teamService = teamService;
            _transferService = transferService;
            DataContext = new MainWindowViewModel(_playerService, _teamService, _transferService);
            InitializeComponent();
        }
    }
}
