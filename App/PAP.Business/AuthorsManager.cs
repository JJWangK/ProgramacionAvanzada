using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Autores.
    /// </summary>
    public class AuthorsManager
    {
        private readonly AuthorsRepository _repository;

        public AuthorsManager()
        {
            _repository = new AuthorsRepository();
        }

        public IEnumerable<Author> GetAll()
        {
            return _repository.GetAll();
        }

        public Author GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Author entity)
        {
            _repository.Add(entity);
        }

        public void Update(Author entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
