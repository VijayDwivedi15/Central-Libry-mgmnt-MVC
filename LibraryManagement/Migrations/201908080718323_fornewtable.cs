namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fornewtable : DbMigration
    {
        public override void Up()
        { 
          
            CreateTable(
                "dbo.SchoolMediums",
                c => new
                    {
                        MediumId = c.Long(nullable: false, identity: true),
                        MediumName = c.String(),
                    })
                .PrimaryKey(t => t.MediumId); 
        } 
        public override void Down()
        {
     
            DropTable("dbo.SchoolMediums");
        
        }
    }
}
