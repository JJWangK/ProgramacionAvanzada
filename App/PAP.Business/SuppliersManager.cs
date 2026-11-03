using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Proveedores.
    /// </summary>
    public class SuppliersManager
    {
        private readonly SuppliersRepository _repository;

        public SuppliersManager()
        {
            _repository = new SuppliersRepository();
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _repository.GetAll();
        }

        public Supplier GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Supplier entity)
        {
            _repository.Add(entity);
        }

        public void Update(Supplier entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
