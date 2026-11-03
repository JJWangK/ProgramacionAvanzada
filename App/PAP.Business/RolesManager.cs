using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Roles.
    /// </summary>
    public class RolesManager
    {
        private readonly RolesRepository _repository;

        public RolesManager()
        {
            _repository = new RolesRepository();
        }

        public IEnumerable<Role> GetAll()
        {
            return _repository.GetAll();
        }

        public Role GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Role entity)
        {
            _repository.Add(entity);
        }

        public void Update(Role entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
