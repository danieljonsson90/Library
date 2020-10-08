namespace ConsidLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LibraryItem", "BorrowDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LibraryItem", "BorrowDate", c => c.DateTime(nullable: false));
        }
    }
}
