 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerSite.Models;
using System.Net.Http;
using Share_Models;
using Newtonsoft.Json;
using System.Collections;
using Microsoft.CodeAnalysis;

namespace CustomerSite.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        //private readonly dbMarketContext _context;
        private HttpClient _httpClient;
        List<Product> _products;
        List<Category> _categories;
        public ProductController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7137");
        }
        [Route("shop.html", Name = ("ShopProduct"))]
        public async Task<IActionResult> Index(int? page)
        {
            var response = await _httpClient.GetAsync("Product/Get");
            var content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            _products = JsonConvert.DeserializeObject<List<Product>>(content);
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 10;
                var lstindangs = _products
                    .OrderBy(x => x.DateCreated).ToList();
                PagedList<Product> models = new PagedList<Product>(lstindangs.AsQueryable(), pageNumber, pageSize);

                ViewBag.CurrentPage = pageNumber;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Route("danhmuc/{Alias}", Name = ("ListProduct"))]
        public async Task<IActionResult> List(string Alias, int page = 1)
        {
            var response = await _httpClient.GetAsync("Category/Get");
            var content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            _categories = JsonConvert.DeserializeObject<List<Category>>(content);
            try
            {
                var pageSize = 10;

                var danhmuc = _categories.SingleOrDefault(x => x.Alias == Alias);

                var lsTinDangs = _products
                    .Where(x => x.CatId == danhmuc.CatId)
                    .OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsTinDangs, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = danhmuc;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [Route("/{Alias}-{id}.html", Name = ("ProductDetails"))]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync("Product/Get");
            var content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            _products = JsonConvert.DeserializeObject<List<Product>>(content);

           
            try
            {
                var product = _products.FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                var lsProduct = _products
                    .Where(x => x.CatId == product.CatId && x.ProductId != id && x.Active == true)
                    .Take(4)
                    .OrderByDescending(x => x.DateCreated)
                    .ToList();
                ViewBag.SanPham = lsProduct;
                return View(product);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //[HttpPost]
        [Route("Rate")]
        public async Task<IActionResult> Rate( string _alias, int id, [FromForm] byte rate)
        {
            if (User.Identity.IsAuthenticated)
            {
                var _rating = new Rating1
                {
                    alias = _alias,                        //product = _products.FirstOrDefault(p => p.ProductId == id),
                    productId = id,
                    Name = User.Identity.Name,
                    points = rate,
                    Date = DateTime.Now
                };
                var response = await _httpClient.PostAsJsonAsync("Product/Rate", _rating);
                    

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Details", new { alias = _alias,id = id });
                }
            }

            return new BadRequestResult();
        }
    }
}
