using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera direcciones de prueba para los clientes existentes.
    /// </summary>
    public class AddressesGenerator : GeneratorBase
    {
        private readonly AddressesRepository _repository;
        private readonly CustomersRepository _customersRepository;

        public AddressesGenerator()
        {
            _repository = new AddressesRepository();
            _customersRepository = new CustomersRepository();
        }

        public override string TableName
        {
            get { return "dbo.Addresses"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            var customers = RequireRows(_customersRepository.GetAll(), "dbo.Customers");
            var target = Math.Min(BatchSize, customers.Count);

            for (var i = 0; i < target; i++)
            {
                var address = new Address
                {
                    CustomerId = customers[i].Id,
                    AddressType = i == 0 ? "Principal" : GeneratorData.Pick(GeneratorData.AddressTypes),
                    Street = "Calle " + GeneratorData.Next(1, 120) + ", casa " + GeneratorData.Next(1, 60),
                    City = GeneratorData.Pick(GeneratorData.Cities),
                    State = GeneratorData.Pick(GeneratorData.States),
                    PostalCode = GeneratorData.Next(10000, 99999).ToString(),
                    Country = "Costa Rica",
                    IsDefault = i == 0
                };

                _repository.Add(address);
            }

            return target;
        }
    }
}
