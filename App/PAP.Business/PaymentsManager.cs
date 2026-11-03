using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Pagos.
    /// </summary>
    public class PaymentsManager
    {
        private readonly PaymentsRepository _repository;

        public PaymentsManager()
        {
            _repository = new PaymentsRepository();
        }

        public IEnumerable<Payment> GetAll()
        {
            return _repository.GetAll();
        }

        public Payment GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Payment entity)
        {
            _repository.Add(entity);
        }

        public void Update(Payment entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
