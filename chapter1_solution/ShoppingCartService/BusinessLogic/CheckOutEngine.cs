using System.Linq;
using AutoMapper;
using ShoppingCartService.BusinessLogic.Exceptions;
using ShoppingCartService.Controllers.Models;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Models;

namespace ShoppingCartService.BusinessLogic
{
    public class CheckOutEngine : ICheckOutEngine
    {
        private readonly IMapper _mapper;
        private readonly IShippingCalculator _shippingCalculator;
        private readonly ICouponEngine _couponEngine;

        public CheckOutEngine(IShippingCalculator shippingCalculator, IMapper mapper, ICouponEngine couponEngine)
        {
            _shippingCalculator = shippingCalculator;
            _mapper = mapper;
            _couponEngine = couponEngine;
        }

        public CheckoutDto CalculateTotals(Cart cart, Coupon coupon = null)
        {

            if (cart.ShippingAddress == null)
                throw new MissingDataException("Cannot calculate total cost - missing Shipping method");
            var itemCost = cart.Items.Sum(item => item.Price * item.Quantity);
            var shippingCost = _shippingCalculator.CalculateShippingCost(cart);
            var customerDiscount = 0.0;
            if (cart.CustomerType == CustomerType.Premium) customerDiscount = 10.0;

            var customerDiscountPercent = (100.0 - customerDiscount) / 100.0;
            var total = (itemCost + shippingCost) * customerDiscountPercent;
            

            var shoppingCartDto = _mapper.Map<ShoppingCartDto>(cart);
            var checkoutDto = new CheckoutDto(shoppingCartDto, shippingCost, customerDiscount, total);
            if (coupon != null)
            {
                return checkoutDto with
                {
                    TotalAfterCoupon = _couponEngine.CalculateDiscount(checkoutDto, coupon)
                };
            } else
                return checkoutDto;
        }
    }
}