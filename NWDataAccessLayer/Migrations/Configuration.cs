namespace DataAccessLayer.Migrations
{
    using DataAccessLayer.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.Context.NwDBcontext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccessLayer.Context.NwDBcontext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Products.Any())
            {
                var products = new Product[]
               {
                        new Product{ProductName="Chai", UnitPrice=18, UnitsInStock=39  },
                        new Product{ProductName="Chang", UnitPrice=19, UnitsInStock=17   },
                        new Product{ProductName="Aniseed Syrup", UnitPrice=10, UnitsInStock=13   },
                        new Product{ProductName="Chef Anton '\'s Cajun Seasoning", UnitPrice=22, UnitsInStock=53  },
                        new Product{ProductName="Chef Anton'\'s Gumbo Mix", UnitPrice=22, UnitsInStock=53 },
                        new Product{ProductName="Grandma'\'s Boysenberry Spread", UnitPrice=21, UnitsInStock=35  },
                        new Product{ProductName="Uncle Bob'\'s Organic Dried Pears", UnitPrice=30, UnitsInStock=15  },
                        new Product{ProductName="Queso Cabrales", UnitPrice=21, UnitsInStock=22  },
                        new Product{ProductName="Tofu", UnitPrice=23, UnitsInStock=25  },
                        new Product{ProductName="Alice Mutton", UnitPrice=39, UnitsInStock=0  },
                        new Product{ProductName="Schoggi Schokolade", UnitPrice=43, UnitsInStock=9  },
                        new Product{ProductName="Sasquatch Ale", UnitPrice=14, UnitsInStock=111  },
                        new Product{ProductName="Côte de Blaye", UnitPrice=263.5, UnitsInStock=17  },
                        new Product{ProductName="Jack'\'s New England Clam Chowder", UnitPrice=9.65, UnitsInStock=85  },
                        new Product{ProductName="Chocolade", UnitPrice=12.75, UnitsInStock=15  },
                        new Product{ProductName="Ravioli Angelo", UnitPrice=19.5, UnitsInStock=36  }
               };

                foreach (Product product in products)
                {
                    context.Products.Add(product);
                }

                context.SaveChanges();

                var Customers = new Customer[]
               {
                        new Customer{CustomerID="BERGS",ContactName="Christina Berglund",Phone="0921-12 34 65",Address="Berguvsvägen  8"    ,City="Luleå",Region=DBNull.Value.ToString(),PostalCode="S-958 22",Country="Sweden" },
                        new Customer{CustomerID="COMMI",ContactName="Pedro Afonso"      ,Phone="(11) 555-7647",Address="Av. dos Lusíadas, 23",City="Sao Paulo",Region="SP",PostalCode="05432-043",Country="Brazil"  },
                        new Customer{CustomerID="ERNSH",ContactName="Roland Mendel"     ,Phone="7675-3425"    ,Address="Kirchgasse 6" ,City="Graz"  ,Region=DBNull.Value.ToString(),PostalCode="8010"     ,Country="Austria" },
                        new Customer{CustomerID="HANAR",ContactName="Mario Pontes"      ,Phone="(21) 555-0091",Address="Rua do Paço, 67",City="Rio de Janeiro",Region="RJ",PostalCode="05454-876",Country="Brazil" },
                        new Customer{CustomerID="MAGAA",ContactName="Giovanni Rovelli"  ,Phone="035-640230"   ,Address="Via Ludovico il Moro 22",City="Bergamo",Region=DBNull.Value.ToString(),PostalCode="24100",Country="Italy" },
                        new Customer{CustomerID="RATTC",ContactName="Paula Wilson"      ,Phone="(505) 555-5939",Address="2817 Milton Dr.",City="Albuquerque",Region="NM",PostalCode="87110",Country="USA" },
                        new Customer{CustomerID="SUPRD",ContactName="Pascale Cartrain",Phone="(071) 23 67 22 20",Address="Boulevard Tirou, 255",City="Charleroi",Region=DBNull.Value.ToString(),PostalCode="B-6000",Country="Belgium" },
                        new Customer{CustomerID="TOMSP",ContactName="Karin Josephs",Phone="0251-031259",Address="Luisenstr. 48",City="Münster",Region=DBNull.Value.ToString(),PostalCode="44087",Country="Germany" },
                        new Customer{CustomerID="VICTE",ContactName="Mary Saveley",Phone="78.32.54.86",Address="2, rue du Commerce",City="Lyon",Region=DBNull.Value.ToString(),PostalCode="69004",Country="France" },
                        new Customer{CustomerID="VINET",ContactName="Paul Henriot",Phone="26.47.15.10",Address="59 rue de l'Abbaye",City="Reims",Region=DBNull.Value.ToString(),PostalCode="51100",Country="France" }
               };

                Customers.ToList().ForEach(cust => context.Customers.Add(cust));

                context.SaveChanges();

                var orders = new Order[]
                {
                        new Order{CustomerID="VINET",OrderDate=Convert.ToDateTime("7/4/1996", CultureInfo.GetCultureInfo("en-US").DateTimeFormat),ShipName="Vins et alcools Chevalier",ShipAddress="59 rue de l''Abbaye"  },
                        new Order{CustomerID="TOMSP",OrderDate=Convert.ToDateTime("7/5/1996",CultureInfo.GetCultureInfo("en-US").DateTimeFormat),ShipName="Toms Spezialitäten",ShipAddress="Luisenstr. 48"  },
                        new Order{CustomerID="HANAR",OrderDate=Convert.ToDateTime("7/8/1996",CultureInfo.GetCultureInfo("en-US").DateTimeFormat),ShipName="Hanari Carnes",ShipAddress="Rua do Paço, 67" },
                        new Order{CustomerID="VICTE",OrderDate=Convert.ToDateTime("7/8/1996",CultureInfo.GetCultureInfo("en-US").DateTimeFormat),ShipName="Victuailles en stock",ShipAddress="2, rue du Commerce"  },
                        new Order{CustomerID="SUPRD",OrderDate=Convert.ToDateTime("7/9/1996",CultureInfo.GetCultureInfo("en-US").DateTimeFormat),ShipName="Suprêmes délices'",ShipAddress="Boulevard Tirou, 255"  },
                        new Order{CustomerID="HANAR",OrderDate=Convert.ToDateTime("7/10/1996",CultureInfo.GetCultureInfo("en-US").DateTimeFormat),ShipName="Hanari Carnes",ShipAddress="Rua do Paço, 67"  },
                        new Order{CustomerID="ERNSH",OrderDate=Convert.ToDateTime("7/17/1996",CultureInfo.GetCultureInfo("en-US").DateTimeFormat),ShipName="Ernst Handel",ShipAddress="Kirchgasse 6"  },
                        new Order{CustomerID="RATTC",OrderDate=Convert.ToDateTime("7/22/1996",CultureInfo.GetCultureInfo("en-US").DateTimeFormat),ShipName="Rattlesnake Canyon Grocery",ShipAddress="2817 Milton Dr."  },
                        new Order{CustomerID="MAGAA",OrderDate=Convert.ToDateTime("8/7/1996",CultureInfo.GetCultureInfo("en-US").DateTimeFormat),ShipName="Magazzini Alimentari Riuniti",ShipAddress="Via Ludovico il Moro 22"  },
                        new Order{CustomerID="BERGS",OrderDate=Convert.ToDateTime("8/14/1996",CultureInfo.GetCultureInfo("en-US").DateTimeFormat),ShipName="Berglunds snabbköp",ShipAddress="Berguvsvägen"  },
                        new Order{CustomerID="COMMI",OrderDate=Convert.ToDateTime("8/27/1996",CultureInfo.GetCultureInfo("en-US").DateTimeFormat),ShipName="Comércio Mineiro",ShipAddress="Av. dos Lusíadas, 23"  }
                };

                foreach (Order order in orders)
                {
                    context.Orders.Add(order);
                }

                context.SaveChanges();

                var orderdetails = new OrderDetail[]
                {
                        new OrderDetail{ OrderID = 1, ProductID= 1, UnitPrice =14,  Quantity= 12,Discount= 0  },
                        new OrderDetail{ OrderID = 1, ProductID= 2, UnitPrice = 9.8, Quantity= 10,Discount=0 },
                        new OrderDetail{ OrderID = 2, ProductID= 3, UnitPrice = 34.8,Quantity= 5,Discount=0 },
                        new OrderDetail{ OrderID = 2, ProductID= 4, UnitPrice = 18.6,Quantity= 9,Discount=0 },
                        new OrderDetail{ OrderID = 3, ProductID= 6, UnitPrice = 42.4,Quantity= 40,Discount=0 },
                        new OrderDetail{ OrderID = 3, ProductID= 7, UnitPrice = 7.7, Quantity=10,Discount=0 },
                        new OrderDetail{ OrderID = 3, ProductID= 1, UnitPrice = 42.4,Quantity=35,Discount=0.15 },
                        new OrderDetail{ OrderID = 4, ProductID= 13, UnitPrice = 16.8,Quantity=15,Discount=0.15 },
                        new OrderDetail{ OrderID = 4, ProductID= 11, UnitPrice = 16.8,Quantity=6,Discount=0.05 },
                        new OrderDetail{ OrderID = 4, ProductID= 2, UnitPrice = 15.6,Quantity=15,Discount=0.05 },
                        new OrderDetail{ OrderID = 4, ProductID= 8, UnitPrice = 16.8,Quantity=20,Discount=0 },
                        new OrderDetail{ OrderID = 5, ProductID= 12, UnitPrice = 64.8,Quantity=40,Discount=0.05 },
                        new OrderDetail{ OrderID = 5, ProductID= 2, UnitPrice = 2, Quantity=25,Discount=0.05 },
                        new OrderDetail{ OrderID = 6, ProductID= 5, UnitPrice = 27.2,Quantity=40,Discount=0 },
                        new OrderDetail{ OrderID = 7, ProductID= 9, UnitPrice = 10, Quantity=20,Discount=0 },
                        new OrderDetail{ OrderID = 7, ProductID= 6, UnitPrice = 14.4,Quantity=42,Discount=0 },
                        new OrderDetail{ OrderID = 7, ProductID= 16, UnitPrice = 16,Quantity=40,Discount=0 },
                        new OrderDetail{ OrderID = 6, ProductID= 16, UnitPrice = 15.2,Quantity=50,Discount=0.2  },
                        new OrderDetail{ OrderID = 6, ProductID= 15, UnitPrice = 17,Quantity=65,Discount=0.2 },
                        new OrderDetail{ OrderID = 8, ProductID= 14, UnitPrice = 25.6,Quantity=6,Discount=0.2  },
                        new OrderDetail{ OrderID = 5, ProductID= 13, UnitPrice = 17,Quantity=12,Discount=0.2  },
                        new OrderDetail{ OrderID = 5, ProductID= 7, UnitPrice = 24,Quantity=15,Discount=0  },
                        new OrderDetail{ OrderID = 8, ProductID= 16, UnitPrice = 30.4,Quantity=2,Discount=0  },
                        new OrderDetail{ OrderID = 8, ProductID= 10, UnitPrice = 3.6,Quantity=12,Discount=0.05  },
                        new OrderDetail{ OrderID = 8, ProductID= 8, UnitPrice = 44,Quantity=6,Discount=0.05  },
                        new OrderDetail{ OrderID = 11,ProductID= 9, UnitPrice = 3.6,Quantity=12,Discount=0  },
                        new OrderDetail{ OrderID = 11,ProductID= 10, UnitPrice = 19.2,Quantity=20,Discount=0  },
                        new OrderDetail{ OrderID = 11,ProductID= 5, UnitPrice = 6.2,Quantity=30,Discount=0  },
                        new OrderDetail{ OrderID = 9, ProductID= 10, UnitPrice = 17,Quantity=20,Discount=0  },
                        new OrderDetail{ OrderID = 9, ProductID= 1, UnitPrice = 99,Quantity=15,Discount=0  },
                        new OrderDetail{ OrderID = 10,ProductID= 9, UnitPrice = 16,Quantity=15,Discount=0  },
                        new OrderDetail{ OrderID = 10,ProductID= 8, UnitPrice = 10.4,Quantity=10,Discount=0 }
                };

                foreach (OrderDetail orddetails in orderdetails)
                {
                    context.OrderDetails.Add(orddetails);
                }

                context.SaveChanges();
            }
            else
            {
                return; // DB has been seeded
            }
        }
    }
}
