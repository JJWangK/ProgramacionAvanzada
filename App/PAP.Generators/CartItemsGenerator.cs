using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    /// <summary>
    /// Genera ítems de carrito de prueba.
    /// </summary>
    public class CartItemsGenerator : GeneratorBase
    {
        private readonly CartItemsRepository _repository;
        private readonly ShoppingCartsRepository _shoppingCartsRepository;
        private readonly BooksRepository _booksRepository;

        public CartItemsGenerator()
        {
            _repository = new CartItemsRepository();
            _shoppingCartsRepository = new ShoppingCartsRepository();
            _booksRepository = new BooksRepository();
        }

        public override string TableName
        {
            get { return "dbo.CartItems"; }
        }

        public override bool HasData()
        {
            return _repository.GetAll().Any();
        }

        protected override int GenerateCore()
        {
            var carts = RequireRows(_shoppingCartsRepository.GetAll(), "dbo.ShoppingCarts");
            var books = RequireRows(_booksRepository.GetAll(), "dbo.Books");
            var target = Math.Min(BatchSize, Math.Min(carts.Count, books.Count));

            for (var i = 0; i < target; i++)
            {
                var cartItem = new CartItem
                {
                    CartId = carts[i].Id,
                    BookId = books[i].Id,
                    Quantity = GeneratorData.Next(1, 5),
                    AddedAt = GeneratorData.RecentDate(30)
                };

                _repository.Add(cartItem);
            }

            return target;
        }
    }
}
