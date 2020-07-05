namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class formodifyissuebookcontain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IssueBooks", "ClassID", c => c.Long(nullable: false));
            DropColumn("dbo.IssueBooks", "BookName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IssueBooks", "BookName", c => c.String());
            DropColumn("dbo.IssueBooks", "ClassID");
        }
    }
}
