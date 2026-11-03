using PAP.Business;
using PAP.DataAccess;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de Libros.
    /// </summary>
    public class BooksController : Controller
    {
        private readonly BooksManager _manager;
        private readonly PublishersManager _publishersManager;
        private readonly CategoriesManager _categoriesManager;
        private readonly GenresManager _genresManager;

        public BooksController()
        {
            _manager = new BooksManager();
            _publishersManager = new PublishersManager();
            _categoriesManager = new CategoriesManager();
            _genresManager = new GenresManager();
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
            var entity = new Book();
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            entity.IsInStock = true;
            PopulateLists(entity);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name)) { ModelState.AddModelError("Name", "El campo Nombre es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.Title)) { ModelState.AddModelError("Title", "El campo Título es obligatorio."); }
            if (entity.Price <= 0) { ModelState.AddModelError("Price", "El precio debe ser mayor que cero."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            var category = entity.CategoryId.HasValue ? _categoriesManager.GetById(entity.CategoryId.Value) : null;
            var publisher = entity.PublisherId.HasValue ? _publishersManager.GetById(entity.PublisherId.Value) : null;
            var genre = entity.GenreId.HasValue ? _genresManager.GetById(entity.GenreId.Value) : null;
            entity.Category = category != null ? category.Name : null;
            entity.Publisher = publisher != null ? publisher.Name : null;
            entity.Genre = genre != null ? genre.Name : null;
            _manager.Create(entity);
            TempData["Mensaje"] = "Libro creado correctamente.";
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
        public ActionResult Edit(Book entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name)) { ModelState.AddModelError("Name", "El campo Nombre es obligatorio."); }
            if (string.IsNullOrWhiteSpace(entity.Title)) { ModelState.AddModelError("Title", "El campo Título es obligatorio."); }
            if (entity.Price <= 0) { ModelState.AddModelError("Price", "El precio debe ser mayor que cero."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            entity.UpdatedAt = DateTime.Now;
            var category = entity.CategoryId.HasValue ? _categoriesManager.GetById(entity.CategoryId.Value) : null;
            var publisher = entity.PublisherId.HasValue ? _publishersManager.GetById(entity.PublisherId.Value) : null;
            var genre = entity.GenreId.HasValue ? _genresManager.GetById(entity.GenreId.Value) : null;
            entity.Category = category != null ? category.Name : null;
            entity.Publisher = publisher != null ? publisher.Name : null;
            entity.Genre = genre != null ? genre.Name : null;
            _manager.Update(entity);
            TempData["Mensaje"] = "Libro actualizado correctamente.";
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
                TempData["Mensaje"] = "Libro eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar porque tiene registros relacionados.";
            }

            return RedirectToAction("Index");
        }

        private void PopulateLists(Book entity)
        {
            ViewBag.PublisherIdList = new SelectList(_publishersManager.GetAll(), "Id", "Name", entity == null ? null : (object)entity.PublisherId);
            ViewBag.CategoryIdList = new SelectList(_categoriesManager.GetAll(), "Id", "Name", entity == null ? null : (object)entity.CategoryId);
            ViewBag.GenreIdList = new SelectList(_genresManager.GetAll(), "Id", "Name", entity == null ? null : (object)entity.GenreId);
        }

    }
}
