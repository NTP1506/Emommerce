using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share_Models;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly dbEcommerceContext _dbContext;
        public CategoryController(dbEcommerceContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<Category>> Get()
        {
            var CategoriesGetAll = await _dbContext.Categories.ToListAsync();
            return Ok(CategoriesGetAll);
        }

        //[HttpGet]
        //public async Task<ActionResult<Category>> GetByAlias(string Alias)
        //{
        //    var Categories = await _dbContext.Categories.Select(p=>p.Alias==Alias).ToListAsync();
        //    return Ok(Categories);
        //}

        [HttpPost]
        public async Task<ActionResult> Create(Category model)
        {
            var category = new Category
            {
                //Name = model.Name,
                //Price = model.Price,
                //Description = model.Description,
                //BrandId = model.BrandId
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Category model)
        {
            var Categories = await _dbContext.Categories.FirstOrDefaultAsync(x => x.CatId == id);

            if (Categories == null)
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
            var Categories = await _dbContext.Categories.FirstOrDefaultAsync(x => x.CatId == id);

            if (Categories == null)
            {
                return NotFound();
            }

            _dbContext.Categories.Remove(Categories);

            await _dbContext.SaveChangesAsync();
            return Accepted();
        }
    }

}
