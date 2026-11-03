using PAP.DataAccess;
using PAP.Repositories;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera categorías de prueba.
    /// </summary>
    public class CategoriesGenerator : GeneratorBase
    {
        private static readonly string[] Names =
        {
            "Ficción", "No ficción", "Ciencia", "Historia", "Tecnología",
            "Arte", "Filosofía", "Infantil", "Poesía", "Negocios"
        };

        private readonly CategoriesRepository _repository;

        public CategoriesGenerator()
        {
            _repository = new CategoriesRepository();
        }

        public override string TableName
        {
            get { return "dbo.Categories"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            for (var i = 0; i < BatchSize; i++)
            {
                var category = new Category
                {
                    Name = Names[i % Names.Length],
                    Description = "Categoría generada automáticamente para pruebas."
                };

                _repository.Add(category);
            }

            return BatchSize;
        }
    }
}
