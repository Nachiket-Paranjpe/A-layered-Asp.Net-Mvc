
using DataAccessLayer.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace DataAccessLayer.Configuration
{
   public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration(DbModelBuilder builder)
        {
            Configure(builder);
        }

        internal void Configure(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Product>().ToTable("Product");
            modelbuilder.Entity<Product>().HasKey(p => p.ProductID);
            modelbuilder.Entity<Product>().Property(p => p.ProductID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelbuilder.Entity<Product>().Property(p => p.RowVersion).IsRowVersion();
            modelbuilder.Entity<Product>().Property(p => p.RowVersion).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);
        }
    }

    class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration(DbModelBuilder builder)
        {
            Configure(builder);
        }
        internal void Configure(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Order>().ToTable("Order");
            modelbuilder.Entity<Order>().HasKey(o => o.OrderID);
            modelbuilder.Entity<Order>().Property(o => o.OrderID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelbuilder.Entity<Order>().Property(p => p.RowVersion).IsRowVersion();
            modelbuilder.Entity<Order>().Property(p => p.RowVersion).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);
        }
    }

    class OrderDetailsConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailsConfiguration(DbModelBuilder builder)
        {
            Configure(builder);
        }
        internal void Configure(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<OrderDetail>().ToTable("Order_Detail");
            modelbuilder.Entity<OrderDetail>().HasKey(ord => new { ord.OrderID, ord.ProductID });
            modelbuilder.Entity<OrderDetail>().Property(p => p.RowVersion).IsRowVersion();
            modelbuilder.Entity<OrderDetail>().Property(p => p.RowVersion).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);
        }
    }

    class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration(DbModelBuilder builder)
        {
            Configure(builder);
        }
        internal void Configure(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Customer>().ToTable("Customer");
            modelbuilder.Entity<Customer>().Property(p => p.RowVersion).IsRowVersion();
            modelbuilder.Entity<Customer>().Property(p => p.RowVersion).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);
        }
    }
}
