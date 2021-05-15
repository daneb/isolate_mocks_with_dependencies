using System;
using MongoDB.Driver;
using ShoppingCartService.Config;
using ShoppingCartService.DataAccess;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Models;
using ShoppingCartServiceTests.Fixtures;
using ShoppingCartServiceTests.Mocks;
using Xunit;

namespace ShoppingCartServiceTests.DataAccess
{
    public class CouponsRepositoryIntegrationTests 
    {
        private readonly ShoppingCartDatabaseSettings _databaseSettings = null;
        private const string Invalid_ID = "507f191e810c19729de860ea";
        
        
        [Fact]
        public void FindById_hasThreeCouponsInDB_returnReturnOnlyCouponWithCorrectId()
        {
            var target = new FakeCouponRepository(_databaseSettings);

            var coupon1 = new Coupon {Value = 10, CouponType = CouponType.Amount, Expiration = DateTime.Now.ToUniversalTime().Date};
            var coupon2 = new Coupon {Value = 20, CouponType = CouponType.Percentage, Expiration = DateTime.Now.ToUniversalTime().Date};
            var coupon3 = new Coupon {Value = 40, CouponType = CouponType.FreeShipping, Expiration = DateTime.Now.ToUniversalTime().Date };
            target.Create(coupon1);
            target.Create(coupon2);
            target.Create(coupon3);

            var actual = target.FindById(coupon2.Id);

            var expected = new Coupon
                {Id = coupon2.Id, Value = 20, CouponType = CouponType.Percentage, Expiration = coupon2.Expiration};

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetById_CouponNotFound_ReturnNull()
        {
            var target = new FakeCouponRepository(_databaseSettings);

            var coupon1 = new Coupon();
            var coupon2 = new Coupon();
            var coupon3 = new Coupon();
            target.Create(coupon1);
            target.Create(coupon2);
            target.Create(coupon3);

            var actual = target.FindById(Invalid_ID);

            Assert.Null(actual);
        }

        [Fact]
        public void DeleteById_CouponFound_RemoveFromDb()
        {
            var target = new FakeCouponRepository(_databaseSettings);
            var coupon = new Coupon();
            target.Create(coupon);

            target.DeleteById(coupon.Id);

            var result = target.FindById(coupon.Id);

            Assert.Null(result);
        }
    }
}