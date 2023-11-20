namespace FootballManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Biographies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamTitle = c.String(nullable: false),
                        StartContract = c.String(nullable: false),
                        EndContract = c.String(),
                        ContractStatus = c.String(),
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Role = c.String(nullable: false),
                        Number = c.Byte(),
                        TeamId = c.Int(),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Biographies", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropIndex("dbo.Biographies", new[] { "PlayerId" });
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
            DropTable("dbo.Biographies");
        }
    }
}
