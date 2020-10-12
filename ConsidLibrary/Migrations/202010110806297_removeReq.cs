namespace ConsidLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeReq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LibraryItem", "Title", c => c.String());
            AlterColumn("dbo.LibraryItem", "Author", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LibraryItem", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.LibraryItem", "Title", c => c.String(nullable: false));
        }
    }
}
