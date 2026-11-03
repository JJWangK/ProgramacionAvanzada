using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Inventario.
    /// </summary>
    public class InventoryManager
    {
        private readonly InventoryRepository _repository;

        public InventoryManager()
        {
            _repository = new InventoryRepository();
        }

        public IEnumerable<Inventory> GetAll()
        {
            return _repository.GetAll();
        }

        public Inventory GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Inventory entity)
        {
            _repository.Add(entity);
        }

        public void Update(Inventory entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
