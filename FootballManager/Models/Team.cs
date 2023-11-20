using System.Collections.Generic;

namespace FootballManager.Models
{
    public class Team : BaseModel
    {
        public string Title { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool CanDelete { get; set; } = true;
        public List<Player> Players { get; set; }
    }
}
