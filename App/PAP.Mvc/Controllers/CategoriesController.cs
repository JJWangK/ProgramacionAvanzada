using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Categorías.
    /// </summary>
    public class CategoriesController : Controller
    {
        private readonly CategoriesManager _manager;

        public CategoriesController()
        {
            _manager = new CategoriesManager();

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
            var entity = new Category();

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name)) { ModelState.AddModelError("Name", "El campo Nombre es obligatorio."); }

            if (!ModelState.IsValid)
            {

                return View(entity);
            }

            _manager.Create(entity);
            TempData["Mensaje"] = "Categoría creado correctamente.";
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
        public ActionResult Edit(Category entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name)) { ModelState.AddModelError("Name", "El campo Nombre es obligatorio."); }

            if (!ModelState.IsValid)
            {

                return View(entity);
            }

            _manager.Update(entity);
            TempData["Mensaje"] = "Categoría actualizado correctamente.";
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
                TempData["Mensaje"] = "Categoría eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

    }
}
