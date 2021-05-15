using System.Collections.Generic;
using ShoppingCartService.Config;
using ShoppingCartService.DataAccess;
using ShoppingCartService.DataAccess.Entities;

namespace ShoppingCartServiceTests.Mocks
{
    public class FakeCouponRepository : ICouponRepository
    {
        private readonly List<Coupon> _coupons = null;
        
        public FakeCouponRepository(IShoppingCartDatabaseSettings settings)
        {
            _coupons = new List<Coupon>();
        }
        
        public Coupon Create(Coupon coupon)
        {
            coupon.Id = System.Guid.NewGuid().ToString();
            _coupons.Add(coupon);

            return coupon;
        }

        public Coupon FindById(string id)
        {
            return _coupons.Find(coupon => coupon.Id == id);
        }

        public void DeleteById(string id)
        {
            var coupon = FindById(id);
            _coupons.Remove(coupon);
        }
    }
}