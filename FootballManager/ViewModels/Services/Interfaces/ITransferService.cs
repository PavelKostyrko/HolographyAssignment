using FootballManager.DtoModels;
using FootballManager.Models;

namespace FootballManager.ViewModels.Services.Interfaces
{
    public interface ITransferService
    {
        void AddFreeAgent(PlayerDto playerDto, TeamDto teamDto);
        void PlayerTransfer(PlayerDto playerDto, TeamDto teamDto);
        void DeletePlayerFromTeam(int? playerDtoId);
    }
}
