using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Direcciones.
    /// </summary>
    public class AddressesController : Controller
    {
        private readonly AddressesManager _manager;
        private readonly CustomersManager _customersManager;

        public AddressesController()
        {
            _manager = new AddressesManager();
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
            var entity = new Address();

            PopulateLists(entity);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Address entity)
        {
            if (entity.CustomerId == 0) { ModelState.AddModelError("CustomerId", "Seleccione cliente."); }
            if (string.IsNullOrWhiteSpace(entity.AddressType)) { ModelState.AddModelError("AddressType", "El campo Tipo de dirección es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.Street)) { ModelState.AddModelError("Street", "El campo Calle es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.City)) { ModelState.AddModelError("City", "El campo Ciudad es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.PostalCode)) { ModelState.AddModelError("PostalCode", "El campo Código postal es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.Country)) { ModelState.AddModelError("Country", "El campo País es obligatorio."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Create(entity);
            TempData["Mensaje"] = "Dirección creado correctamente.";
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
        public ActionResult Edit(Address entity)
        {
            if (entity.CustomerId == 0) { ModelState.AddModelError("CustomerId", "Seleccione cliente."); }
            if (string.IsNullOrWhiteSpace(entity.AddressType)) { ModelState.AddModelError("AddressType", "El campo Tipo de dirección es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.Street)) { ModelState.AddModelError("Street", "El campo Calle es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.City)) { ModelState.AddModelError("City", "El campo Ciudad es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.PostalCode)) { ModelState.AddModelError("PostalCode", "El campo Código postal es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.Country)) { ModelState.AddModelError("Country", "El campo País es obligatorio."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Update(entity);
            TempData["Mensaje"] = "Dirección actualizado correctamente.";
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
                TempData["Mensaje"] = "Dirección eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

        private void PopulateLists(Address entity)
        {
            ViewBag.CustomerIdList = new SelectList(_customersManager.GetAll(), "Id", "Email", entity == null ? null : (object)entity.CustomerId);
        }

    }
}
