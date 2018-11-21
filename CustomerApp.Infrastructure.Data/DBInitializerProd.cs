using System;
using System.Collections.Generic;
using CustomerApp.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Infrastructure.Data
{
    public class DBInitializerProd
    {
        public static void SeedDB(CustomerAppContext ctx)
        {    
            ctx.Database.EnsureCreated();

            /*ctx.Database.ExecuteSqlCommand("DROP TABLE IF EXISTS " +
                                           "dbo.OrderLines, dbo.Orders");
            ctx.SaveChanges();*/
            ctx.Database.EnsureCreated();

            var customerTypes = new List<CustomerType>()
            {
                new CustomerType(){ Name = "Guest" },
                new CustomerType(){ Name = "VIP" },
                new CustomerType(){ Name = "Rich" },
                new CustomerType(){ Name = "Soo Poor" },
                new CustomerType(){ Name = "Fun" }
            };
            ctx.AddRange(customerTypes);
            ctx.SaveChanges();
            
            var customers = new List<Customer>()
            {
                new Customer(){ FirstName = "Bill1", LastName = "Billson1", Address= "StreetRoad 1122", Type = new CustomerType(){Id = 1}},
                new Customer(){ FirstName = "Bill2", LastName = "Billson2", Address= "StreetRoad 2122", Type = new CustomerType(){Id = 2}},
                new Customer(){ FirstName = "Bill3", LastName = "Billson3", Address= "StreetRoad 3122", Type = new CustomerType(){Id = 3}},
                new Customer(){ FirstName = "Bill4", LastName = "Billson4", Address= "StreetRoad 4122", Type = new CustomerType(){Id = 3}},
                new Customer(){ FirstName = "Bill5", LastName = "Billson5", Address= "StreetRoad 5122", Type = new CustomerType(){Id = 2}},
                new Customer(){ FirstName = "Bill6", LastName = "Billson6", Address= "StreetRoad 6122", Type = new CustomerType(){Id = 4}},
                new Customer(){ FirstName = "Bill7", LastName = "Billson7", Address= "StreetRoad 7122", Type = new CustomerType(){Id = 5}},
                new Customer(){ FirstName = "Bill8", LastName = "Billson8", Address= "StreetRoad 8122", Type = new CustomerType(){Id = 3}},
                new Customer(){ FirstName = "Bill9", LastName = "Billson9", Address= "StreetRoad 9122", Type = new CustomerType(){Id = 2}},
                new Customer(){ FirstName = "Bill10", LastName = "Billson10", Address= "StreetRoad 10122", Type = new CustomerType(){Id = 5}},
                new Customer(){ FirstName = "Bill11", LastName = "Billson11", Address= "StreetRoad 11122", Type = new CustomerType(){Id = 5}},
                new Customer(){ FirstName = "Bill12", LastName = "Billson12", Address= "StreetRoad 12122", Type = new CustomerType(){Id = 4}},
                new Customer(){ FirstName = "Bill13", LastName = "Billson13", Address= "StreetRoad 13122", Type = new CustomerType(){Id = 4}},
                
            };
            
            ctx.AddRange(customers);

            ctx.SaveChanges();
        }
    }
}