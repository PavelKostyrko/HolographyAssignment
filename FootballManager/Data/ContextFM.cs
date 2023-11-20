using FootballManager.Data.Configurations;
using FootballManager.Models;
using System.Data.Entity;

namespace FootballManager.Data
{
    public class ContextFM : DbContext
    {       
        public ContextFM() : base("DefaultConnection")
        {}

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Biography> Biographies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new PlayerConfiguration());
            modelBuilder.Configurations.Add(new TeamConfiguration());
            modelBuilder.Configurations.Add(new BiographyConfiguration());
        }
    }
}
