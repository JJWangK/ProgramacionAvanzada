using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Ítems de pedido.
    /// </summary>
    public class OrderItemsManager
    {
        private readonly OrderItemsRepository _repository;

        public OrderItemsManager()
        {
            _repository = new OrderItemsRepository();
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return _repository.GetAll();
        }

        public OrderItem GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(OrderItem entity)
        {
            _repository.Add(entity);
        }

        public void Update(OrderItem entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
