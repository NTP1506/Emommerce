using System;
using System.Collections.Generic;
using EcommerceAPI.Models;
using Share_Models;

namespace EcommerceAPI.ModelViews
{
    public class ProductHomeVM
    {
        public Category category { get; set; }
        public List<Product> lsProducts { get; set; }
    }
}
