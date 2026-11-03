using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Clientes.
    /// </summary>
    public class CustomersController : Controller
    {
        private readonly CustomersManager _manager;

        public CustomersController()
        {
            _manager = new CustomersManager();

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
            var entity = new Customer();
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            entity.IsActive = true;

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer entity)
        {
            if (string.IsNullOrWhiteSpace(entity.FirstName)) { ModelState.AddModelError("FirstName", "El campo Nombre es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.LastName)) { ModelState.AddModelError("LastName", "El campo Apellido es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.Email)) { ModelState.AddModelError("Email", "El campo Correo es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.PasswordHash)) { ModelState.AddModelError("PasswordHash", "El campo Contraseña es obligatorio."); }

            if (!ModelState.IsValid)
            {

                return View(entity);
            }

            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            _manager.Create(entity);
            TempData["Mensaje"] = "Cliente creado correctamente.";
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

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer entity)
        {
            if (string.IsNullOrWhiteSpace(entity.FirstName)) { ModelState.AddModelError("FirstName", "El campo Nombre es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.LastName)) { ModelState.AddModelError("LastName", "El campo Apellido es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.Email)) { ModelState.AddModelError("Email", "El campo Correo es obligatorio."); }

            if (!ModelState.IsValid)
            {

                return View(entity);
            }

            entity.UpdatedAt = DateTime.Now;
            if (string.IsNullOrWhiteSpace(entity.PasswordHash))
            {
                var existing = new CustomersManager().GetById(entity.Id);
                if (existing != null) { entity.PasswordHash = existing.PasswordHash; }
            }
            _manager.Update(entity);
            TempData["Mensaje"] = "Cliente actualizado correctamente.";
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
                TempData["Mensaje"] = "Cliente eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

    }
}
