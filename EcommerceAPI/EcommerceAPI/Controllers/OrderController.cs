using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share_Models;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly dbEcommerceContext _dbContext;
        [HttpGet]
        public async Task<ActionResult<Order>> Get()
        {

            var orders = await _dbContext.Orders.Include(o => o.TransactStatus).ToListAsync();
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Order model)
        {
            var order = new Order
            {
                //Name = model.Name,
                //Price = model.Price,
                //Description = model.Description,
                //BrandId = model.BrandId
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Order model)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);

            if (order == null)
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
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            _dbContext.Orders.Remove(order);

            await _dbContext.SaveChangesAsync();
            return Accepted();
        }
    }
}
