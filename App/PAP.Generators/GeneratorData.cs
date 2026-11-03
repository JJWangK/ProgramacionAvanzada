using System;
using System.Collections.Generic;

namespace PAP.Generators
{
    /// <summary>
    /// Valores de apoyo para los generadores: nombres, ciudades y números aleatorios.
    /// </summary>
    internal static class GeneratorData
    {
        private static readonly Random Random = new Random();
        private static readonly object Sync = new object();

        public static readonly string[] FirstNames =
        {
            "Ana", "Luis", "Carlos", "María", "José", "Laura", "Andrés", "Sofía", "Diego", "Elena",
            "Pablo", "Marta", "Jorge", "Lucía", "Raúl", "Carmen", "Miguel", "Paula", "Sergio", "Isabel"
        };

        public static readonly string[] LastNames =
        {
            "Rodríguez", "Jiménez", "Mora", "Vargas", "Solís", "Castro", "Rojas", "Herrera", "Ramírez", "Quesada",
            "Chaves", "Araya", "Segura", "Blanco", "Aguilar", "Salazar", "Méndez", "Cordero", "Villalobos", "Fallas"
        };

        public static readonly string[] Countries =
        {
            "Costa Rica", "México", "Colombia", "España", "Argentina",
            "Chile", "Perú", "Panamá", "Guatemala", "Ecuador"
        };

        public static readonly string[] Cities =
        {
            "San José", "Alajuela", "Cartago", "Heredia", "Liberia",
            "Puntarenas", "Limón", "Ciudad Quesada", "Grecia", "San Isidro"
        };

        public static readonly string[] States =
        {
            "San José", "Alajuela", "Cartago", "Heredia", "Guanacaste", "Puntarenas", "Limón"
        };

        public static readonly string[] AddressTypes = { "Principal", "Envío", "Facturación" };

        public static T Pick<T>(IList<T> items)
        {
            lock (Sync)
            {
                return items[Random.Next(items.Count)];
            }
        }

        public static int Next(int min, int maxExclusive)
        {
            lock (Sync)
            {
                return Random.Next(min, maxExclusive);
            }
        }

        public static decimal Money(decimal min, decimal max)
        {
            lock (Sync)
            {
                var value = min + (decimal)Random.NextDouble() * (max - min);
                return Math.Round(value, 2);
            }
        }

        public static DateTime RandomDate(int startYear, int endYear)
        {
            var start = new DateTime(startYear, 1, 1);
            var range = (new DateTime(endYear, 12, 31) - start).Days;
            return start.AddDays(Next(0, range));
        }

        public static DateTime RecentDate(int maxDaysAgo)
        {
            return DateTime.Now.AddDays(-Next(1, maxDaysAgo));
        }

        /// <summary>
        /// Código corto y único para campos como ISBN o referencias.
        /// </summary>
        public static string Code(string prefix)
        {
            return prefix + Guid.NewGuid().ToString("N").Substring(0, 10).ToUpperInvariant();
        }

        public static string Email(string prefix)
        {
            return prefix + "." + Guid.NewGuid().ToString("N").Substring(0, 8) + "@bookstore.test";
        }

        public static string Phone()
        {
            return "8" + Next(1000000, 9999999);
        }
    }
}
