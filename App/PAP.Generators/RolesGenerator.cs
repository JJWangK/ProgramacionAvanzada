using PAP.DataAccess;
using PAP.Repositories;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera roles de prueba.
    /// </summary>
    public class RolesGenerator : GeneratorBase
    {
        private static readonly string[] Names =
        {
            "Administrador", "Ventas", "Inventario", "Soporte", "Contabilidad",
            "Bodega", "Compras", "Marketing", "Atención al cliente", "Gerencia"
        };

        private readonly RolesRepository _repository;

        public RolesGenerator()
        {
            _repository = new RolesRepository();
        }

        public override string TableName
        {
            get { return "dbo.Roles"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            for (var i = 0; i < BatchSize; i++)
            {
                var role = new Role
                {
                    Name = Names[i % Names.Length]
                };

                _repository.Add(role);
            }

            return BatchSize;
        }
    }
}
