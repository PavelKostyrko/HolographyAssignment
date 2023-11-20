using FootballManager.DtoModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FootballManager.ViewModels.Services.Interfaces
{
    public interface IPlayerService
    {
        ObservableCollection<PlayerDto> GetAllPlayers();
        List<PlayerDto> GetAllPlayersForTransfer();
        List<PlayerDto> GetAllFreeAgents();
        void CreatePlayer(PlayerDto playerDto);
        void ChangePlayerDeleteStatus(int? playerDtoId);
    }
}
