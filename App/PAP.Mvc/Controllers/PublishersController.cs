using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Editoriales.
    /// </summary>
    public class PublishersController : Controller
    {
        private readonly PublishersManager _manager;

        public PublishersController()
        {
            _manager = new PublishersManager();

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
            var entity = new Publisher();
            entity.CreatedAt = DateTime.Now;

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Publisher entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name)) { ModelState.AddModelError("Name", "El campo Nombre es obligatorio."); }

            if (!ModelState.IsValid)
            {

                return View(entity);
            }

            entity.CreatedAt = DateTime.Now;
            _manager.Create(entity);
            TempData["Mensaje"] = "Editorial creado correctamente.";
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
        public ActionResult Edit(Publisher entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name)) { ModelState.AddModelError("Name", "El campo Nombre es obligatorio."); }

            if (!ModelState.IsValid)
            {

                return View(entity);
            }

            _manager.Update(entity);
            TempData["Mensaje"] = "Editorial actualizado correctamente.";
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
                TempData["Mensaje"] = "Editorial eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

    }
}
