using PAP.Business;
using PAP.DataAccess;
using PAP.Models;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;

namespace PAP.Mvc.Controllers
{
    /// <summary>
    /// Controlador de la relación muchos-a-muchos entre libros y autores.
    /// </summary>
    public class BookAuthorsController : Controller
    {
        private readonly BookAuthorsManager _manager;

        public BookAuthorsController()
        {
            _manager = new BookAuthorsManager();
        }

        public ActionResult Index()
        {
            return View(_manager.GetAll());
        }

        public ActionResult Details(int bookId, int authorId)
        {
            var entity = _manager.GetById(bookId, authorId);
            if (entity == null)
            {
                return HttpNotFound();
            }

            return View(entity);
        }

        public ActionResult Create()
        {
            PopulateLists(null);
            return View(new BookAuthor());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthor entity)
        {
            if (entity.BookId == 0) { ModelState.AddModelError("BookId", "Seleccione un libro."); }
            if (entity.AuthorId == 0) { ModelState.AddModelError("AuthorId", "Seleccione un autor."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                return View(entity);
            }

            try
            {
                _manager.Create(entity);
                TempData["Mensaje"] = "Relación libro-autor creada correctamente.";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                PopulateLists(entity);
                return View(entity);
            }
        }

        public ActionResult Edit(int bookId, int authorId)
        {
            var entity = _manager.GetById(bookId, authorId);
            if (entity == null)
            {
                return HttpNotFound();
            }

            PopulateLists(entity);
            ViewBag.OldBookId = bookId;
            ViewBag.OldAuthorId = authorId;
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthor entity, int oldBookId, int oldAuthorId)
        {
            if (entity.BookId == 0) { ModelState.AddModelError("BookId", "Seleccione un libro."); }
            if (entity.AuthorId == 0) { ModelState.AddModelError("AuthorId", "Seleccione un autor."); }

            if (!ModelState.IsValid)
            {
                PopulateLists(entity);
                ViewBag.OldBookId = oldBookId;
                ViewBag.OldAuthorId = oldAuthorId;
                return View(entity);
            }

            try
            {
                _manager.Update(oldBookId, oldAuthorId, entity.BookId, entity.AuthorId);
                TempData["Mensaje"] = "Relación libro-autor actualizada correctamente.";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                PopulateLists(entity);
                ViewBag.OldBookId = oldBookId;
                ViewBag.OldAuthorId = oldAuthorId;
                return View(entity);
            }
        }

        public ActionResult Delete(int bookId, int authorId)
        {
            var entity = _manager.GetById(bookId, authorId);
            if (entity == null)
            {
                return HttpNotFound();
            }

            return View(entity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int bookId, int authorId)
        {
            try
            {
                _manager.Delete(bookId, authorId);
                TempData["Mensaje"] = "Relación libro-autor eliminada correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar la relación libro-autor.";
            }

            return RedirectToAction("Index");
        }

        private void PopulateLists(BookAuthor entity)
        {
            ViewBag.BookIdList = new SelectList(_manager.GetBooks(), "Id", "Title", entity == null ? null : (object)entity.BookId);
            ViewBag.AuthorIdList = new SelectList(_manager.GetAuthors(), "Id", "FirstName", entity == null ? null : (object)entity.AuthorId);
        }
    }
}
