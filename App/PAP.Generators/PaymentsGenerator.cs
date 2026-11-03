using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera pagos de prueba para los pedidos existentes.
    /// </summary>
    public class PaymentsGenerator : GeneratorBase
    {
        private static readonly string[] Methods = { "Tarjeta", "Efectivo", "Transferencia", "SINPE" };

        private readonly PaymentsRepository _repository;
        private readonly OrdersRepository _ordersRepository;

        public PaymentsGenerator()
        {
            _repository = new PaymentsRepository();
            _ordersRepository = new OrdersRepository();
        }

        public override string TableName
        {
            get { return "dbo.Payments"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            var orders = RequireRows(_ordersRepository.GetAll(), "dbo.Orders");

            for (var i = 0; i < BatchSize; i++)
            {
                var order = orders[i % orders.Count];
                var payment = new Payment
                {
                    OrderId = order.Id,
                    PaymentMethod = GeneratorData.Pick(Methods),
                    Amount = order.Total,
                    PaymentDate = GeneratorData.RecentDate(90),
                    Status = "Aprobado",
                    TransactionRef = GeneratorData.Code("TX")
                };

                _repository.Add(payment);
            }

            return BatchSize;
        }
    }
}
