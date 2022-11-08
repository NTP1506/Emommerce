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

        public OrderController(dbEcommerceContext dbContext)
        {
             _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<Order>> Get()
        {

            var orders = await _dbContext.Orders.Include(o => o.TransactStatus).ToListAsync();
            return Ok(orders);
        }
        [HttpGet]
        [Route("OrderlastID")]
        public async Task<ActionResult<Order>> GetLastID()
        {
            var order_ID = await _dbContext.Orders
            .OrderByDescending(o => o.OrderId).FirstOrDefaultAsync();

            if (order_ID == null)
            {
                order_ID.OrderId = 0;
            }
            await _dbContext.SaveChangesAsync();
            return Ok(order_ID);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetID(int id)
        {
            var Order = await _dbContext.Orders
            .FirstOrDefaultAsync(p => p.CustomerId == id);

            if (Order == null)
            {
                return NotFound();
            }
            await _dbContext.SaveChangesAsync();
            return Ok(Order);
        }

        [HttpPost]

        public async Task<ActionResult> Create([FromBody]Order model)
        {
            //var order = new Order
            //{
            //    //Name = model.Name,
            //    //Price = model.Price,
            //    //Description = model.Description,
            //    //BrandId = model.BrandId
            //};

            _dbContext.Orders.Add(model);
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update( int id, Order model)
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
        public async Task<ActionResult> Delete([FromBody] int id)
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
