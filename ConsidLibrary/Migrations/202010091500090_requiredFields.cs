namespace ConsidLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Category", "CategoryName", c => c.String(nullable: false));
            AlterColumn("dbo.Employee", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Employee", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.LibraryItem", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.LibraryItem", "Type", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LibraryItem", "Type", c => c.String());
            AlterColumn("dbo.LibraryItem", "Author", c => c.String());
            AlterColumn("dbo.Employee", "LastName", c => c.String());
            AlterColumn("dbo.Employee", "FirstName", c => c.String());
            AlterColumn("dbo.Category", "CategoryName", c => c.String());
        }
    }
}
