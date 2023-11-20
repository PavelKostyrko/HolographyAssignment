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
    public class TeamService : BaseService, ITeamService
    {
        public TeamService(ContextFM context, IMapper mapper) : base(context, mapper)
        { }

        public ObservableCollection<TeamDto> GetAllTeams()
        {
            return _mapper.Map<ObservableCollection<TeamDto>>(_context.Teams?.Include(o => o.Players));
        }

        public List<TeamDto> GetAllTeamsForTransfer()
        {
            return _mapper.Map<List<TeamDto>>(_context.Teams?.Where(o => o.CanDelete == true).Include(o => o.Players).ToList());
        }
        
        public void CreateTeam(TeamDto teamDto)
        {
            if (teamDto != null
                && teamDto.Title.All(char.IsLetter)
                && teamDto.City.All(char.IsLetter)
                && teamDto.Country.All(char.IsLetter)
                && teamDto.Id == null)
            {
                if (!_context.Teams.Any(o => o.Title == teamDto.Title
                    && o.City == teamDto.City
                    && o.Country == teamDto.Country))
                {
                    try
                    {
                        _context.Teams.Add(_mapper.Map<Team>(teamDto));
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Saving to the database has been failed because: {ex.Message}",
                                "The team has not been created!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else MessageBox.Show("Such the team already exists!",
                    "The team has not been created!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("The information about the team is not valid!",
               "The team has not been created!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ChangeTeamDeleteStatus(int? teamDtoId)
        {
            if (teamDtoId != null)
            {
                var teamDb = _context.Teams.SingleOrDefault(o => o.Id == teamDtoId);
                
                if (teamDb != null)
                {
                    try
                    {
                        teamDb.CanDelete = false;
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Saving to the database has been failed because: {ex.Message}",
                            "The team information has not been updated!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else MessageBox.Show("Id can`t be null!",
                "The team information has not been updated!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
