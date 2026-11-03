using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Órdenes de compra.
    /// </summary>
    public class PurchaseOrdersController : Controller
    {
        private readonly PurchaseOrdersManager _manager;
        private readonly SuppliersManager _suppliersManager;

        public PurchaseOrdersController()
        {
            _manager = new PurchaseOrdersManager();
            _suppliersManager = new SuppliersManager();
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
            var entity = new PurchaseOrder();
            entity.OrderDate = DateTime.Now;
            entity.Status = "Pendiente";
            PopulateLists(entity);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PurchaseOrder entity)
        {
            if (entity.SupplierId == 0) { ModelState.AddModelError("SupplierId", "Seleccione proveedor."); }
            if (string.IsNullOrWhiteSpace(entity.Status)) { ModelState.AddModelError("Status", "El campo Estado es obligatorio."); }
            if (entity.OrderDate == DateTime.MinValue) { ModelState.AddModelError("OrderDate", "Seleccione una fecha válida."); }
            if (entity.TotalCost < 0) { ModelState.AddModelError("TotalCost", "El costo total no puede ser negativo."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Create(entity);
            TempData["Mensaje"] = "Orden de compra creado correctamente.";
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
        public ActionResult Edit(PurchaseOrder entity)
        {
            if (entity.SupplierId == 0) { ModelState.AddModelError("SupplierId", "Seleccione proveedor."); }
            if (string.IsNullOrWhiteSpace(entity.Status)) { ModelState.AddModelError("Status", "El campo Estado es obligatorio."); }
            if (entity.OrderDate == DateTime.MinValue) { ModelState.AddModelError("OrderDate", "Seleccione una fecha válida."); }
            if (entity.TotalCost < 0) { ModelState.AddModelError("TotalCost", "El costo total no puede ser negativo."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Update(entity);
            TempData["Mensaje"] = "Orden de compra actualizado correctamente.";
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
                TempData["Mensaje"] = "Orden de compra eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

        private void PopulateLists(PurchaseOrder entity)
        {
            ViewBag.SupplierIdList = new SelectList(_suppliersManager.GetAll(), "Id", "Name", entity == null ? null : (object)entity.SupplierId);
        }

    }
}
