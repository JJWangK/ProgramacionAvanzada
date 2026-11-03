using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Editoriales.
    /// </summary>
    public class PublishersManager
    {
        private readonly PublishersRepository _repository;

        public PublishersManager()
        {
            _repository = new PublishersRepository();
        }

        public IEnumerable<Publisher> GetAll()
        {
            return _repository.GetAll();
        }

        public Publisher GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Publisher entity)
        {
            _repository.Add(entity);
        }

        public void Update(Publisher entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
