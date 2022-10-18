using System;
using System.Collections.Generic;
using CustomerSite.Models;
using Share_Models;

namespace WebShop.ModelViews
{
    public class XemDonHang
    {
        public Order DonHang { get; set; }
        public List<OrderDetail> ChiTietDonHang { get; set; }
    }
}
