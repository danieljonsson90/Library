namespace ConsidLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LibraryItem", "Type", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LibraryItem", "Type", c => c.String());
        }
    }
}
