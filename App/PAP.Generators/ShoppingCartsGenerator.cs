using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera carritos de compra de prueba para los clientes existentes.
    /// </summary>
    public class ShoppingCartsGenerator : GeneratorBase
    {
        private readonly ShoppingCartsRepository _repository;
        private readonly CustomersRepository _customersRepository;

        public ShoppingCartsGenerator()
        {
            _repository = new ShoppingCartsRepository();
            _customersRepository = new CustomersRepository();
        }

        public override string TableName
        {
            get { return "dbo.ShoppingCarts"; }
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
                var createdAt = GeneratorData.RecentDate(30);
                var shoppingCart = new ShoppingCart
                {
                    CustomerId = customers[i].Id,
                    CreatedAt = createdAt,
                    UpdatedAt = DateTime.Now
                };

                _repository.Add(shoppingCart);
            }

            return target;
        }
    }
}
