using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share_Models;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderDetailController : Controller
    {
        private readonly dbEcommerceContext _dbContext;

        public OrderDetailController(dbEcommerceContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<OrderDetail>> Get()
        {

            var orderdetails = await _dbContext.OrderDetails.ToListAsync();
            return Ok(orderdetails);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] OrderDetail model)
        {
            _dbContext.OrderDetails.Add(model);
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }


        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(int id, OrderDetail model)
        //{
        //    var orderdetail = await _dbContext.OrderDetails.FirstOrDefaultAsync(x => x.OrderDetailId == id);

        //    if (orderdetail == null)
        //    {
        //        return NotFound();
        //    }
        //    await _dbContext.SaveChangesAsync();
        //    return Accepted();
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete([FromBody] int id)
        //{
        //    var orderdetail = await _dbContext.OrderDetails.FirstOrDefaultAsync(x => x.OrderDetailId == id);

        //    if (orderdetail == null)
        //    {
        //        return NotFound();
        //    }

        //    _dbContext.OrderDetails.Remove(orderdetail);

        //    await _dbContext.SaveChangesAsync();
        //    return Accepted();
        //}
    }
}
