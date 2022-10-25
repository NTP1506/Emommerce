
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
        [Route("customer-get")]
        public async Task<ActionResult<Share_Models.Customer>> Get()
        {

            var customers = await _dbContext.Customers.ToListAsync();
            return Ok(customers);
        }


        [HttpGet]
        [Route("CustomerlastID")]
        public async Task<ActionResult<Customer>> GetLastID()
        {
            var KH =await _dbContext.Customers
            .OrderByDescending(blog => blog.CustomerId).FirstOrDefaultAsync();

            if (KH == null)
            {
                return NotFound();
            }
            await _dbContext.SaveChangesAsync();
            return Ok(KH);
        }

        [HttpPost]
        [Route("customerpost")]
        public async Task<ActionResult> Create([FromBody] Customer model)
        {
           
            _dbContext.Customers.Add(model);
            await _dbContext.SaveChangesAsync();

            return Accepted();

        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update( int id, [FromBody] Share_Models.Customer model)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
            
            //_dbContext.Customers.Add(model);
            if (customer == null)
            {
                return NotFound();
            }
            customer.CustomerId = model.CustomerId;
            customer.Password = model.Password;
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
