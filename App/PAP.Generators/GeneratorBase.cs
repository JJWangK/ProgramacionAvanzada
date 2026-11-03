using System;
using System.Collections.Generic;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Base de los generadores: valida que la tabla esté vacía y delega la inserción.
    /// </summary>
    public abstract class GeneratorBase : IDataGenerator
    {
        /// <summary>
        /// Cantidad de registros que genera cada generador por ejecución.
        /// </summary>
        public const int BatchSize = 10;

        public abstract string TableName { get; }

        public abstract bool HasData();

        public int Generate()
        {
            GeneratorStatusService.EnsureCanGenerate(this);
            return GenerateCore();
        }

        protected abstract int GenerateCore();

        /// <summary>
        /// Devuelve las filas de una tabla de la que depende el generador.
        /// </summary>
        protected static IList<T> RequireRows<T>(IEnumerable<T> rows, string tableName)
        {
            var list = rows == null ? new List<T>() : rows.ToList();
            if (list.Count == 0)
            {
                throw new InvalidOperationException("La tabla " + tableName + " no tiene datos. Genere primero esa tabla.");
            }

            return list;
        }
    }
}
