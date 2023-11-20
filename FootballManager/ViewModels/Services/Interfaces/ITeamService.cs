using FootballManager.DtoModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FootballManager.ViewModels.Services.Interfaces
{
    public interface ITeamService
    {
        ObservableCollection<TeamDto> GetAllTeams();
        List<TeamDto> GetAllTeamsForTransfer();
        void CreateTeam(TeamDto teamDto);
        void ChangeTeamDeleteStatus(int? teamDtoId);
    }
}
