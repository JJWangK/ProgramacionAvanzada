using PAP.Models;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera relaciones libro-autor de prueba.
    /// </summary>
    public class BookAuthorsGenerator : GeneratorBase
    {
        private readonly BookAuthorsRepository _repository;
        private readonly BooksRepository _booksRepository;
        private readonly AuthorsRepository _authorsRepository;

        public BookAuthorsGenerator()
        {
            _repository = new BookAuthorsRepository();
            _booksRepository = new BooksRepository();
            _authorsRepository = new AuthorsRepository();
        }

        public override string TableName
        {
            get { return "dbo.BookAuthors"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            var books = RequireRows(_booksRepository.GetAll(), "dbo.Books");
            var authors = RequireRows(_authorsRepository.GetAll(), "dbo.Authors");
            var target = Math.Min(BatchSize, books.Count);

            for (var i = 0; i < target; i++)
            {
                var relation = new BookAuthor
                {
                    BookId = books[i].Id,
                    AuthorId = authors[i % authors.Count].Id
                };

                _repository.Add(relation);
            }

            return target;
        }
    }
}
