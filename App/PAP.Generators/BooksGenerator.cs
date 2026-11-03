using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera libros de prueba usando categorías, editoriales y géneros existentes.
    /// </summary>
    public class BooksGenerator : GeneratorBase
    {
        private readonly BooksRepository _repository;
        private readonly CategoriesRepository _categoriesRepository;
        private readonly PublishersRepository _publishersRepository;
        private readonly GenresRepository _genresRepository;

        public BooksGenerator()
        {
            _repository = new BooksRepository();
            _categoriesRepository = new CategoriesRepository();
            _publishersRepository = new PublishersRepository();
            _genresRepository = new GenresRepository();
        }

        public override string TableName
        {
            get { return "dbo.Books"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            var categories = RequireRows(_categoriesRepository.GetAll(), "dbo.Categories");
            var publishers = RequireRows(_publishersRepository.GetAll(), "dbo.Publishers");
            var genres = RequireRows(_genresRepository.GetAll(), "dbo.Genres");

            for (var i = 0; i < BatchSize; i++)
            {
                var category = GeneratorData.Pick(categories);
                var publisher = GeneratorData.Pick(publishers);
                var genre = GeneratorData.Pick(genres);
                var code = GeneratorData.Code("LIB");

                var book = new Book
                {
                    Name = "Libro " + code,
                    Title = "Título de prueba " + (i + 1),
                    Category = category.Name,
                    Publisher = publisher.Name,
                    Genre = genre.Name,
                    Description = "Libro generado automáticamente para pruebas.",
                    PublicationDate = GeneratorData.RandomDate(1990, 2024),
                    Height = GeneratorData.Money(15, 30),
                    Width = GeneratorData.Money(10, 25),
                    Price = GeneratorData.Money(8, 120),
                    IsBestSeller = GeneratorData.Next(0, 2) == 1,
                    IsInStock = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    PublisherId = publisher.Id,
                    CategoryId = category.Id,
                    GenreId = genre.Id,
                    ISBN = GeneratorData.Code("978"),
                    Pages = GeneratorData.Next(80, 600),
                    Language = "Español",
                    CoverImageUrl = null
                };

                _repository.Add(book);
            }

            return BatchSize;
        }
    }
}
