using Newtonsoft.Json;
using Share_Models;
using System.Net.Http;

namespace CustomerSite.Clients
{
    public interface IProductClient
    {
    }
    public class ProductClient : BaseClient, IProductClient
    {
        public ProductClient(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor) : base(clientFactory, httpContextAccessor)
        {

        }

        //public ProductClient(IHttpClientFactory clientFactory) : base(clientFactory)
        //{
        //}

        public async Task<List<Product>> GetAllProduct()
        {
            var response = await httpClient.GetAsync("api/product/all");
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<Product>>(contents);

            return products ?? new List<Product>();
        }
    }
}
