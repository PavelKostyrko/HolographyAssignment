using FootballManager.Models;
using System.Data.Entity.ModelConfiguration;

namespace FootballManager.Data.Configurations
{
    public class BiographyConfiguration : EntityTypeConfiguration<Biography>
    {
        public BiographyConfiguration()
        {
            ToTable("Biographies").HasKey(p => p.Id);
            Property(p => p.Id).IsRequired();
            Property(p => p.TeamTitle).IsRequired();
            Property(p => p.StartContract).IsRequired();
            HasRequired(p => p.Player).WithMany(p => p.BiographicalEpisodies);
        }
    }
}
