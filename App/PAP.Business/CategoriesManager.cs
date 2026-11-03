using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Categorías.
    /// </summary>
    public class CategoriesManager
    {
        private readonly CategoriesRepository _repository;

        public CategoriesManager()
        {
            _repository = new CategoriesRepository();
        }

        public IEnumerable<Category> GetAll()
        {
            return _repository.GetAll();
        }

        public Category GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Category entity)
        {
            _repository.Add(entity);
        }

        public void Update(Category entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
