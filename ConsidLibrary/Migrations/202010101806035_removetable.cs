namespace ConsidLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removetable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LibraryItem", "LibraryTypes_LibraryTypesID", "dbo.LibraryTypes");
            DropIndex("dbo.LibraryItem", new[] { "LibraryTypes_LibraryTypesID" });
            DropColumn("dbo.LibraryItem", "LibraryTypes_LibraryTypesID");
            DropTable("dbo.LibraryTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LibraryTypes",
                c => new
                    {
                        LibraryTypesID = c.Int(nullable: false, identity: true),
                        Book = c.String(),
                        AudioBook = c.String(),
                        DVD = c.String(),
                        ReferenceBook = c.String(),
                    })
                .PrimaryKey(t => t.LibraryTypesID);
            
            AddColumn("dbo.LibraryItem", "LibraryTypes_LibraryTypesID", c => c.Int());
            CreateIndex("dbo.LibraryItem", "LibraryTypes_LibraryTypesID");
            AddForeignKey("dbo.LibraryItem", "LibraryTypes_LibraryTypesID", "dbo.LibraryTypes", "LibraryTypesID");
        }
    }
}
