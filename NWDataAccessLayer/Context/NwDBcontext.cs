using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public partial class NwDBcontext : DbContext , IDbcontext
    {
     public NwDBcontext()
            : base(nameOrConnectionString:"EF6-NorthwindDB")
        { }     
     public new IDbSet<T> Set<T> () where T:class
        {
            return base.Set<T>();
        }       
     public DbSet<Product> Products { get; set; }
     public DbSet<Order> Orders { get; set; }
     public DbSet<OrderDetail> OrderDetails { get; set; }
     public DbSet<Customer> Customers { get; set; }
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Product>().HasKey(p => p.ProductID);
            modelBuilder.Entity<Product>().Property(p => p.ProductID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Product>().Property(p => p.RowVersion).IsRowVersion();
            modelBuilder.Entity<Product>().Property(p => p.RowVersion).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);

            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Order>().HasKey(o => o.OrderID);
            modelBuilder.Entity<Order>().Property(o => o.OrderID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Order>().Property(p => p.RowVersion).IsRowVersion();
            modelBuilder.Entity<Order>().Property(p => p.RowVersion).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);

            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail");
            modelBuilder.Entity<OrderDetail>().HasKey(ord => new { ord.OrderID, ord.ProductID });
            modelBuilder.Entity<OrderDetail>().Property(p => p.RowVersion).IsRowVersion();
            modelBuilder.Entity<OrderDetail>().Property(p => p.RowVersion).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);

            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Customer>().HasKey(cust => cust.CustomerID);
            modelBuilder.Entity<Customer>().Property(p => p.RowVersion).IsRowVersion();
            modelBuilder.Entity<Customer>().Property(p => p.RowVersion).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);

            // Below code should not be used till it is further analysed
            //modelBuilder.Configurations.Add(new OrderConfiguration(modelBuilder));
            //modelBuilder.Configurations.Add(new ProductConfiguration(modelBuilder));
            //modelBuilder.Configurations.Add(new OrderDetailsConfiguration(modelBuilder));
            //modelBuilder.Configurations.Add(new CustomerConfiguration(modelBuilder));
        }   
    public int Savechanges()
        {
            return base.SaveChanges();
        }

    }
}
