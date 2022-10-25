using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share_Models
{
    public partial class Rating
    {
        public int Id { get; set; }
        public byte points { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; }
        public byte Status { get; set; }
        public virtual Product? product { get; set; }
        public virtual Customer? customer { get; set; }
    }
}
