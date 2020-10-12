namespace ConsidLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookclass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LibraryItem", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.LibraryItem", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.LibraryItem", "Type", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LibraryItem", "Type", c => c.String());
            AlterColumn("dbo.LibraryItem", "Author", c => c.String());
            AlterColumn("dbo.LibraryItem", "Title", c => c.String());
        }
    }
}
