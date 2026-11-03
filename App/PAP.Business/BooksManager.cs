using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Libros.
    /// </summary>
    public class BooksManager
    {
        private readonly BooksRepository _repository;

        public BooksManager()
        {
            _repository = new BooksRepository();
        }

        public IEnumerable<Book> GetAll()
        {
            return _repository.GetAll();
        }

        public Book GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Book entity)
        {
            _repository.Add(entity);
        }

        public void Update(Book entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
