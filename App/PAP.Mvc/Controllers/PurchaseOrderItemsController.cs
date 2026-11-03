using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Ítems de orden de compra.
    /// </summary>
    public class PurchaseOrderItemsController : Controller
    {
        private readonly PurchaseOrderItemsManager _manager;
        private readonly PurchaseOrdersManager _purchaseOrdersManager;
        private readonly BooksManager _booksManager;

        public PurchaseOrderItemsController()
        {
            _manager = new PurchaseOrderItemsManager();
            _purchaseOrdersManager = new PurchaseOrdersManager();
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
            var entity = new PurchaseOrderItem();
            entity.Quantity = 1;
            PopulateLists(entity);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PurchaseOrderItem entity)
        {
            if (entity.PurchaseOrderId == 0) { ModelState.AddModelError("PurchaseOrderId", "Seleccione orden de compra."); }
            if (entity.BookId == 0) { ModelState.AddModelError("BookId", "Seleccione libro."); }
            if (entity.Quantity < 1) { ModelState.AddModelError("Quantity", "La cantidad debe ser al menos 1."); }
            if (entity.UnitCost < 0) { ModelState.AddModelError("UnitCost", "El costo unitario no puede ser negativo."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Create(entity);
            TempData["Mensaje"] = "Ítem de orden de compra creado correctamente.";
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
        public ActionResult Edit(PurchaseOrderItem entity)
        {
            if (entity.PurchaseOrderId == 0) { ModelState.AddModelError("PurchaseOrderId", "Seleccione orden de compra."); }
            if (entity.BookId == 0) { ModelState.AddModelError("BookId", "Seleccione libro."); }
            if (entity.Quantity < 1) { ModelState.AddModelError("Quantity", "La cantidad debe ser al menos 1."); }
            if (entity.UnitCost < 0) { ModelState.AddModelError("UnitCost", "El costo unitario no puede ser negativo."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Update(entity);
            TempData["Mensaje"] = "Ítem de orden de compra actualizado correctamente.";
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
                TempData["Mensaje"] = "Ítem de orden de compra eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

        private void PopulateLists(PurchaseOrderItem entity)
        {
            ViewBag.PurchaseOrderIdList = new SelectList(_purchaseOrdersManager.GetAll(), "Id", "Id", entity == null ? null : (object)entity.PurchaseOrderId);
            ViewBag.BookIdList = new SelectList(_booksManager.GetAll(), "Id", "Title", entity == null ? null : (object)entity.BookId);
        }

    }
}
