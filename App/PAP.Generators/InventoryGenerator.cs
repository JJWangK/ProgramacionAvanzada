using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera inventario de prueba para los libros existentes.
    /// </summary>
    public class InventoryGenerator : GeneratorBase
    {
        private readonly InventoryRepository _repository;
        private readonly BooksRepository _booksRepository;

        public InventoryGenerator()
        {
            _repository = new InventoryRepository();
            _booksRepository = new BooksRepository();
        }

        public override string TableName
        {
            get { return "dbo.Inventory"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            var books = RequireRows(_booksRepository.GetAll(), "dbo.Books");
            var target = Math.Min(BatchSize, books.Count);

            for (var i = 0; i < target; i++)
            {
                var inventory = new Inventory
                {
                    BookId = books[i].Id,
                    QuantityInStock = GeneratorData.Next(5, 100),
                    ReorderLevel = GeneratorData.Next(1, 20),
                    LastRestockedAt = GeneratorData.RecentDate(60)
                };

                _repository.Add(inventory);
            }

            return target;
        }
    }
}
