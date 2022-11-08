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
            //Assert
            Assert.NotNull(data);
            Assert.NotEmpty(data);
            Assert.Equivalent(_customers, data);
        }

        [Theory]
        [InlineData(1)]
        public void GetId_Success(int id)
        {
            //ARRANGE
            CustomerController customerController = new CustomerController(_context);

            //ACT
            var result = customerController.GetID(id).Result.Result as ObjectResult;
            Customer? data = (Customer?)result.Value;
            Assert.NotNull(data);
            Assert.Equivalent(_customers.SingleOrDefault(c => c.CustomerId == id), data);
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
            // Assert
            Assert.NotNull(result);
            Assert.Equal(_context.Customers.LastOrDefault().CustomerId, id);
            Assert.Equal(_context.Customers.LastOrDefault().FullName, name);
            Assert.Equal(_context.Customers.LastOrDefault().Phone, phone);
            Assert.Equal(_context.Customers.LastOrDefault().Email, email);
        }

        [Theory]
        [InlineData(1, "customer_1'", "123456789", "customer11@gmail.com")]
        [InlineData(2, "customer_2'", "123456789", "customer22@gmail.com")]
        [InlineData(3, "customer_3'", "123456789", "customer33@gmail.com")]
        public void Upadate_Success(int id, string name, string phone, string email)
        {
            //ARRANGE
            CustomerController customerController = new CustomerController(_context);
            Customer p = new Customer() { CustomerId = id, FullName = name, Phone = phone, Email = email };
            //ACT
            var result = customerController.Update(id,p).Result;
            Assert.NotNull(result);
            Assert.Equivalent(_context.Customers.SingleOrDefault(c=>c.CustomerId == id), p);
        }
        [Theory]
        [InlineData(9, "customer_1'", "123456789", "customer11@gmail.com")]
        [InlineData(10, "customer_2'", "123456789", "customer22@gmail.com")]
        [InlineData(11, "customer_3'", "123456789", "customer33@gmail.com")]
        public void Upadate_Fail(int id, string name, string phone, string email)
        {
            //ARRANGE
            CustomerController customerController = new CustomerController(_context);
            Customer p = new Customer() { CustomerId = id, FullName = name, Phone = phone, Email = email };
            //ACT
            var result = customerController.Update(id, p).Result;
            Assert.IsType<NotFoundResult>(result);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Delete_Success(int id)
        {
            //ARRANGE
            CustomerController customerController = new CustomerController(_context);
            Customer p = new Customer() { CustomerId = id };
            //ACT
            var result = customerController.Delete(id).Result;
            Assert.NotNull(result);
            Customer c = _context.Customers.SingleOrDefault(p =>p.CustomerId == id);
            Assert.Null(c);
            Assert.IsType<AcceptedResult>(result);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(22)]
        [InlineData(33)]
        public void Delete_Fail(int id)
        {
            //ARRANGE
            CustomerController customerController = new CustomerController(_context);
            //ACT
            var result = customerController.Delete(id).Result;
            Assert.IsType<NotFoundResult>(result);
        }


        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }


    }
}
