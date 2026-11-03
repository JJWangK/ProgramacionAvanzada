using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Ítems de carrito.
    /// </summary>
    public class CartItemsController : Controller
    {
        private readonly CartItemsManager _manager;
        private readonly ShoppingCartsManager _shoppingCartsManager;
        private readonly BooksManager _booksManager;

        public CartItemsController()
        {
            _manager = new CartItemsManager();
            _shoppingCartsManager = new ShoppingCartsManager();
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
            var entity = new CartItem();
            entity.AddedAt = DateTime.Now;
            entity.Quantity = 1;
            PopulateLists(entity);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CartItem entity)
        {
            if (entity.CartId == 0) { ModelState.AddModelError("CartId", "Seleccione carrito."); }
            if (entity.BookId == 0) { ModelState.AddModelError("BookId", "Seleccione libro."); }
            if (entity.Quantity < 1) { ModelState.AddModelError("Quantity", "La cantidad debe ser al menos 1."); }
            if (entity.AddedAt == DateTime.MinValue) { ModelState.AddModelError("AddedAt", "Seleccione una fecha válida."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Create(entity);
            TempData["Mensaje"] = "Ítem de carrito creado correctamente.";
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
        public ActionResult Edit(CartItem entity)
        {
            if (entity.CartId == 0) { ModelState.AddModelError("CartId", "Seleccione carrito."); }
            if (entity.BookId == 0) { ModelState.AddModelError("BookId", "Seleccione libro."); }
            if (entity.Quantity < 1) { ModelState.AddModelError("Quantity", "La cantidad debe ser al menos 1."); }
            if (entity.AddedAt == DateTime.MinValue) { ModelState.AddModelError("AddedAt", "Seleccione una fecha válida."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Update(entity);
            TempData["Mensaje"] = "Ítem de carrito actualizado correctamente.";
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
                TempData["Mensaje"] = "Ítem de carrito eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

        private void PopulateLists(CartItem entity)
        {
            ViewBag.CartIdList = new SelectList(_shoppingCartsManager.GetAll(), "Id", "Id", entity == null ? null : (object)entity.CartId);
            ViewBag.BookIdList = new SelectList(_booksManager.GetAll(), "Id", "Title", entity == null ? null : (object)entity.BookId);
        }

    }
}
