using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Órdenes de compra.
    /// </summary>
    public class PurchaseOrdersManager
    {
        private readonly PurchaseOrdersRepository _repository;

        public PurchaseOrdersManager()
        {
            _repository = new PurchaseOrdersRepository();
        }

        public IEnumerable<PurchaseOrder> GetAll()
        {
            return _repository.GetAll();
        }

        public PurchaseOrder GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(PurchaseOrder entity)
        {
            _repository.Add(entity);
        }

        public void Update(PurchaseOrder entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
