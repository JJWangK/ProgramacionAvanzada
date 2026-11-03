using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    /// <summary>
    /// Lógica de negocio de Reseñas.
    /// </summary>
    public class ReviewsManager
    {
        private readonly ReviewsRepository _repository;

        public ReviewsManager()
        {
            _repository = new ReviewsRepository();
        }

        public IEnumerable<Review> GetAll()
        {
            return _repository.GetAll();
        }

        public Review GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Review entity)
        {
            _repository.Add(entity);
        }

        public void Update(Review entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
