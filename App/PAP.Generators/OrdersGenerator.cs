using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera pedidos de prueba usando clientes y direcciones existentes.
    /// </summary>
    public class OrdersGenerator : GeneratorBase
    {
        private readonly OrdersRepository _repository;
        private readonly CustomersRepository _customersRepository;
        private readonly AddressesRepository _addressesRepository;

        public OrdersGenerator()
        {
            _repository = new OrdersRepository();
            _customersRepository = new CustomersRepository();
            _addressesRepository = new AddressesRepository();
        }

        public override string TableName
        {
            get { return "dbo.Orders"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            var customers = RequireRows(_customersRepository.GetAll(), "dbo.Customers");
            var addresses = _addressesRepository.GetAll().ToList();

            for (var i = 0; i < BatchSize; i++)
            {
                var customer = customers[i % customers.Count];
                var address = addresses.FirstOrDefault(a => a.CustomerId == customer.Id);
                var subtotal = GeneratorData.Money(20, 300);
                var tax = Math.Round(subtotal * 0.13m, 2);
                var shipping = GeneratorData.Money(0, 10);

                var order = new Order
                {
                    CustomerId = customer.Id,
                    ShippingAddressId = address == null ? (int?)null : address.Id,
                    OrderDate = GeneratorData.RecentDate(90),
                    Status = "Pendiente",
                    Subtotal = subtotal,
                    TaxAmount = tax,
                    ShippingCost = shipping,
                    Total = subtotal + tax + shipping,
                    Notes = null,
                    UpdatedAt = DateTime.Now
                };

                _repository.Add(order);
            }

            return BatchSize;
        }
    }
}
