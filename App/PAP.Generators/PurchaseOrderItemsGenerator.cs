using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera ítems de orden de compra de prueba.
    /// </summary>
    public class PurchaseOrderItemsGenerator : GeneratorBase
    {
        private readonly PurchaseOrderItemsRepository _repository;
        private readonly PurchaseOrdersRepository _purchaseOrdersRepository;
        private readonly BooksRepository _booksRepository;

        public PurchaseOrderItemsGenerator()
        {
            _repository = new PurchaseOrderItemsRepository();
            _purchaseOrdersRepository = new PurchaseOrdersRepository();
            _booksRepository = new BooksRepository();
        }

        public override string TableName
        {
            get { return "dbo.PurchaseOrderItems"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            var purchaseOrders = RequireRows(_purchaseOrdersRepository.GetAll(), "dbo.PurchaseOrders");
            var books = RequireRows(_booksRepository.GetAll(), "dbo.Books");

            for (var i = 0; i < BatchSize; i++)
            {
                var purchaseOrderItem = new PurchaseOrderItem
                {
                    PurchaseOrderId = purchaseOrders[i % purchaseOrders.Count].Id,
                    BookId = books[i % books.Count].Id,
                    Quantity = GeneratorData.Next(5, 50),
                    UnitCost = GeneratorData.Money(5, 80)
                };

                _repository.Add(purchaseOrderItem);
            }

            return BatchSize;
        }
    }
}
