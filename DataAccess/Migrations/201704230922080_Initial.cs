namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Gadgets",
                c => new
                    {
                        GadgetId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GadgetId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.GadgetOrders",
                c => new
                    {
                        GadgetOrderId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        GadgetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GadgetOrderId)
                .ForeignKey("dbo.Gadgets", t => t.GadgetId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.GadgetId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        OwnerName = c.String(),
                        Street = c.String(),
                        Zip = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GadgetOrders", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.GadgetOrders", "GadgetId", "dbo.Gadgets");
            DropForeignKey("dbo.Gadgets", "CategoryId", "dbo.Categories");
            DropIndex("dbo.GadgetOrders", new[] { "GadgetId" });
            DropIndex("dbo.GadgetOrders", new[] { "OrderId" });
            DropIndex("dbo.Gadgets", new[] { "CategoryId" });
            DropTable("dbo.Orders");
            DropTable("dbo.GadgetOrders");
            DropTable("dbo.Gadgets");
            DropTable("dbo.Categories");
        }
    }
}
