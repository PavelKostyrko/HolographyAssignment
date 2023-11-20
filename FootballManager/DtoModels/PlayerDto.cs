using System;
using System.Collections.Generic;

namespace FootballManager.DtoModels
{
    public class PlayerDto : BaseDtoModel
    {
        private string name;
        private string surname;
        private DateTime? birthDate;
        private string role;
        private byte? number;
        private int? teamId;
        private TeamDto team;
        private bool canDelete = true;
        private List<BiographyDto> biographicalEpisodies;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");

            }
        }
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");

            }
        }
        public DateTime? BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");

            }
        }
        public string Role
        {
            get { return role; }
            set
            {
                role = value;
                OnPropertyChanged("Role");

            }
        }
        public byte? Number
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged("Number");

            }
        }
        public int? TeamId
        {
            get { return teamId; }
            set
            {
                teamId = value;
                OnPropertyChanged("TeamId");

            }
        }
        public TeamDto Team
        {
            get { return team; }
            set
            {
                team = value;
                OnPropertyChanged("Team");

            }
        }
        public bool CanDelete
        {
            get { return canDelete; }
            set
            {
                canDelete = value;
                OnPropertyChanged("CanDelete");

            }
        }
        public List<BiographyDto> BiographicalEpisodies
        {
            get { return biographicalEpisodies; }
            set
            {
                biographicalEpisodies = value;
                OnPropertyChanged("BiographicalEpisodies");

            }
        }
    }
}
