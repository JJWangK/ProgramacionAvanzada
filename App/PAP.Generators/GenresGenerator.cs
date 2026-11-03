using PAP.DataAccess;
using PAP.Repositories;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera géneros literarios de prueba.
    /// </summary>
    public class GenresGenerator : GeneratorBase
    {
        private static readonly string[] Names =
        {
            "Novela", "Cuento", "Ensayo", "Biografía", "Ciencia ficción",
            "Misterio", "Romance", "Aventura", "Terror", "Drama"
        };

        private readonly GenresRepository _repository;

        public GenresGenerator()
        {
            _repository = new GenresRepository();
        }

        public override string TableName
        {
            get { return "dbo.Genres"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            for (var i = 0; i < BatchSize; i++)
            {
                var genre = new Genre
                {
                    Name = Names[i % Names.Length],
                    Description = "Género generado automáticamente para pruebas."
                };

                _repository.Add(genre);
            }

            return BatchSize;
        }
    }
}
