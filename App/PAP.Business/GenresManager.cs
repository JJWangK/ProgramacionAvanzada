using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Géneros.
    /// </summary>
    public class GenresManager
    {
        private readonly GenresRepository _repository;

        public GenresManager()
        {
            _repository = new GenresRepository();
        }

        public IEnumerable<Genre> GetAll()
        {
            return _repository.GetAll();
        }

        public Genre GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Genre entity)
        {
            _repository.Add(entity);
        }

        public void Update(Genre entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
