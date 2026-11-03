using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Carritos.
    /// </summary>
    public class ShoppingCartsController : Controller
    {
        private readonly ShoppingCartsManager _manager;
        private readonly CustomersManager _customersManager;

        public ShoppingCartsController()
        {
            _manager = new ShoppingCartsManager();
            _customersManager = new CustomersManager();
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
            var entity = new ShoppingCart();
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            PopulateLists(entity);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShoppingCart entity)
        {
            if (entity.CustomerId == 0) { ModelState.AddModelError("CustomerId", "Seleccione cliente."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            _manager.Create(entity);
            TempData["Mensaje"] = "Carrito creado correctamente.";
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
        public ActionResult Edit(ShoppingCart entity)
        {
            if (entity.CustomerId == 0) { ModelState.AddModelError("CustomerId", "Seleccione cliente."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            entity.UpdatedAt = DateTime.Now;
            _manager.Update(entity);
            TempData["Mensaje"] = "Carrito actualizado correctamente.";
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
                TempData["Mensaje"] = "Carrito eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

        private void PopulateLists(ShoppingCart entity)
        {
            ViewBag.CustomerIdList = new SelectList(_customersManager.GetAll(), "Id", "Email", entity == null ? null : (object)entity.CustomerId);
        }

    }
}
