using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Pedidos.
    /// </summary>
    public class OrdersController : Controller
    {
        private readonly OrdersManager _manager;
        private readonly CustomersManager _customersManager;
        private readonly AddressesManager _addressesManager;

        public OrdersController()
        {
            _manager = new OrdersManager();
            _customersManager = new CustomersManager();
            _addressesManager = new AddressesManager();
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
            var entity = new Order();
            entity.OrderDate = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            entity.Status = "Pendiente";
            PopulateLists(entity);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order entity)
        {
            if (entity.CustomerId == 0) { ModelState.AddModelError("CustomerId", "Seleccione cliente."); }
            if (string.IsNullOrWhiteSpace(entity.Status)) { ModelState.AddModelError("Status", "El campo Estado es obligatorio."); }
            if (entity.OrderDate == DateTime.MinValue) { ModelState.AddModelError("OrderDate", "Seleccione una fecha válida."); }
            if (entity.Subtotal < 0) { ModelState.AddModelError("Subtotal", "El subtotal no puede ser negativo."); }
            if (entity.TaxAmount < 0) { ModelState.AddModelError("TaxAmount", "El impuesto no puede ser negativo."); }
            if (entity.ShippingCost < 0) { ModelState.AddModelError("ShippingCost", "El envío no puede ser negativo."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            entity.UpdatedAt = DateTime.Now;
            entity.Total = entity.Subtotal + entity.TaxAmount + entity.ShippingCost;
            _manager.Create(entity);
            TempData["Mensaje"] = "Pedido creado correctamente.";
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
        public ActionResult Edit(Order entity)
        {
            if (entity.CustomerId == 0) { ModelState.AddModelError("CustomerId", "Seleccione cliente."); }
            if (string.IsNullOrWhiteSpace(entity.Status)) { ModelState.AddModelError("Status", "El campo Estado es obligatorio."); }
            if (entity.OrderDate == DateTime.MinValue) { ModelState.AddModelError("OrderDate", "Seleccione una fecha válida."); }
            if (entity.Subtotal < 0) { ModelState.AddModelError("Subtotal", "El subtotal no puede ser negativo."); }
            if (entity.TaxAmount < 0) { ModelState.AddModelError("TaxAmount", "El impuesto no puede ser negativo."); }
            if (entity.ShippingCost < 0) { ModelState.AddModelError("ShippingCost", "El envío no puede ser negativo."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            entity.UpdatedAt = DateTime.Now;
            entity.Total = entity.Subtotal + entity.TaxAmount + entity.ShippingCost;
            _manager.Update(entity);
            TempData["Mensaje"] = "Pedido actualizado correctamente.";
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
                TempData["Mensaje"] = "Pedido eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

        private void PopulateLists(Order entity)
        {
            ViewBag.CustomerIdList = new SelectList(_customersManager.GetAll(), "Id", "Email", entity == null ? null : (object)entity.CustomerId);
            ViewBag.ShippingAddressIdList = new SelectList(_addressesManager.GetAll(), "Id", "Street", entity == null ? null : (object)entity.ShippingAddressId);
        }

    }
}
