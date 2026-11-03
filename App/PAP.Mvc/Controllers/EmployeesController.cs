using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Empleados.
    /// </summary>
    public class EmployeesController : Controller
    {
        private readonly EmployeesManager _manager;
        private readonly RolesManager _rolesManager;

        public EmployeesController()
        {
            _manager = new EmployeesManager();
            _rolesManager = new RolesManager();
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
            var entity = new Employee();
            entity.CreatedAt = DateTime.Now;
            entity.IsActive = true;
            PopulateLists(entity);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee entity)
        {
            if (entity.RoleId == 0) { ModelState.AddModelError("RoleId", "Seleccione rol."); }
            if (string.IsNullOrWhiteSpace(entity.FirstName)) { ModelState.AddModelError("FirstName", "El campo Nombre es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.LastName)) { ModelState.AddModelError("LastName", "El campo Apellido es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.Email)) { ModelState.AddModelError("Email", "El campo Correo es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.PasswordHash)) { ModelState.AddModelError("PasswordHash", "El campo Contraseña es obligatorio."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            entity.CreatedAt = DateTime.Now;
            _manager.Create(entity);
            TempData["Mensaje"] = "Empleado creado correctamente.";
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
        public ActionResult Edit(Employee entity)
        {
            if (entity.RoleId == 0) { ModelState.AddModelError("RoleId", "Seleccione rol."); }
            if (string.IsNullOrWhiteSpace(entity.FirstName)) { ModelState.AddModelError("FirstName", "El campo Nombre es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.LastName)) { ModelState.AddModelError("LastName", "El campo Apellido es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.Email)) { ModelState.AddModelError("Email", "El campo Correo es obligatorio."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            if (string.IsNullOrWhiteSpace(entity.PasswordHash))
            {
                var existing = new EmployeesManager().GetById(entity.Id);
                if (existing != null) { entity.PasswordHash = existing.PasswordHash; }
            }
            _manager.Update(entity);
            TempData["Mensaje"] = "Empleado actualizado correctamente.";
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
                TempData["Mensaje"] = "Empleado eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

        private void PopulateLists(Employee entity)
        {
            ViewBag.RoleIdList = new SelectList(_rolesManager.GetAll(), "Id", "Name", entity == null ? null : (object)entity.RoleId);
        }

    }
}
