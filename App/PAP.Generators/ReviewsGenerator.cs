using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera reseñas de prueba para libros y clientes existentes.
    /// </summary>
    public class ReviewsGenerator : GeneratorBase
    {
        private readonly ReviewsRepository _repository;
        private readonly BooksRepository _booksRepository;
        private readonly CustomersRepository _customersRepository;

        public ReviewsGenerator()
        {
            _repository = new ReviewsRepository();
            _booksRepository = new BooksRepository();
            _customersRepository = new CustomersRepository();
        }

        public override string TableName
        {
            get { return "dbo.Reviews"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            var books = RequireRows(_booksRepository.GetAll(), "dbo.Books");
            var customers = RequireRows(_customersRepository.GetAll(), "dbo.Customers");
            var target = Math.Min(BatchSize, Math.Min(books.Count, customers.Count));

            for (var i = 0; i < target; i++)
            {
                var review = new Review
                {
                    BookId = books[i].Id,
                    CustomerId = customers[i].Id,
                    Rating = (byte)GeneratorData.Next(1, 6),
                    Comment = "Reseña generada automáticamente para pruebas.",
                    CreatedAt = GeneratorData.RecentDate(120)
                };

                _repository.Add(review);
            }

            return target;
        }
    }
}
