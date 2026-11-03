using System;
using System.Collections.Generic;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Decide qué generadores pueden ejecutarse según los datos y las dependencias.
    /// La vista y el controller solo muestran el resultado de esta clase.
    /// </summary>
    public static class GeneratorStatusService
    {
        /// <summary>
        /// Estado de los 20 generadores para la pantalla Admin.
        /// </summary>
        public static IList<GeneratorStatus> GetStatuses()
        {
            var generators = GeneratorCatalog.CreateAll();
            var tablesWithData = GetTablesWithData(generators);
            var statuses = new List<GeneratorStatus>();

            foreach (var generator in generators)
            {
                var hasData = tablesWithData.Contains(generator.TableName);
                var missing = GetMissingDependencies(generator.TableName, tablesWithData);
                var isEnabled = !hasData && missing.Count == 0;

                statuses.Add(new GeneratorStatus(generator.TableName, isEnabled, GetMessage(hasData, missing)));
            }

            return statuses;
        }

        /// <summary>
        /// Valida las reglas antes de generar. También lo usa GeneratorBase.
        /// </summary>
        public static void EnsureCanGenerate(IDataGenerator generator)
        {
            var tablesWithData = GetTablesWithData(GeneratorCatalog.CreateAll());

            if (tablesWithData.Contains(generator.TableName))
            {
                throw new InvalidOperationException(
                    "La tabla " + generator.TableName + " ya tiene datos. No se puede generar de nuevo.");
            }

            var missing = GetMissingDependencies(generator.TableName, tablesWithData);
            if (missing.Count > 0)
            {
                throw new InvalidOperationException(
                    "La tabla " + generator.TableName + " requiere datos en " + string.Join(", ", missing) +
                    ". Genere primero esas tablas.");
            }
        }

        private static HashSet<string> GetTablesWithData(IEnumerable<IDataGenerator> generators)
        {
            var tables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var generator in generators)
            {
                if (generator.HasData())
                {
                    tables.Add(generator.TableName);
                }
            }

            return tables;
        }

        private static IList<string> GetMissingDependencies(string tableName, HashSet<string> tablesWithData)
        {
            return GeneratorDependencies.GetDependencies(tableName)
                .Where(dependency => !tablesWithData.Contains(dependency))
                .ToList();
        }

        private static string GetMessage(bool hasData, IList<string> missing)
        {
            if (hasData)
            {
                return "La tabla ya tiene datos.";
            }

            if (missing.Count > 0)
            {
                return "Requiere datos en " + string.Join(", ", missing) + ".";
            }

            return "Listo para generar.";
        }
    }
}
