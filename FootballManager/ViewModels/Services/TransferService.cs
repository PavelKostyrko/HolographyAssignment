using FootballManager.Data;
using FootballManager.ViewModels.Services.Interfaces;
using System.Windows;
using System;
using System.Linq;
using System.Data.Entity;
using FootballManager.DtoModels;
using AutoMapper;
using FootballManager.Models;

namespace FootballManager.ViewModels.Services
{
    internal class TransferService : BaseService, ITransferService
    {
        public TransferService(ContextFM context, IMapper mapper) : base(context, mapper)
        { }

        public void AddFreeAgent(PlayerDto playerDto, TeamDto teamDto)
        {
            if (playerDto != null && teamDto != null
                && teamDto.Id != null && teamDto.Id > 0
                && teamDto.Title.All(char.IsLetter)
                && teamDto.City.All(char.IsLetter)
                && teamDto.Country.All(char.IsLetter)
                && playerDto.Id != null && playerDto.Id > 0
                && playerDto.Name.All(char.IsLetter)
                && playerDto.Surname.All(char.IsLetter))
            {
                var playerDb = _context.Players.Include(o => o.BiographicalEpisodies).SingleOrDefault(o => o.Id == playerDto.Id);
                var newTeamDb = _context.Teams.Include(o => o.Players).SingleOrDefault(o => o.Id == teamDto.Id);

                if (playerDb != null && newTeamDb != null)
                {
                    try
                    {
                        playerDb.Number = playerDto.Number;
                        playerDb.TeamId = newTeamDb.Id;
                        playerDb.Team = newTeamDb;

                        newTeamDb.Players.Add(playerDb);

                        playerDb.BiographicalEpisodies.Add(CreateNewBiography(playerDb, newTeamDb));

                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Saving to the database has been failed because: {ex.Message}",
                            "The transfer has been failed!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else MessageBox.Show("The player or the team don't exist",
                    "The adding to team has been failed!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("The player or the team don't exist",
                "The adding to the team has been failed!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void PlayerTransfer(PlayerDto playerDto, TeamDto teamDto)
        {
            if (playerDto != null && teamDto != null
                && teamDto.Id != null && teamDto.Id > 0
                && teamDto.Title.All(char.IsLetter)
                && teamDto.City.All(char.IsLetter)
                && teamDto.Country.All(char.IsLetter)
                && playerDto.Id != null && playerDto.Id > 0
                && playerDto.Name.All(char.IsLetter)
                && playerDto.Surname.All(char.IsLetter))
            {
                var playerDb = _context.Players.Include(o => o.BiographicalEpisodies).SingleOrDefault(o => o.Id == playerDto.Id);
                var newTeamDb = _context.Teams.Include(o => o.Players).SingleOrDefault(o => o.Id == teamDto.Id);
                var previousTeamDb = _context.Teams.Include(o => o.Players).SingleOrDefault(o => o.Id == playerDto.TeamId);

                if (playerDb != null && newTeamDb != null && previousTeamDb != null)
                {
                    try
                    {
                        playerDb.Number = playerDto.Number;
                        playerDb.TeamId = newTeamDb.Id;
                        playerDb.Team = newTeamDb;

                        previousTeamDb.Players.Remove(playerDb);
                        newTeamDb.Players.Add(playerDb);

                        ChangeBiography(playerDb, previousTeamDb);
                        playerDb.BiographicalEpisodies.Add(CreateNewBiography(playerDb, newTeamDb));

                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Saving to the database has been failed because: {ex.Message}",
                            "The transfer has been failed!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else MessageBox.Show("The player or the team don't exist",
                    "The transfer has been failed!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("The player or the team don't exist",
                "The transfer has been failed!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void DeletePlayerFromTeam(int? playerDtoId)
        {
            if (playerDtoId != null)
            {
                var playerDb = _context.Players.Include(o => o.BiographicalEpisodies).SingleOrDefault(o => o.Id == playerDtoId);
                var previousTeamDb = _context.Teams.Include(o => o.Players).SingleOrDefault(o => o.Id == playerDb.TeamId);

                if(playerDb != null && previousTeamDb !=null)
                {
                    try
                    {
                        previousTeamDb.Players.Remove(playerDb);
                        playerDb.Team = null;
                        playerDb.TeamId = null;
                        playerDb.Number = null;

                        ChangeBiography(playerDb, previousTeamDb);

                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Saving to the database has been failed because: {ex.Message}",
                            "The deleting from the team has been failed!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else MessageBox.Show("The player or team don't exist",
                "The deleting from the team has been failed!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else MessageBox.Show("Id can`t be null!",
                "The deleting from the team has been failed!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private Biography CreateNewBiography(Player player, Team team)
        {
            var newBiography = new Biography()
            {
                TeamTitle = team.Title,
                StartContract = $"{DateTime.Now:d}",
                EndContract = null,
                ContractStatus = "Действующий",
                PlayerId = player.Id,
                Player = player
            };

            _context.Biographies.Add(newBiography);
            return newBiography;
        }

        private void ChangeBiography(Player player, Team previousTeam)
        {
            var biographyDb = _context.Biographies.SingleOrDefault(o => o.PlayerId == player.Id 
                && o.TeamTitle == previousTeam.Title && o.ContractStatus == "Действующий");

            if (biographyDb != null)
            {
                biographyDb.EndContract = $"{DateTime.Now:d}";
                biographyDb.ContractStatus = "Расторгнут";
            }
            else MessageBox.Show("The biographical episode don`t exist!",
                "The change of biographical episode has been failed!", MessageBoxButton.OK, MessageBoxImage.Error);
        }   
    }
}
