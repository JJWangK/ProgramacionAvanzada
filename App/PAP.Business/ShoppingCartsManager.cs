using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Carritos.
    /// </summary>
    public class ShoppingCartsManager
    {
        private readonly ShoppingCartsRepository _repository;

        public ShoppingCartsManager()
        {
            _repository = new ShoppingCartsRepository();
        }

        public IEnumerable<ShoppingCart> GetAll()
        {
            return _repository.GetAll();
        }

        public ShoppingCart GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(ShoppingCart entity)
        {
            _repository.Add(entity);
        }

        public void Update(ShoppingCart entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
