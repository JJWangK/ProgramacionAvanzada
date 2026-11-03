using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera clientes de prueba.
    /// </summary>
    public class CustomersGenerator : GeneratorBase
    {
        private readonly CustomersRepository _repository;

        public CustomersGenerator()
        {
            _repository = new CustomersRepository();
        }

        public override string TableName
        {
            get { return "dbo.Customers"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            for (var i = 0; i < BatchSize; i++)
            {
                var customer = new Customer
                {
                    FirstName = GeneratorData.Pick(GeneratorData.FirstNames),
                    LastName = GeneratorData.Pick(GeneratorData.LastNames),
                    Email = GeneratorData.Email("cliente"),
                    PasswordHash = "hash-generado-para-pruebas",
                    Phone = GeneratorData.Phone(),
                    BirthDate = GeneratorData.RandomDate(1960, 2004),
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _repository.Add(customer);
            }

            return BatchSize;
        }
    }
}
