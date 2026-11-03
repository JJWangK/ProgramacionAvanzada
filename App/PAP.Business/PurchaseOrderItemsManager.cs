using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Ítems de orden de compra.
    /// </summary>
    public class PurchaseOrderItemsManager
    {
        private readonly PurchaseOrderItemsRepository _repository;

        public PurchaseOrderItemsManager()
        {
            _repository = new PurchaseOrderItemsRepository();
        }

        public IEnumerable<PurchaseOrderItem> GetAll()
        {
            return _repository.GetAll();
        }

        public PurchaseOrderItem GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(PurchaseOrderItem entity)
        {
            _repository.Add(entity);
        }

        public void Update(PurchaseOrderItem entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
