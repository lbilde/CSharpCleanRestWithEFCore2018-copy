using System;
using CustomerApp.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Infrastructure.Data
{
    public class DBInitializerProd
    {
        public static void SeedDB(CustomerAppContext ctx)
        {
            ctx.Database.EnsureCreated();
            ctx.Database.ExecuteSqlCommand("DROP TABLE IF EXISTS " +
                                           "dbo.OrderLines, " +
                                           "dbo.Orders, " +
                                           "dbo.Products, " +
                                           "dbo.Customers, " +
                                           "dbo.CustomerTypes, " +
                                           "dbo.AspNetUserClaims, " +
                                           "dbo.AspNetUserLogins, " +
                                           "dbo.AspNetUserRoles, " +
                                           "dbo.AspNetUserTokens, " +
                                           "dbo.AspNetUsers, " +
                                           "dbo.AspNetRoleClaims," +
                                           "dbo.AspNetRoles, " +
                                           "dbo.users, " +
                                           "dbo.roles");
            
            ctx.Database.ExecuteSqlCommand("INSERT INTO dbo.CustomerTypes ( name ) " +
                                           "VALUES " +
                                           "('Guest'), " +
                                           "('VIP'), " +
                                           "('Rich'), " +
                                           "('Soo Poor');");

            ctx.Database.ExecuteSqlCommand("INSERT INTO dbo.Customers " +
                                           "( firstName, lastName, address, typeId) " +
                                           "VALUES " +
                                           "('Bill1', 'Billson1', 'StreetRoad 122', 1), " +
                                           "('Bill2', 'Billson2', 'StreetRoad 222', 2), " +
                                           "('Bill3', 'Billson3', 'StreetRoad 322', 3), " +
                                           "('Bill4', 'Billson4', 'StreetRoad 422', 1), " +
                                           "('Bill5', 'Billson5', 'StreetRoad 522', 1), " +
                                           "('Bill6', 'Billson6', 'StreetRoad 622', 2), " +
                                           "('Bill7', 'Billson7', 'StreetRoad 722', 1), " +
                                           "('Bill8', 'Billson8', 'StreetRoad 822', 1), " +
                                           "('Bill9', 'Billson9', 'StreetRoad 922', 3), " +
                                           "('Bill10', 'Billson10', 'StreetRoad 1022', 4), " +
                                           "('Bill11', 'Billson11', 'StreetRoad 1122', 4), " +
                                           "('Bill12', 'Billson12', 'StreetRoad 1222', 3);");
            ctx.SaveChanges();
        }
    }
}