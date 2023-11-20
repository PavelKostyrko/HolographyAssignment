using FootballManager.Models;
using System.Data.Entity.ModelConfiguration;

namespace FootballManager.Data.Configurations
{
    public class TeamConfiguration : EntityTypeConfiguration<Team>
    {
        public TeamConfiguration()
        {
            ToTable("Teams").HasKey(p => p.Id);
            Property(p => p.Id).IsRequired();
            Property(p => p.Title).IsRequired();
            Property(p => p.City).IsRequired();
            Property(p => p.Country).IsRequired();
        }
    }
}
