using System.Collections.Generic;

namespace PAP.Generators
{
    /// <summary>
    /// Catálogo de generadores disponibles en la pantalla Admin.
    /// </summary>
    public static class GeneratorCatalog
    {
        /// <summary>
        /// Devuelve los generadores en el mismo orden de la tabla del enunciado.
        /// </summary>
        public static IList<IDataGenerator> CreateAll()
        {
            return new List<IDataGenerator>
            {
                new AddressesGenerator(),
                new AuthorsGenerator(),
                new BookAuthorsGenerator(),
                new BooksGenerator(),
                new CartItemsGenerator(),
                new CategoriesGenerator(),
                new CustomersGenerator(),
                new EmployeesGenerator(),
                new GenresGenerator(),
                new InventoryGenerator(),
                new OrderItemsGenerator(),
                new OrdersGenerator(),
                new PaymentsGenerator(),
                new PublishersGenerator(),
                new PurchaseOrderItemsGenerator(),
                new PurchaseOrdersGenerator(),
                new ReviewsGenerator(),
                new RolesGenerator(),
                new ShoppingCartsGenerator(),
                new SuppliersGenerator()
            };
        }
    }
}
