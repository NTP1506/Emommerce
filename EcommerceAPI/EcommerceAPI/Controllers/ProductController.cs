using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share_Models;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly dbEcommerceContext _dbContext;


        public ProductController(dbEcommerceContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task<ActionResult<Product>> Get()
        {
           
            var products = await _dbContext.Products.Include(c =>c.Cat).ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product model)
        {
            var product = new Product
            {
                //Name = model.Name,
                //Price = model.Price,
                //Description = model.Description,
                //BrandId = model.BrandId
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Product model)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            //product.Name = model.Name;
            //product.Price = model.Price;
            //product.Description = model.Description;

            await _dbContext.SaveChangesAsync();
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(product);

            await _dbContext.SaveChangesAsync();
            return Accepted();
        }
    }
}
