using System.Collections.Generic;

namespace FootballManager.DtoModels
{
    public class TeamDto : BaseDtoModel
    {
        private string title;
        private string city;
        private string country;
        private bool canDelete = true;
        private List<PlayerDto> players;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }
        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                OnPropertyChanged("Country");
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
        public List<PlayerDto> Players
        {
            get { return players; }
            set
            {
                players = value;
                OnPropertyChanged("Players");
            }
        }
    }
}
