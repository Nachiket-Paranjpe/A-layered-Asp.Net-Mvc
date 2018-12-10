namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                {
                    CustomerID = c.String(nullable: false, maxLength: 128),
                    ContactName = c.String(),
                    Phone = c.String(),
                    Address = c.String(),
                    City = c.String(),
                    Region = c.String(),
                    PostalCode = c.String(),
                    Country = c.String(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.CustomerID);

            CreateTable(
                "dbo.Order",
                c => new
                {
                    OrderID = c.Int(nullable: false, identity: true),
                    CustomerID = c.String(maxLength: 128),
                    OrderDate = c.DateTime(nullable: false),
                    ShipName = c.String(),
                    ShipAddress = c.String(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .Index(t => t.CustomerID);

            CreateTable(
                "dbo.OrderDetail",
                c => new
                {
                    OrderID = c.Int(nullable: false),
                    ProductID = c.Int(nullable: false),
                    UnitPrice = c.Double(nullable: false),
                    Quantity = c.Int(nullable: false),
                    Discount = c.Double(nullable: false),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => new { t.OrderID, t.ProductID })
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);

            CreateTable(
                "dbo.Product",
                c => new
                {
                    ProductID = c.Int(nullable: false, identity: true),
                    ProductName = c.String(),
                    UnitPrice = c.Double(nullable: false),
                    UnitsInStock = c.Int(nullable: false),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    SupplierID = c.Int(),
                    CategoryID = c.Int(),
                })
                .PrimaryKey(t => t.ProductID);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropIndex("dbo.OrderDetail", new[] { "ProductID" });
            DropIndex("dbo.OrderDetail", new[] { "OrderID" });
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropTable("dbo.Product");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Order");
            DropTable("dbo.Customer");
        }
    }
}
