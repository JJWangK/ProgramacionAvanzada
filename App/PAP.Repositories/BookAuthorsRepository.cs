using PAP.DataAccess;
using PAP.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PAP.Repositories
{
    /// <summary>
    /// Repositorio de la relación muchos-a-muchos entre libros y autores.
    /// </summary>
    public class BookAuthorsRepository
    {
        private readonly BookStoreDBEntities _context;

        public BookAuthorsRepository()
        {
            _context = new BookStoreDBEntities();
        }

        public IEnumerable<BookAuthor> GetAll()
        {
            return _context.Books
                .SelectMany(b => b.Authors.Select(a => new BookAuthor
                {
                    BookId = b.Id,
                    AuthorId = a.Id,
                    BookTitle = b.Title,
                    AuthorName = a.FirstName + " " + a.LastName
                }))
                .ToList();
        }

        public BookAuthor GetById(int bookId, int authorId)
        {
            return _context.Books
                .Where(b => b.Id == bookId)
                .SelectMany(b => b.Authors
                    .Where(a => a.Id == authorId)
                    .Select(a => new BookAuthor
                    {
                        BookId = b.Id,
                        AuthorId = a.Id,
                        BookTitle = b.Title,
                        AuthorName = a.FirstName + " " + a.LastName
                    }))
                .FirstOrDefault();
        }

        public void Add(BookAuthor entity)
        {
            var book = _context.Books.Find(entity.BookId);
            var author = _context.Authors.Find(entity.AuthorId);

            if (book == null)
            {
                throw new System.InvalidOperationException("El libro seleccionado no existe.");
            }

            if (author == null)
            {
                throw new System.InvalidOperationException("El autor seleccionado no existe.");
            }

            if (book.Authors.Any(a => a.Id == author.Id))
            {
                throw new System.InvalidOperationException("La relación libro-autor ya existe.");
            }

            book.Authors.Add(author);
            _context.SaveChanges();
        }

        public void Update(int oldBookId, int oldAuthorId, int newBookId, int newAuthorId)
        {
            if (oldBookId == newBookId && oldAuthorId == newAuthorId)
            {
                return;
            }

            Remove(oldBookId, oldAuthorId);
            Add(new BookAuthor { BookId = newBookId, AuthorId = newAuthorId });
        }

        public void Delete(int bookId, int authorId)
        {
            Remove(bookId, authorId);
        }

        private void Remove(int bookId, int authorId)
        {
            var book = _context.Books.Include("Authors").FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                return;
            }

            var author = book.Authors.FirstOrDefault(a => a.Id == authorId);
            if (author == null)
            {
                return;
            }

            book.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
