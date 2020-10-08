namespace ConsidLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsCEO = c.Boolean(nullable: false),
                        IsManager = c.Boolean(nullable: false),
                        ManagerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LibraryItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(),
                        CategoryName = c.String(),
                        Title = c.String(),
                        Author = c.String(),
                        Pages = c.Int(),
                        RunTimeMinutes = c.Int(),
                        IsBorrowable = c.Boolean(nullable: false),
                        Borrower = c.String(),
                        BorrowDate = c.DateTime(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LibraryItem", "CategoryId", "dbo.Category");
            DropIndex("dbo.LibraryItem", new[] { "CategoryId" });
            DropTable("dbo.LibraryItem");
            DropTable("dbo.Employee");
            DropTable("dbo.Category");
        }
    }
}
