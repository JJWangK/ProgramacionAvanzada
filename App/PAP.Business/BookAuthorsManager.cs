using PAP.DataAccess;
using PAP.Models;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de la relación libro-autor.
    /// </summary>
    public class BookAuthorsManager
    {
        private readonly BookAuthorsRepository _repository;
        private readonly BooksRepository _booksRepository;
        private readonly AuthorsRepository _authorsRepository;

        public BookAuthorsManager()
        {
            _repository = new BookAuthorsRepository();
            _booksRepository = new BooksRepository();
            _authorsRepository = new AuthorsRepository();
        }

        public IEnumerable<BookAuthor> GetAll()
        {
            return _repository.GetAll();
        }

        public BookAuthor GetById(int bookId, int authorId)
        {
            return _repository.GetById(bookId, authorId);
        }

        public void Create(BookAuthor entity)
        {
            _repository.Add(entity);
        }

        public void Update(int oldBookId, int oldAuthorId, int newBookId, int newAuthorId)
        {
            _repository.Update(oldBookId, oldAuthorId, newBookId, newAuthorId);
        }

        public void Delete(int bookId, int authorId)
        {
            _repository.Delete(bookId, authorId);
        }

        public IEnumerable<PAP.DataAccess.Book> GetBooks()
        {
            return _booksRepository.GetAll();
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _authorsRepository.GetAll();
        }
    }
}
