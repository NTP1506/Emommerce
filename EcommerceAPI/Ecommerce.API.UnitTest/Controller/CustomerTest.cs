using EcommerceAPI.Controllers;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.API.UnitTest.Controller
{
    public class CustomerTest : IDisposable
    {
        private readonly DbContextOptions<dbEcommerceContext> _options;
        private readonly dbEcommerceContext _context;
        private readonly List<Customer> _customers;

        public CustomerTest()
        {
            _options = new DbContextOptionsBuilder<dbEcommerceContext>().UseInMemoryDatabase("CustomerTestDB").Options;
            _context = new dbEcommerceContext(_options);
            // Create Fake Data
            _customers = new()
            {
                new Customer(){CustomerId = 1, FullName = "Customer_1",Phone = "123456789" , Email = "customer1@gmail.com" },
                new Customer(){CustomerId = 2, FullName = "Customer_2",Phone = "123456789" , Email = "customer2@gmail.com" },
                new Customer(){CustomerId = 3, FullName = "Customer_3",Phone = "123456789" , Email = "customer3@gmail.com" },
                new Customer(){CustomerId = 4, FullName = "Customer_4",Phone = "123456789" , Email = "customer4@gmail.com" },
                new Customer(){CustomerId = 5, FullName = "Customer_5",Phone = "123456789" , Email = "customer5@gmail.com" },
            };
            _context.Database.EnsureDeleted();
            _context.Customers.AddRange(_customers);
            _context.SaveChanges();
        }
        [Fact]
        public void GetAll_Success()
        {
            //ARRANGE
            CustomerController customerController = new CustomerController(_context);

            //ACT
            var result = customerController.Get().Result.Result as ObjectResult;
            List<Customer>? data = (List<Customer>?)result.Value;
            Assert.NotNull(data);
            Assert.NotEmpty(data);
            Assert.Equivalent(_customers, data);
        }

        [Theory]
        [InlineData(6, "customer_6", "123456789","customer6@gmail.com")]
        [InlineData(7, "customer_7", "123456789", "customer7@gmail.com")]
        [InlineData(8, "customer_8", "123456789", "customer8@gmail.com")]
        public void Create_Success(int id, string name, string phone, string email)
        {
            //ARRANGE
            CustomerController customerController = new CustomerController(_context);
            Customer p = new Customer() { CustomerId = id, FullName = name, Phone = phone , Email = email };
            //ACT
            var result = customerController.Create(p).Result;
            Assert.NotNull(result);
            Assert.Equal(_context.Customers.LastOrDefault().CustomerId, id);
            Assert.Equal(_context.Customers.LastOrDefault().FullName, name);
            Assert.Equal(_context.Customers.LastOrDefault().Phone, phone);
            Assert.Equal(_context.Customers.LastOrDefault().Email, email);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }


    }
}
