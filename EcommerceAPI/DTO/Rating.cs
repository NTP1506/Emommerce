using Share_Models;
namespace CustomerSite.ModelViews

{
    public class Rating
    {
        public Product product { get; set; }
        public int productId { get; set; }
        public string? Email { get; set; }
        public Customer customer { get; set; }
        public byte points { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; }
        public byte Status { get; set; }
    }
}
