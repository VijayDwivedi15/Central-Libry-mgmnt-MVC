namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forclassaddedinsubject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "ClassID", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "ClassID");
        }
    }
}
