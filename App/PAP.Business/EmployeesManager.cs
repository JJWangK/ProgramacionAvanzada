using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Empleados.
    /// </summary>
    public class EmployeesManager
    {
        private readonly EmployeesRepository _repository;

        public EmployeesManager()
        {
            _repository = new EmployeesRepository();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _repository.GetAll();
        }

        public Employee GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Employee entity)
        {
            _repository.Add(entity);
        }

        public void Update(Employee entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
