namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forforgienkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schools", "MediumId", "dbo.Media");
            DropIndex("dbo.Schools", new[] { "MediumId" });
            CreateIndex("dbo.Schools", "MediumId");
            AddForeignKey("dbo.Schools", "MediumId", "dbo.SchoolMediums", "MediumId", cascadeDelete: true);
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        MediumId = c.Long(nullable: false, identity: true),
                        MediumName = c.String(),
                    })
                .PrimaryKey(t => t.MediumId);
            
            DropForeignKey("dbo.Schools", "MediumId", "dbo.SchoolMediums");
            DropIndex("dbo.Schools", new[] { "MediumId" });
            CreateIndex("dbo.Schools", "MediumId");
            AddForeignKey("dbo.Schools", "MediumId", "dbo.Media", "MediumId", cascadeDelete: true);
        }
    }
}
