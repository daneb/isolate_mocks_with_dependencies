using System.Collections.Generic;
using ShoppingCartService.Config;
using ShoppingCartService.DataAccess;
using ShoppingCartService.DataAccess.Entities;

namespace ShoppingCartServiceTests.Mocks
{
    public class FakeShoppingCartRepository : IShoppingCartRepository
    {
        private List<Cart> _carts = null;

        public FakeShoppingCartRepository(IShoppingCartDatabaseSettings settings)
        {
            _carts = new List<Cart>();
        }
        
        public IEnumerable<Cart> FindAll()
        {
            return _carts;
        }

        public Cart FindById(string id)
        {
            return _carts.Find(c => c.Id == id);
        }

        public Cart Create(Cart cart)
        {
            cart.Id = System.Guid.NewGuid().ToString();
            _carts.Add(cart);

            return cart;
        }

        public void Update(string id, Cart cart)
        {
            Remove(id);
            _carts.Add(cart);
        }

        public void Remove(Cart cart)
        {
            _carts.Remove(cart);
        }

        public void Remove(string id)
        {
            var old = FindById(id);
            _carts.Remove(old);

        }
    }
}