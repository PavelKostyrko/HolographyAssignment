using AutoMapper;
using FootballManager.Data;
using FootballManager.DtoModels;
using FootballManager.Models;
using FootballManager.ViewModels.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace FootballManager.ViewModels.Services
{
    public class PlayerService : BaseService, IPlayerService
    {
        public PlayerService(ContextFM context, IMapper mapper) : base(context, mapper)
        { }

        public ObservableCollection<PlayerDto> GetAllPlayers()
        {
            _context.Players?.Include(o => o.BiographicalEpisodies).Load();
            _context.Players?.Include(o => o.Team).Load();
            return _mapper.Map<ObservableCollection<PlayerDto>>(_context.Players?.Local);
        }
        public List<PlayerDto> GetAllFreeAgents()
        {
            return _mapper.Map<List<PlayerDto>>(_context.Players.Where(o => o.TeamId == null && o.CanDelete == true).ToList());
        }
        public List<PlayerDto> GetAllPlayersForTransfer()
        {
            return _mapper.Map<List<PlayerDto>>(_context.Players.Where(o => o.TeamId != null && o.CanDelete == true).ToList());
        }

        public void CreatePlayer(PlayerDto playerDto)
        {
            if (playerDto != null
                && playerDto.Name.All(char.IsLetter)
                && playerDto.Surname.All(char.IsLetter)
                && playerDto.BirthDate != null
                && playerDto.Id == null)    
            {
                
                if (!_context.Players.Any(o => o.Name == playerDto.Name 
                    && o.Surname == playerDto.Surname
                    && o.BirthDate == playerDto.BirthDate 
                    && o.Role == playerDto.Role))
                {
                    try
                    {
                        _context.Players.Add(_mapper.Map<Player>(playerDto));
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Saving to the database has been failed because: {ex.Message}",
                            "The player has not been created!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else MessageBox.Show("Such the player already exists!",
                    "The player has not been created!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else MessageBox.Show("The information about the player is not valid!",
                "The player has not been created!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ChangePlayerDeleteStatus(int? playerDtoId)
        {
            if (playerDtoId != null)
            {
                var playerDb = _context.Players.SingleOrDefault(o => o.Id == playerDtoId);
                
                if (playerDb != null)
                {
                    try
                    {
                        playerDb.CanDelete = false;
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Saving to the database has been failed because: {ex.Message}",
                            "The player information has not been updated!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else MessageBox.Show("Id can`t be null!",
                "The player information has not been updated!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
