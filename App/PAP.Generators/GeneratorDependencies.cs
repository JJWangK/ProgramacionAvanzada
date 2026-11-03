using System;
using System.Collections.Generic;

namespace PAP.Generators
{
    /// <summary>
    /// Dependencias entre tablas para habilitar los botones de generación.
    /// </summary>
    internal static class GeneratorDependencies
    {
        private static readonly IDictionary<string, string[]> Map =
            new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
            {
                { "dbo.Addresses", new[] { "dbo.Customers" } },
                { "dbo.Authors", new string[0] },
                { "dbo.BookAuthors", new[] { "dbo.Books", "dbo.Authors" } },
                { "dbo.Books", new[] { "dbo.Publishers", "dbo.Categories", "dbo.Genres" } },
                { "dbo.CartItems", new[] { "dbo.ShoppingCarts", "dbo.Books" } },
                { "dbo.Categories", new string[0] },
                { "dbo.Customers", new string[0] },
                { "dbo.Employees", new[] { "dbo.Roles" } },
                { "dbo.Genres", new string[0] },
                { "dbo.Inventory", new[] { "dbo.Books" } },
                { "dbo.OrderItems", new[] { "dbo.Orders", "dbo.Books" } },
                { "dbo.Orders", new[] { "dbo.Customers", "dbo.Addresses" } },
                { "dbo.Payments", new[] { "dbo.Orders" } },
                { "dbo.Publishers", new string[0] },
                { "dbo.PurchaseOrderItems", new[] { "dbo.PurchaseOrders", "dbo.Books" } },
                { "dbo.PurchaseOrders", new[] { "dbo.Suppliers" } },
                { "dbo.Reviews", new[] { "dbo.Books", "dbo.Customers" } },
                { "dbo.Roles", new string[0] },
                { "dbo.ShoppingCarts", new[] { "dbo.Customers" } },
                { "dbo.Suppliers", new string[0] }
            };

        /// <summary>
        /// Devuelve las tablas de las que depende la tabla indicada.
        /// </summary>
        public static IList<string> GetDependencies(string tableName)
        {
            string[] dependencies;
            return Map.TryGetValue(tableName, out dependencies) ? dependencies : new string[0];
        }
    }
}
