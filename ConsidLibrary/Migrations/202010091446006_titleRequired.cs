namespace ConsidLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class titleRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LibraryItem", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LibraryItem", "Title", c => c.String());
        }
    }
}
