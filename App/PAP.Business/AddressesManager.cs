using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Direcciones.
    /// </summary>
    public class AddressesManager
    {
        private readonly AddressesRepository _repository;

        public AddressesManager()
        {
            _repository = new AddressesRepository();
        }

        public IEnumerable<Address> GetAll()
        {
            return _repository.GetAll();
        }

        public Address GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Address entity)
        {
            _repository.Add(entity);
        }

        public void Update(Address entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
