//using Share_Models;
namespace Share_Models

{
    public class Rating1
    {
        //public Product? product { get; set; }
        public int productId { get; set; }
        public string? Name { get; set; }
        //public Customer? customer { get; set; }
        public byte points { get; set; }
        public string? Comment { get; set; }

        public string? alias { get; set; }

        public DateTime Date { get; set; }
        public byte Status { get; set; }
    }
}
