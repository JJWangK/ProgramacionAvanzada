using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Ítems de pedido.
    /// </summary>
    public class OrderItemsController : Controller
    {
        private readonly OrderItemsManager _manager;
        private readonly OrdersManager _ordersManager;
        private readonly BooksManager _booksManager;

        public OrderItemsController()
        {
            _manager = new OrderItemsManager();
            _ordersManager = new OrdersManager();
            _booksManager = new BooksManager();
        }

        public ActionResult Index()
        {
            return View(_manager.GetAll());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = _manager.GetById(id.Value);
            if (entity == null)
            {
                return HttpNotFound();
            }

            return View(entity);
        }

        public ActionResult Create()
        {
            var entity = new OrderItem();
            entity.Quantity = 1;
            entity.Discount = 0;
            PopulateLists(entity);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderItem entity)
        {
            if (entity.OrderId == 0) { ModelState.AddModelError("OrderId", "Seleccione pedido."); }
            if (entity.BookId == 0) { ModelState.AddModelError("BookId", "Seleccione libro."); }
            if (entity.Quantity < 1) { ModelState.AddModelError("Quantity", "La cantidad debe ser al menos 1."); }
            if (entity.UnitPrice < 0) { ModelState.AddModelError("UnitPrice", "El precio unitario no puede ser negativo."); }
            if (entity.Discount < 0) { ModelState.AddModelError("Discount", "El descuento no puede ser negativo."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Create(entity);
            TempData["Mensaje"] = "Ítem de pedido creado correctamente.";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = _manager.GetById(id.Value);
            if (entity == null)
            {
                return HttpNotFound();
            }

            PopulateLists(entity);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderItem entity)
        {
            if (entity.OrderId == 0) { ModelState.AddModelError("OrderId", "Seleccione pedido."); }
            if (entity.BookId == 0) { ModelState.AddModelError("BookId", "Seleccione libro."); }
            if (entity.Quantity < 1) { ModelState.AddModelError("Quantity", "La cantidad debe ser al menos 1."); }
            if (entity.UnitPrice < 0) { ModelState.AddModelError("UnitPrice", "El precio unitario no puede ser negativo."); }
            if (entity.Discount < 0) { ModelState.AddModelError("Discount", "El descuento no puede ser negativo."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Update(entity);
            TempData["Mensaje"] = "Ítem de pedido actualizado correctamente.";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = _manager.GetById(id.Value);
            if (entity == null)
            {
                return HttpNotFound();
            }

            return View(entity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _manager.Delete(id);
                TempData["Mensaje"] = "Ítem de pedido eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

        private void PopulateLists(OrderItem entity)
        {
            ViewBag.OrderIdList = new SelectList(_ordersManager.GetAll(), "Id", "Id", entity == null ? null : (object)entity.OrderId);
            ViewBag.BookIdList = new SelectList(_booksManager.GetAll(), "Id", "Title", entity == null ? null : (object)entity.BookId);
        }

    }
}
