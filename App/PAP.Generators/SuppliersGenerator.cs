using PAP.DataAccess;
using PAP.Repositories;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera proveedores de prueba.
    /// </summary>
    public class SuppliersGenerator : GeneratorBase
    {
        private static readonly string[] Names =
        {
            "Distribuidora Central", "Comercial El Roble", "Suministros del Pacífico", "Grupo Norteño", "Importadora Andina",
            "Proveedora del Valle", "Abastecedora Caribe", "Mayorista La Estrella", "Comercial San Isidro", "Distribuidora Aurora"
        };

        private readonly SuppliersRepository _repository;

        public SuppliersGenerator()
        {
            _repository = new SuppliersRepository();
        }

        public override string TableName
        {
            get { return "dbo.Suppliers"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            for (var i = 0; i < BatchSize; i++)
            {
                var supplier = new Supplier
                {
                    Name = Names[i % Names.Length],
                    ContactName = GeneratorData.Pick(GeneratorData.FirstNames) + " " + GeneratorData.Pick(GeneratorData.LastNames),
                    Email = GeneratorData.Email("proveedor"),
                    Phone = GeneratorData.Phone(),
                    Address = GeneratorData.Pick(GeneratorData.Cities) + ", Costa Rica",
                    IsActive = true
                };

                _repository.Add(supplier);
            }

            return BatchSize;
        }
    }
}
