using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Inventario.
    /// </summary>
    public class InventoryController : Controller
    {
        private readonly InventoryManager _manager;
        private readonly BooksManager _booksManager;

        public InventoryController()
        {
            _manager = new InventoryManager();
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
            var entity = new Inventory();
            entity.LastRestockedAt = DateTime.Now;
            PopulateLists(entity);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inventory entity)
        {
            if (entity.BookId == 0) { ModelState.AddModelError("BookId", "Seleccione libro."); }
            if (entity.QuantityInStock < 0) { ModelState.AddModelError("QuantityInStock", "La cantidad no puede ser negativa."); }
            if (entity.ReorderLevel < 0) { ModelState.AddModelError("ReorderLevel", "El nivel de reposición no puede ser negativo."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Create(entity);
            TempData["Mensaje"] = "Inventario creado correctamente.";
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
        public ActionResult Edit(Inventory entity)
        {
            if (entity.BookId == 0) { ModelState.AddModelError("BookId", "Seleccione libro."); }
            if (entity.QuantityInStock < 0) { ModelState.AddModelError("QuantityInStock", "La cantidad no puede ser negativa."); }
            if (entity.ReorderLevel < 0) { ModelState.AddModelError("ReorderLevel", "El nivel de reposición no puede ser negativo."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Update(entity);
            TempData["Mensaje"] = "Inventario actualizado correctamente.";
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
                TempData["Mensaje"] = "Inventario eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

        private void PopulateLists(Inventory entity)
        {
            ViewBag.BookIdList = new SelectList(_booksManager.GetAll(), "Id", "Title", entity == null ? null : (object)entity.BookId);
        }

    }
}
