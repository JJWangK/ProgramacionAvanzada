using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Reseñas.
    /// </summary>
    public class ReviewsController : Controller
    {
        private readonly ReviewsManager _manager;
        private readonly BooksManager _booksManager;
        private readonly CustomersManager _customersManager;

        public ReviewsController()
        {
            _manager = new ReviewsManager();
            _booksManager = new BooksManager();
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
            var entity = new Review();
            entity.CreatedAt = DateTime.Now;
            entity.Rating = 5;
            PopulateLists(entity);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Review entity)
        {
            if (entity.BookId == 0) { ModelState.AddModelError("BookId", "Seleccione libro."); }
            if (entity.CustomerId == 0) { ModelState.AddModelError("CustomerId", "Seleccione cliente."); }
            if (entity.Rating < 1 || entity.Rating > 5) { ModelState.AddModelError("Rating", "La calificación debe estar entre 1 y 5."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            entity.CreatedAt = DateTime.Now;
            _manager.Create(entity);
            TempData["Mensaje"] = "Reseña creado correctamente.";
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
        public ActionResult Edit(Review entity)
        {
            if (entity.BookId == 0) { ModelState.AddModelError("BookId", "Seleccione libro."); }
            if (entity.CustomerId == 0) { ModelState.AddModelError("CustomerId", "Seleccione cliente."); }
            if (entity.Rating < 1 || entity.Rating > 5) { ModelState.AddModelError("Rating", "La calificación debe estar entre 1 y 5."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            _manager.Update(entity);
            TempData["Mensaje"] = "Reseña actualizado correctamente.";
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
                TempData["Mensaje"] = "Reseña eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

        private void PopulateLists(Review entity)
        {
            ViewBag.BookIdList = new SelectList(_booksManager.GetAll(), "Id", "Title", entity == null ? null : (object)entity.BookId);
            ViewBag.CustomerIdList = new SelectList(_customersManager.GetAll(), "Id", "Email", entity == null ? null : (object)entity.CustomerId);
        }

    }
}
