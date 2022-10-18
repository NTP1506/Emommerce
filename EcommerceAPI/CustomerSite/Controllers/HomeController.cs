using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CustomerSite.Models;
using CustomerSite.ModelViews;
using Share_Models;
using Newtonsoft.Json;

namespace CustomerSite.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly dbMarketContext _context;
        private HttpClient _httpClient;
        List<Product> _products;
        List<Category> _categories;
        public HomeController(ILogger<HomeController> logger)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7137/");
            _logger = logger;
            //_context = context;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Product");
            var content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            _products = JsonConvert.DeserializeObject<List<Product>>(content);

            response = await _httpClient.GetAsync("Category");
            content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            _categories = JsonConvert.DeserializeObject<List<Category>>(content);
            HomeViewVM model = new HomeViewVM();

            var lsProducts = _products
                .Where(x => x.Active == true && x.HomeFlag == true)
                .OrderByDescending(x => x.DateCreated)
                .ToList();

            List<ProductHomeVM> lsProductViews = new List<ProductHomeVM>();
            var lsCats = _categories
                .Where(x => x.Published == true)
                .OrderByDescending(x => x.Ordering)
                .ToList();

            foreach (var item in lsCats)
            {
                ProductHomeVM productHome = new ProductHomeVM();
                productHome.category = item;
                productHome.lsProducts = lsProducts.Where(x => x.CatId == item.CatId).ToList();
                lsProductViews.Add(productHome);

                model.Products = lsProductViews;

                ViewBag.AllProducts = lsProducts;
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("lien-he.html", Name = "Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [Route("gioi-thieu.html", Name = "About")]
        public IActionResult About()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
