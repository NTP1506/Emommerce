﻿using EcommerceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share_Models;


namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly dbEcommerceContext _dbContext;
        private readonly RatingController _rating;

        public ProductController(dbEcommerceContext dbContext)
        {
            _dbContext = dbContext;
            _rating = new RatingController(_dbContext);
        }


        [HttpGet]
        
        public async Task<ActionResult<Product>> Get()
        {

            var products = await _dbContext.Products.Include(c => c.Cat).ToListAsync();
          
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetID(int id)
        {
            var SP = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.ProductId == id);

            if (SP == null)
            {
                return NotFound();
            }
            await _dbContext.SaveChangesAsync();
            return Ok(SP);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product model)
        {
            //var product = new Product
            //{
            //    //ProductId = model.ProductId,
            //    ProductName = model.ProductName,
            //    Price = model.Price,
            //    Discount = model.Discount,
            //    OrderDetails = model.OrderDetails,
            //    Descriptions = model.Descriptions,
            //    ShortDesc = model.ShortDesc,
            //    BestSellers = model.BestSellers,
            //    UnitslnStock = model.UnitslnStock,
            //    Cat = model.Cat,
            //};

            _dbContext.Products.Add(model);
            await _dbContext.SaveChangesAsync();

            return Accepted();
        }

        [HttpPost]

        public async Task<ActionResult> Rate(Rating1  productRate)
        {
            try
            {
                Product? product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productRate.productId);
                Customer? user   = await _dbContext.Customers.FirstOrDefaultAsync(u => u.FullName == productRate.Name);
                Rating? rate     = await _dbContext.Ratings.FirstOrDefaultAsync(r => r.product.ProductId == product.ProductId && r.customer.CustomerId == user.CustomerId);

                if (product != null && user != null)
                {
                    if (rate != null)
                    {
                        //calculate average rating for existing user rating
                        product.Rating = (float)(product.Rating * product.RatingCount - rate.points + productRate.points) / product.RatingCount;
                        rate.points = productRate.points;
                        rate.Comment = productRate.Comment;
                        rate.Date = productRate.Date;
                    }
                    else
                    {
                        if (product.RatingCount == null) product.RatingCount = 1;
                        if (product.Rating == null || product.Rating <= 0) product.Rating = productRate.points;
                        else
                        {
                            //calculate average rating for new user rating
                            product.Rating = (float)(product.Rating * product.RatingCount + productRate.points) / (product.RatingCount + 1);
                            product.RatingCount++;
                        }
                        _dbContext.Entry(product).State = EntityState.Modified;
                        rate = new Rating
                        {
                            customer = user,
                            points = productRate.points,
                            product = product,
                            Comment = productRate.Comment,
                            Date = productRate.Date
                        };
                        //await _dbContext.AddAsync(rate);
                    }
                    try
                    {
                        
                            
                        await _rating.CreateAsync(rate);
                        await _dbContext.SaveChangesAsync();
                        return new OkResult();
                    }
                    catch { return new BadRequestResult(); }
                }
            }
            catch { return new BadRequestResult(); }
            return new BadRequestResult();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Product model)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            product.ProductName = model.ProductName;
            product.ProductId = model.ProductId;
            product.BestSellers = model.BestSellers;
            product.Descriptions = model.Descriptions;
            product.Price = model.Price;
            product.Discount = model.Discount;
            product.UnitslnStock = model.UnitslnStock;
            product.CatId = model.CatId;
            product.Thumb = model.Thumb;
            
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
