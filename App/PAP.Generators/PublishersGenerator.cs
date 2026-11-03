using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera editoriales de prueba.
    /// </summary>
    public class PublishersGenerator : GeneratorBase
    {
        private static readonly string[] Names =
        {
            "Editorial Sol", "Ediciones del Valle", "Grupo Horizonte", "Editorial Mar Abierto", "Letras y Papel",
            "Editorial Cumbre", "Nueva Página", "Ediciones Central", "Editorial Aurora", "Libros del Sur"
        };

        private readonly PublishersRepository _repository;

        public PublishersGenerator()
        {
            _repository = new PublishersRepository();
        }

        public override string TableName
        {
            get { return "dbo.Publishers"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            for (var i = 0; i < BatchSize; i++)
            {
                var publisher = new Publisher
                {
                    Name = Names[i % Names.Length],
                    Country = GeneratorData.Pick(GeneratorData.Countries),
                    Website = "https://" + GeneratorData.Code("editorial").ToLowerInvariant() + ".test",
                    Email = GeneratorData.Email("editorial"),
                    CreatedAt = DateTime.Now
                };

                _repository.Add(publisher);
            }

            return BatchSize;
        }
    }
}
