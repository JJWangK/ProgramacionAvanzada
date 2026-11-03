using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Pedidos.
    /// </summary>
    public class OrdersManager
    {
        private readonly OrdersRepository _repository;

        public OrdersManager()
        {
            _repository = new OrdersRepository();
        }

        public IEnumerable<Order> GetAll()
        {
            return _repository.GetAll();
        }

        public Order GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Order entity)
        {
            _repository.Add(entity);
        }

        public void Update(Order entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
