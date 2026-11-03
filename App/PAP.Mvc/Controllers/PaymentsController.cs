using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Pagos.
    /// </summary>
    public class PaymentsController : Controller
    {
        private readonly PaymentsManager _manager;
        private readonly OrdersManager _ordersManager;

        public PaymentsController()
        {
            _manager = new PaymentsManager();
            _ordersManager = new OrdersManager();
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
            var entity = new Payment();
            entity.PaymentDate = DateTime.Now;
            entity.Status = "Completado";
            PopulateLists(entity);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Payment entity)
        {
            if (entity.OrderId == 0) { ModelState.AddModelError("OrderId", "Seleccione pedido."); }
            if (string.IsNullOrWhiteSpace(entity.PaymentMethod)) { ModelState.AddModelError("PaymentMethod", "El campo Método de pago es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.Status)) { ModelState.AddModelError("Status", "El campo Estado es obligatorio."); }
            if (entity.Amount <= 0) { ModelState.AddModelError("Amount", "El monto debe ser mayor que cero."); }
            if (entity.PaymentDate == DateTime.MinValue) { ModelState.AddModelError("PaymentDate", "Seleccione una fecha válida."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Create(entity);
            TempData["Mensaje"] = "Pago creado correctamente.";
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
        public ActionResult Edit(Payment entity)
        {
            if (entity.OrderId == 0) { ModelState.AddModelError("OrderId", "Seleccione pedido."); }
            if (string.IsNullOrWhiteSpace(entity.PaymentMethod)) { ModelState.AddModelError("PaymentMethod", "El campo Método de pago es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.Status)) { ModelState.AddModelError("Status", "El campo Estado es obligatorio."); }
            if (entity.Amount <= 0) { ModelState.AddModelError("Amount", "El monto debe ser mayor que cero."); }
            if (entity.PaymentDate == DateTime.MinValue) { ModelState.AddModelError("PaymentDate", "Seleccione una fecha válida."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Update(entity);
            TempData["Mensaje"] = "Pago actualizado correctamente.";
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
                TempData["Mensaje"] = "Pago eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

        private void PopulateLists(Payment entity)
        {
            ViewBag.OrderIdList = new SelectList(_ordersManager.GetAll(), "Id", "Id", entity == null ? null : (object)entity.OrderId);
        }

    }
}
