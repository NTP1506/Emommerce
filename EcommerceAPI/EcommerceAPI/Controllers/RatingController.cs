using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share_Models;

namespace EcommerceAPI.Controllers
{

    //[ApiController]
    //[Route("[controller]")]
    public class RatingController : Controller
    {
        private readonly dbEcommerceContext _dbContext;


        public RatingController(dbEcommerceContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> CreateAsync(Rating entry)
        {
            Rating dup = await _dbContext.Ratings.Where(r => r.product.ProductId == entry.product.ProductId
                                                           && r.customer.CustomerId == entry.customer.CustomerId)
                                               .FirstOrDefaultAsync();

            if (dup != null) return await UpdateAsync(dup.Id, entry);
            try
            {
                await _dbContext.Ratings.AddAsync(entry);
                return new OkResult();
            }
            catch { return new BadRequestResult(); }
        }
        public async Task<ActionResult> UpdateAsync(int id, Rating entry)
        {
            if (id != entry.Id) return new BadRequestResult();
            if (await _dbContext.Ratings.AnyAsync(r => r.Id == id))
            {
                _dbContext.Entry(entry).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return new OkResult();
            }
            return new NotFoundResult();
        }
    }
}
