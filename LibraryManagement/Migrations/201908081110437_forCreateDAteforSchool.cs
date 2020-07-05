namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forCreateDAteforSchool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schools", "CreatedBy", c => c.String());
            AddColumn("dbo.Schools", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schools", "CreatedDate");
            DropColumn("dbo.Schools", "CreatedBy");
        }
    }
}
