using FootballManager.DtoModels;
using FootballManager.ViewModels.Commands;
using FootballManager.ViewModels.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace FootballManager.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region VARIABLES 
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;
        private readonly ITransferService _transferService;

        private byte? numberForPlayer;

        private TeamDto selectedTeamForTransfer;
        private TeamDto teamForCreating;
        private PlayerDto selectedPlayerForTransfer;
        private PlayerDto selectedFreeAgent;
        private PlayerDto playerForCreating;
        
        private List<PlayerDto> playersForTransfer;
        private List<PlayerDto> freeAgents;
        private ObservableCollection<PlayerDto> allPlayers;
        private List<TeamDto> teamsForTransfer;
        private ObservableCollection<TeamDto> allTeams;

        private RelayCommand showPLayerTabCreatingBox;
        private RelayCommand savePlayerTab;
        private RelayCommand deletePlayer;
        private RelayCommand showTeamTabCreatingBox;
        private RelayCommand saveTeamTab;
        private RelayCommand deleteTeam;
        private RelayCommand deletePlayerFromTeam;
        private RelayCommand showCompositionTabPlayerForTransferSelectionBox;
        private RelayCommand showCompositionTabFreeAgentSelectionBox;
        private RelayCommand saveCompositionTab;
        #endregion

        public MainWindowViewModel(IPlayerService playerService, ITeamService teamService, ITransferService transferService)
        {
            _playerService = playerService;
            _teamService = teamService;
            _transferService = transferService;
            RefreshPlayersLists();
            RefreshTeamsLists();
        }

        #region PROPERTIES
        public Button SaveButtonPlayerTab { get; set; } = new Button() { IsEnabled = false };
        public Button SaveButtonTeamTab { get; set; } = new Button() { IsEnabled = false };
        public Button SaveButtonCompositionTab { get; set; } = new Button() { IsEnabled = false };

        public GroupBox CreatingBoxPlayerTab { get; set; } = new GroupBox { Visibility = Visibility.Hidden };
        public GroupBox CreatingBoxTeamTab { get; set; } = new GroupBox { Visibility = Visibility.Hidden };
        public GroupBox CreatingBoxCompositionTab { get; set; } = new GroupBox { Visibility = Visibility.Hidden };

        public ComboBox FreeAgentSelectionBox { get; set; } = new ComboBox();
        public ComboBox PlayerForTransferSelectionBox { get; set; } = new ComboBox();

        public PlayerDto PlayerForCreating
        {
            get { return playerForCreating; }
            set
            {
                playerForCreating = value;
                OnPropertyChanged("PlayerForCreating");
            }
        }
        public PlayerDto SelectedFreeAgent
        {
            get { return selectedFreeAgent; }
            set
            {
                selectedFreeAgent = value;
                OnPropertyChanged("SelectedFreeAgent");
            }
        }
        public PlayerDto SelectedPlayerForTransfer
        {
            get { return selectedPlayerForTransfer; }
            set
            {
                selectedPlayerForTransfer = value;
                OnPropertyChanged("SelectedPlayerForTransfer");
            }
        }

        public TeamDto TeamForCreating
        {
            get { return teamForCreating; }
            set
            {
                teamForCreating = value;
                OnPropertyChanged("TeamForCreating");
            }
        }
        public TeamDto SelectedTeamForTransfer
        {
            get { return selectedTeamForTransfer; }
            set
            {
                selectedTeamForTransfer = value;
                OnPropertyChanged("SelectedTeamForTransfer");
            }
        }

        public byte? NumberForPlayer
        {
            get { return numberForPlayer; }
            set
            {
                numberForPlayer = value;
                OnPropertyChanged("NumberForPlayer");
            }
        }

        public ObservableCollection<PlayerDto> AllPlayers
        {
            get { return allPlayers; }
            set
            {
                allPlayers = value;
                OnPropertyChanged("AllPlayers");
            }
        }
        public List<PlayerDto> FreeAgents
        {
            get { return freeAgents; }
            set
            {
                freeAgents = value;
                OnPropertyChanged("FreeAgents");
            }
        }
        public List<PlayerDto> PlayersForTransfer
        {
            get { return playersForTransfer; }
            set
            {
                playersForTransfer = value;
                OnPropertyChanged("PlayersForTransfer");
            }
        }

        public ObservableCollection<TeamDto> AllTeams
        {
            get { return allTeams; }
            set
            {
                allTeams = value;
                OnPropertyChanged("AllTeams");
            }
        }
        public List<TeamDto> TeamsForTransfer
        {
            get { return teamsForTransfer; }
            set
            {
                teamsForTransfer = value;
                OnPropertyChanged("TeamsForTransfer");
            }
        }
        #endregion

        #region COMMANDS
        public RelayCommand ShowPLayerTabCreatingBox
        {
            get
            {
                return showPLayerTabCreatingBox ?? (showPLayerTabCreatingBox = new RelayCommand(obj =>
                {
                    AddCreatingFormPlayerTab();
                    EnableSaveButtonPlayerTab();
                    PlayerForCreating = new PlayerDto();
                }
                ));
            }
        }
        public RelayCommand SavePlayerTab
        {
            get
            {
                return savePlayerTab ?? (savePlayerTab = new RelayCommand(obj =>
                {
                    _playerService.CreatePlayer(PlayerForCreating);
                    RefreshPlayersLists();
                    HideCreatingFormPlayerTab();
                    DisableSaveButtonPlayerTab();
                }
                ));
            }
        }
        public RelayCommand DeletePlayer
        {
            get
            {
                return deletePlayer ?? (deletePlayer = new RelayCommand(obj =>
                {
                    if (obj is PlayerDto playerDto)
                    {
                        if (MessageBox.Show($"{playerDto.Name} {playerDto.Surname}", "Do you want to delete?", MessageBoxButton.YesNo, 
                            MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                        {
                            _playerService.ChangePlayerDeleteStatus(playerDto.Id);
                            RefreshPlayersLists();
                        }
                    }
                }
                ));
            }
        }

        public RelayCommand ShowTeamTabCreatingBox
        {
            get
            {
                return showTeamTabCreatingBox ?? (showTeamTabCreatingBox = new RelayCommand(obj =>
                {
                    AddCreatingFormTeamTab();
                    EnableSaveButtonTeamTab();
                    TeamForCreating = new TeamDto();
                }
                ));
            }
        }
        public RelayCommand SaveTeamTab
        {
            get
            {
                return saveTeamTab ?? (saveTeamTab = new RelayCommand(obj =>
                {
                    _teamService.CreateTeam(TeamForCreating);
                    RefreshTeamsLists();
                    HideCreatingFormTeamTab();
                    DisableSaveButtonTeamTab();
                }
                ));
            }
        }
        public RelayCommand DeleteTeam
        {
            get
            {
                return deleteTeam ?? (deleteTeam = new RelayCommand(obj =>
                {
                    if (obj is TeamDto teamDto)
                    {
                        if (MessageBox.Show(teamDto.Title, "Do you want to delete?", MessageBoxButton.YesNo, 
                            MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                        {
                            _teamService.ChangeTeamDeleteStatus(teamDto.Id);
                            RefreshTeamsLists();
                        }
                    }
                }
                ));
            }
        }

        public RelayCommand ShowCompositionTabFreeAgentSelectionBox
        {
            get
            {
                return showCompositionTabFreeAgentSelectionBox ?? (showCompositionTabFreeAgentSelectionBox = new RelayCommand(obj =>
                {
                    AddCreatingFormCompositionTab();
                    EnableSaveButtonCompositionTab();
                    EnableFreeAgentSelectionBoxCompositionTab();
                    DisableEnablePlayerForTransferSelectionBoxCompositionTab();

                    RefreshPlayersLists();
                    RefreshTeamsLists();
                }
                ));
            }
        }
        public RelayCommand ShowCompositionTabPlayerForTransferSelectionBox
        {
            get
            {
                return showCompositionTabPlayerForTransferSelectionBox ?? (showCompositionTabPlayerForTransferSelectionBox = new RelayCommand(obj =>
                {
                    AddCreatingFormCompositionTab();
                    EnableSaveButtonCompositionTab();
                    EnablePlayerForTransferSelectionBoxCompositionTab();
                    DisableEnableFreeAgentSelectionBoxCompositionTab();

                    RefreshPlayersLists();
                    RefreshTeamsLists();
                }
                ));
            }
        }
        public RelayCommand SaveCompositionTab
        {
            get
            {
                return saveCompositionTab ?? (saveCompositionTab = new RelayCommand(obj =>
                {
                    if(FreeAgentSelectionBox.IsEnabled == true && PlayerForTransferSelectionBox.IsEnabled == false)
                    {
                        SelectedFreeAgent.Number = NumberForPlayer;
                        _transferService.AddFreeAgent(SelectedFreeAgent, SelectedTeamForTransfer);   
                    }

                    if (PlayerForTransferSelectionBox.IsEnabled == true && FreeAgentSelectionBox.IsEnabled == false)
                    {
                        SelectedPlayerForTransfer.Number = NumberForPlayer;
                        _transferService.PlayerTransfer(SelectedPlayerForTransfer, SelectedTeamForTransfer);  
                    }

                    SelectedPlayerForTransfer = null;
                    SelectedFreeAgent = null;
                    SelectedTeamForTransfer = null;
                    NumberForPlayer = null;
                    HideCreatingFormCompositionTab();
                    DisableSaveButtonCompositionTab();

                    RefreshPlayersLists();
                    RefreshTeamsLists();
                }
                ));
            }
        }
        
        public RelayCommand DeletePlayerFromTeam
        {
            get
            {
                return deletePlayerFromTeam ?? (deletePlayerFromTeam = new RelayCommand(obj =>
                {
                    if (obj is PlayerDto playerDto)
                    {
                        if (MessageBox.Show($"{playerDto.Name} {playerDto.Surname}", "Remove from the team??", MessageBoxButton.YesNo,
                            MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                        {
                            _transferService.DeletePlayerFromTeam(playerDto.Id);
                        }
                    }

                    RefreshPlayersLists();
                    RefreshTeamsLists();
                }
                ));
            }
        }
        #endregion

        #region AUXILIARY METHODS
        private void AddCreatingFormPlayerTab()
        {
            CreatingBoxPlayerTab.Visibility = Visibility.Visible;
        }
        private void HideCreatingFormPlayerTab()
        {
            CreatingBoxPlayerTab.Visibility = Visibility.Hidden;
        }
        private void AddCreatingFormTeamTab()
        {
            CreatingBoxTeamTab.Visibility = Visibility.Visible;
        }
        private void HideCreatingFormTeamTab()
        {
            CreatingBoxTeamTab.Visibility = Visibility.Hidden;
        }
        private void AddCreatingFormCompositionTab()
        {
            CreatingBoxCompositionTab.Visibility = Visibility.Visible;
        }
        private void HideCreatingFormCompositionTab()
        {
            CreatingBoxCompositionTab.Visibility = Visibility.Hidden;
        }
        private void EnableSaveButtonPlayerTab()
        {
            SaveButtonPlayerTab.IsEnabled = true;
        }
        private void DisableSaveButtonPlayerTab()
        {
            SaveButtonPlayerTab.IsEnabled = false;
        }
        private void EnableSaveButtonTeamTab()
        {
            SaveButtonTeamTab.IsEnabled = true;
        }
        private void DisableSaveButtonTeamTab()
        {
            SaveButtonTeamTab.IsEnabled = false;
        }
        private void EnableSaveButtonCompositionTab()
        {
            SaveButtonCompositionTab.IsEnabled = true;
        }
        private void DisableSaveButtonCompositionTab()
        {
            SaveButtonCompositionTab.IsEnabled = false;
        }
        private void EnableFreeAgentSelectionBoxCompositionTab()
        {
            FreeAgentSelectionBox.IsEnabled = true;
        }
        private void DisableEnableFreeAgentSelectionBoxCompositionTab()
        {
            FreeAgentSelectionBox.IsEnabled = false;
        }
        private void EnablePlayerForTransferSelectionBoxCompositionTab()
        {
            PlayerForTransferSelectionBox.IsEnabled = true;
        }
        private void DisableEnablePlayerForTransferSelectionBoxCompositionTab()
        {
            PlayerForTransferSelectionBox.IsEnabled = false;
        }
        private void RefreshPlayersLists()
        {
            AllPlayers = new ObservableCollection<PlayerDto>(_playerService.GetAllPlayers());
            FreeAgents = _playerService.GetAllFreeAgents();
            PlayersForTransfer = _playerService.GetAllPlayersForTransfer();
        }
        private void RefreshTeamsLists()
        {
            AllTeams = new ObservableCollection<TeamDto>(_teamService.GetAllTeams());
            TeamsForTransfer = _teamService.GetAllTeamsForTransfer();
        }
        #endregion
    }
}