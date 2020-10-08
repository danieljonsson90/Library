namespace ConsidLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedLibraryItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LibraryItem", "CategoryId", "dbo.Category");
            DropIndex("dbo.LibraryItem", new[] { "CategoryId" });
            AlterColumn("dbo.LibraryItem", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.LibraryItem", "CategoryId");
            AddForeignKey("dbo.LibraryItem", "CategoryId", "dbo.Category", "Id", cascadeDelete: true);
            DropColumn("dbo.LibraryItem", "CategoryName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LibraryItem", "CategoryName", c => c.String());
            DropForeignKey("dbo.LibraryItem", "CategoryId", "dbo.Category");
            DropIndex("dbo.LibraryItem", new[] { "CategoryId" });
            AlterColumn("dbo.LibraryItem", "CategoryId", c => c.Int());
            CreateIndex("dbo.LibraryItem", "CategoryId");
            AddForeignKey("dbo.LibraryItem", "CategoryId", "dbo.Category", "Id");
        }
    }
}
