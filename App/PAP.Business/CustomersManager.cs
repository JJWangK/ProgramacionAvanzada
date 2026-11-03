using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Clientes.
    /// </summary>
    public class CustomersManager
    {
        private readonly CustomersRepository _repository;

        public CustomersManager()
        {
            _repository = new CustomersRepository();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _repository.GetAll();
        }

        public Customer GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Customer entity)
        {
            _repository.Add(entity);
        }

        public void Update(Customer entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
