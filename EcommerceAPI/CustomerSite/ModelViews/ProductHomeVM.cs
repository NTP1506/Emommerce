using System;
using System.Collections.Generic;
using CustomerSite.Models;
using Share_Models;

namespace CustomerSite.ModelViews
{
    public class ProductHomeVM
    {
        public Category category { get; set; }
        public List<Product> lsProducts { get; set; }
    }
}
