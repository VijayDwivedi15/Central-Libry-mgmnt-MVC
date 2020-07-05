namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fornewtablebookissuedetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookIssueDetails",
                c => new
                    {
                        IssueDetailID = c.Long(nullable: false, identity: true),
                        IssueID = c.Long(nullable: false),
                        BookID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.IssueDetailID)
                .ForeignKey("dbo.IssueBooks", t => t.IssueID, cascadeDelete: true)
                .Index(t => t.IssueID);
            
            DropColumn("dbo.IssueBooks", "BookID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IssueBooks", "BookID", c => c.Long(nullable: false));
            DropForeignKey("dbo.BookIssueDetails", "IssueID", "dbo.IssueBooks");
            DropIndex("dbo.BookIssueDetails", new[] { "IssueID" });
            DropTable("dbo.BookIssueDetails");
        }
    }
}
