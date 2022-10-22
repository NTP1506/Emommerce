using System;
using System.Collections.Generic;
using CustomerSite.Models;
using Share_Models;

namespace CustomerSite.ModelViews
{
    public class XemDonHang
    {
        public Order DonHang { get; set; }
        public List<OrderDetail> ChiTietDonHang { get; set; }
    }
}
