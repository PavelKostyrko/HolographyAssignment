namespace FootballManager.Migrations
{
    using FootballManager.Models;
    using System;
    using System.Data.Entity.Migrations;


    internal sealed class Configuration : DbMigrationsConfiguration<Data.ContextFM>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.ContextFM context)
        {
            //Player p1 = new Player
            //{
            //    Id = 1,
            //    Name = "Алексей",
            //    Surname = "Сапожков",
            //    BirthDate = DateTime.Now,
            //    Role = "Защитник",
            //    Number = 11,
            //    TeamId = 1,
            //    CanDelete = true
            //};
            //Player p2 = new Player
            //{
            //    Id = 2,
            //    Name = "Владимир",
            //    Surname = "Цупенков",
            //    BirthDate = DateTime.Now,
            //    Role = "Нападающий",
            //    Number = 33,
            //    TeamId = 2,
            //    CanDelete = true
            //};
            //Team t1 = new Team
            //{
            //    Id = 1,
            //    Title = "Манчестер Юнайтед",
            //    City = "Стретфорд",
            //    Country = "Великобритания",
            //    CanDelete = true
            //};
            //Team t2 = new Team
            //{
            //    Id = 2,
            //    Title = "Барселона",
            //    City = "Барселона",
            //    Country = "Испания",
            //    CanDelete = true
            //};
            //Team t3 = new Team
            //{
            //    Id = 3,
            //    Title = "Ювентус",
            //    City = "Турин",
            //    Country = "Италия",
            //    CanDelete = true
            //};
            //Biography b1 = new Biography
            //{
            //    Id = 1,
            //    TeamTitle = "Манчестер Юнайтед",
            //    StartContract = $"{DateTime.Now:d}",
            //    EndContract = null,
            //    ContractStatus = "Действующий",
            //    PlayerId = 1
            //};

            //context.Players.Add(p1);
            //context.Players.Add(p2);
            //context.Teams.Add(t1);
            //context.Teams.Add(t2);
            //context.Teams.Add(t3);
            //context.Biographies.Add(b1);
            //context.SaveChanges();
        }
    }
}
