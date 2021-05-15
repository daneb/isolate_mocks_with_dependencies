using System;
using System.Collections.Generic;
using ShoppingCartService.BusinessLogic.Coupons;
using ShoppingCartService.BusinessLogic.Exceptions;
using ShoppingCartService.Controllers.Models;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Models;

namespace ShoppingCartService.BusinessLogic
{
    public interface ICouponEngine
    {
        double CalculateDiscount(CheckoutDto checkoutDto, Coupon coupon);
    }
}