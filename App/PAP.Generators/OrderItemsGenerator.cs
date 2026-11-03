using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera ítems de pedido de prueba.
    /// </summary>
    public class OrderItemsGenerator : GeneratorBase
    {
        private readonly OrderItemsRepository _repository;
        private readonly OrdersRepository _ordersRepository;
        private readonly BooksRepository _booksRepository;

        public OrderItemsGenerator()
        {
            _repository = new OrderItemsRepository();
            _ordersRepository = new OrdersRepository();
            _booksRepository = new BooksRepository();
        }

        public override string TableName
        {
            get { return "dbo.OrderItems"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            var orders = RequireRows(_ordersRepository.GetAll(), "dbo.Orders");
            var books = RequireRows(_booksRepository.GetAll(), "dbo.Books");

            for (var i = 0; i < BatchSize; i++)
            {
                var book = books[i % books.Count];
                var quantity = GeneratorData.Next(1, 4);
                var orderItem = new OrderItem
                {
                    OrderId = orders[i % orders.Count].Id,
                    BookId = book.Id,
                    Quantity = quantity,
                    UnitPrice = book.Price,
                    Discount = GeneratorData.Next(0, 3) == 0 ? GeneratorData.Money(1, 5) : 0
                };

                _repository.Add(orderItem);
            }

            return BatchSize;
        }
    }
}
