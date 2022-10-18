using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share_Models;

namespace EcommerceAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly dbEcommerceContext _dbContext;
        public AccountController(dbEcommerceContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<Account>> Get()
        {

            var accounts = await _dbContext.Accounts.ToListAsync();
            return Ok(accounts);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Account model)
        {
            var account = new Account
            {
                //Name = model.Name,
                //Price = model.Price,
                //Description = model.Description,
                //BrandId = model.BrandId
            };

            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Account model)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.AccountId == id);

            if (account == null)
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
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.AccountId == id);

            if (account == null)
            {
                return NotFound();
            }

            _dbContext.Accounts.Remove(account);

            await _dbContext.SaveChangesAsync();
            return Accepted();
        }
    }
}
