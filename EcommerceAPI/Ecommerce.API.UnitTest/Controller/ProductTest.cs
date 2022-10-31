using EcommerceAPI.Controllers;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.API.UnitTest.Controller
{
    public class ProductTest : IDisposable
    {
        private readonly DbContextOptions<dbEcommerceContext> _options;
        private readonly dbEcommerceContext _context;
        private readonly List<Product> _products;
        private readonly List<Category> _catagories;



        public ProductTest()
        {
            _options = new DbContextOptionsBuilder<dbEcommerceContext>().UseInMemoryDatabase("ProductTestDB").Options;
            _context = new dbEcommerceContext(_options);

            // Create Fake Data
            _catagories = new()
            {
                new Category(){CatId = 1, CartName = "ao", Published = true, Alias = "ao" },
                new Category(){CatId = 2, CartName = "quan", Published = true, Alias = "quan" },
            };
            _products = new()
            {
                new Product(){ProductId = 1, ProductName = "Product_1", Active = true, Cat = _catagories[0] },
                new Product(){ProductId = 2, ProductName = "Product_2", Active = true, Cat = _catagories[0] },
                new Product(){ProductId = 3, ProductName = "Product_3", Active = true, Cat = _catagories[0] },
                new Product(){ProductId = 4, ProductName = "Product_4", Active = true, Cat = _catagories[1] },
                new Product(){ProductId = 5, ProductName = "Product_5", Active = true, Cat = _catagories[1] },
            };
            _context.Database.EnsureDeleted();
            _context.Categories.AddRange(_catagories);
            _context.Products.AddRange(_products);
            _context.SaveChanges();
        }

        [Fact]
        public void GetAll_Success()
        {
            //ARRANGE
            ProductController productController = new ProductController(_context);

            //ACT
            var result = productController.Get().Result.Result as ObjectResult;
            List<Product>? data = (List<Product>?)result.Value;
            Assert.NotNull(data);
            Assert.NotEmpty(data);
            Assert.Equivalent(_products, data);
        }
        [Theory]
        [InlineData(6, "product 6", 1)]
        [InlineData(7, "product 7", 1)]
        [InlineData(8, "product 8", 2)]
        public void Create_Success(int id, string name, int catId)
        {
            //ARRANGE
            ProductController productController = new ProductController(_context);
            Product p = new Product() { ProductId = id, ProductName = name, Cat = _catagories.FirstOrDefault(c => c.CatId == catId) };
            //ACT
            var result = productController.Create(p).Result;
            Assert.NotNull(result);
            Assert.Equal(_context.Products.LastOrDefault().ProductId, id);
            Assert.Equal(_context.Products.LastOrDefault().ProductName, name);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
   
}
