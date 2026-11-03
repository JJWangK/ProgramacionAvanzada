using PAP.Business;
using PAP.Mvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Totales reales de cada tabla para el panel de inicio.
            var totals = new Dictionary<string, int>
            {
                { "Books", new BooksManager().GetAll().Count() },
                { "Authors", new AuthorsManager().GetAll().Count() },
                { "Categories", new CategoriesManager().GetAll().Count() },
                { "Genres", new GenresManager().GetAll().Count() },
                { "Publishers", new PublishersManager().GetAll().Count() },
                { "BookAuthors", new BookAuthorsManager().GetAll().Count() },
                { "Customers", new CustomersManager().GetAll().Count() },
                { "Addresses", new AddressesManager().GetAll().Count() },
                { "ShoppingCarts", new ShoppingCartsManager().GetAll().Count() },
                { "CartItems", new CartItemsManager().GetAll().Count() },
                { "Orders", new OrdersManager().GetAll().Count() },
                { "OrderItems", new OrderItemsManager().GetAll().Count() },
                { "Payments", new PaymentsManager().GetAll().Count() },
                { "Reviews", new ReviewsManager().GetAll().Count() },
                { "Suppliers", new SuppliersManager().GetAll().Count() },
                { "PurchaseOrders", new PurchaseOrdersManager().GetAll().Count() },
                { "PurchaseOrderItems", new PurchaseOrderItemsManager().GetAll().Count() },
                { "Inventory", new InventoryManager().GetAll().Count() },
                { "Roles", new RolesManager().GetAll().Count() },
                { "Employees", new EmployeesManager().GetAll().Count() }
            };

            return View(new DashboardViewModel { Totals = totals });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
