using System;
using System.Collections.Generic;

#nullable disable

namespace Share_Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? OrderNuber { get; set; }
        public int? Quantity { get; set; }
        public int? Discount { get; set; }
        public string ProductName { get; set; }
        public int? Amount { get; set; }
        public int? Total { get; set; }
        public DateTime? ShipDate { get; set; }
        
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
