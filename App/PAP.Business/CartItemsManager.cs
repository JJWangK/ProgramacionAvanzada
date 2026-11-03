using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Ítems de carrito.
    /// </summary>
    public class CartItemsManager
    {
        private readonly CartItemsRepository _repository;

        public CartItemsManager()
        {
            _repository = new CartItemsRepository();
        }

        public IEnumerable<CartItem> GetAll()
        {
            return _repository.GetAll();
        }

        public CartItem GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(CartItem entity)
        {
            _repository.Add(entity);
        }

        public void Update(CartItem entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
