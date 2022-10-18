
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share_Models;

namespace EcommerceAPI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly dbEcommerceContext _dbContext;


        public CustomerController(dbEcommerceContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task<ActionResult<Share_Models.Customer>> Get()
        {

            var customers = await _dbContext.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Share_Models.Customer model)
        {
            var customer = new Share_Models.Customer
            {
                //Name = model.Name,
                //Price = model.Price,
                //Description = model.Description,
                //BrandId = model.BrandId
            };

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Share_Models.Customer model)
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
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            _dbContext.Customers.Remove(customer);

            await _dbContext.SaveChangesAsync();
            return Accepted();
        }
    }
}
