using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera órdenes de compra de prueba usando los proveedores existentes.
    /// </summary>
    public class PurchaseOrdersGenerator : GeneratorBase
    {
        private readonly PurchaseOrdersRepository _repository;
        private readonly SuppliersRepository _suppliersRepository;

        public PurchaseOrdersGenerator()
        {
            _repository = new PurchaseOrdersRepository();
            _suppliersRepository = new SuppliersRepository();
        }

        public override string TableName
        {
            get { return "dbo.PurchaseOrders"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            var suppliers = RequireRows(_suppliersRepository.GetAll(), "dbo.Suppliers");

            for (var i = 0; i < BatchSize; i++)
            {
                var orderDate = GeneratorData.RecentDate(60);
                var purchaseOrder = new PurchaseOrder
                {
                    SupplierId = suppliers[i % suppliers.Count].Id,
                    OrderDate = orderDate,
                    ExpectedDate = orderDate.AddDays(GeneratorData.Next(3, 20)),
                    Status = "Pendiente",
                    TotalCost = GeneratorData.Money(100, 2000)
                };

                _repository.Add(purchaseOrder);
            }

            return BatchSize;
        }
    }
}
