using FootballManager.Models;
using System.Data.Entity.ModelConfiguration;

namespace FootballManager.Data.Configurations
{
    public class PlayerConfiguration : EntityTypeConfiguration<Player>
    {
        public PlayerConfiguration()
        {
            ToTable("Players").HasKey(p => p.Id);
            Property(p => p.Id).IsRequired();
            Property(p => p.Name).IsRequired();
            Property(p => p.Surname).IsRequired();
            Property(p => p.BirthDate).IsRequired();
            Property(p => p.Role).IsRequired();
            HasOptional(p => p.Team).WithMany(p => p.Players);
        }
    }
}
