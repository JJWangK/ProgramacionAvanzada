using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera autores de prueba.
    /// </summary>
    public class AuthorsGenerator : GeneratorBase
    {
        private readonly AuthorsRepository _repository;

        public AuthorsGenerator()
        {
            _repository = new AuthorsRepository();
        }

        public override string TableName
        {
            get { return "dbo.Authors"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            for (var i = 0; i < BatchSize; i++)
            {
                var author = new Author
                {
                    FirstName = GeneratorData.Pick(GeneratorData.FirstNames),
                    LastName = GeneratorData.Pick(GeneratorData.LastNames),
                    Biography = "Autor de prueba generado automáticamente.",
                    BirthDate = GeneratorData.RandomDate(1950, 1995),
                    Country = GeneratorData.Pick(GeneratorData.Countries),
                    CreatedAt = DateTime.Now
                };

                _repository.Add(author);
            }

            return BatchSize;
        }
    }
}
