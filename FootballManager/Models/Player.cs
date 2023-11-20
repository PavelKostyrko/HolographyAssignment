using System;
using System.Collections.Generic;

namespace FootballManager.Models
{
    public class Player : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Role { get; set; }
        public byte? Number { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
        public bool CanDelete { get; set; } = true;
        public List<Biography> BiographicalEpisodies { get; set; }
    }
}
