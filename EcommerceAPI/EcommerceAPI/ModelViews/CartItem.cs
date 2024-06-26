﻿using System;
using EcommerceAPI.Models;
using Share_Models;

namespace EcommerceAPI.ModelViews
{
    public class CartItem
    {
        public Product product { get; set; }
        public int amount { get; set; }
        public double TotalMoney => amount * product.Price.Value;
    }
}
