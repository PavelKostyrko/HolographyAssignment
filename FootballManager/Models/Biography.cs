using System;

namespace FootballManager.Models
{
    public class Biography : BaseModel
    {
        public string TeamTitle { get; set; }
        public string StartContract { get; set; }
        public string EndContract { get; set; }
        public string ContractStatus { get; set; }
        public int? PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
