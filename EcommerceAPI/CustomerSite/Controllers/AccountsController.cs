using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
//using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerSite.Extension;
using CustomerSite.Helpper;
using CustomerSite.Models;
using CustomerSite.ModelViews;
using Share_Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerSite.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        
        // private IHttpClientFactory clientFactory;
        private HttpClient _httpClient;
        List<Customer> _customers;
        List<Order> _orders;
        
        public AccountsController()
        {
           
            //this.clientFactory = clientFactory;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7137");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ValidatePhone(string Phone)
        {
            var response = await _httpClient.GetAsync("/customer-get");
            var content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            _customers = JsonConvert.DeserializeObject<List<Customer>>(content);

            try
            {
                var khachhang = _customers.SingleOrDefault(x => x.Phone.ToLower() == Phone.ToLower());
                if (khachhang != null)
                    return Json(data: "Số điện thoại : " + Phone + "đã được sử dụng");

                return Json(data: true);

            }
            catch
            {
                return Json(data: true);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateEmail(string Email)
        {

            var response = await _httpClient.GetAsync("Order/Get");
            var content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            _orders = JsonConvert.DeserializeObject<List<Order>>(content);
            try
            {
                var khachhang = _customers.SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
                if (khachhang != null)
                    return Json(data: "Email : " + Email + " đã được sử dụng");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }
        
        [AllowAnonymous]
        [Route("tai-khoan-cua-toi.html", Name = "Dashboard")]
        public async Task< IActionResult> Dashboard()
        {
            var response = await _httpClient.GetAsync("/customer-get");
            var content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            _customers = JsonConvert.DeserializeObject<List<Customer>>(content);

            response = await _httpClient.GetAsync("Order");
            content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            _orders = JsonConvert.DeserializeObject<List<Order>>(content);
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                var khachhang = _customers.SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    var lsDonHang = _orders
                        .Where(x => x.CustomerId == khachhang.CustomerId)
                        .OrderByDescending(x => x.OrderDate)
                        .ToList();
                    ViewBag.DonHang = lsDonHang;
                    return View(khachhang);
                }

            }
            return RedirectToAction("Login");
        }
        //[HttpGet]
        [AllowAnonymous]
        [Route("dang-ky.html", Name = "DangKy")]
        public IActionResult DangkyTaiKhoan()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-ky.html", Name = "DangKy")]
        public async Task<IActionResult> DangkyTaiKhoan(RegisterViewModel taikhoan)
        {
            Customer khachhangID = new Customer();
            try
            {
                if (ModelState.IsValid)
                {
                    string salt = Utilities.GetRandomKey();
                    Customer khachhang = new Customer
                    {
                        FullName = taikhoan.FullName,
                        Phone = taikhoan.Phone.Trim().ToLower(),
                        Email = taikhoan.Email.Trim().ToLower(),
                        Password = (taikhoan.Password + salt.Trim()).ToMD5(),
                        Active = true,
                        Salt = salt,
                        CreateDate = DateTime.Now
                    };
                    try
                    {
                        var jsonInString = JsonConvert.SerializeObject(taikhoan);
                       
                        var response = await _httpClient.PostAsJsonAsync("Account/register", taikhoan);
                        //var response = await _httpClient.PostAsync("/Account/register", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                        //var response = await _httpClient.PostAsync("Account/register", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                        var contents = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<RegisterViewModel>(contents);
                        // post list customer to database
                        response = await _httpClient.PostAsJsonAsync("/customerpost", khachhang);
                        contents = await response.Content.ReadAsStringAsync();

                        // get the last customer id to save session
                        response = await _httpClient.GetAsync("/CustomerlastID");
                        contents = await response.Content.ReadAsStringAsync();  // laasys body cua data
                        khachhangID = JsonConvert.DeserializeObject<Customer>(contents);
                        HttpContext.Session.SetString("CustomerId", khachhangID.CustomerId.ToString());
                        var taikhoanid = HttpContext.Session.GetString("CustomerId");
                        //data = JsonConvert.DeserializeObject<RegisterViewModel>(contents);
                        if ( data.StatusCode != null)
                        {
                            return RedirectToAction("Dashboard", "Accounts");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Failed to register");
                            return RedirectToAction("DangkyTaiKhoan", "Accounts");
                        }

                    }
                    catch (Exception ex)
                    {

                        return RedirectToAction("DangkyTaiKhoan", "Accounts");
                    }
                }
                else
                {
                    return View(taikhoan);
                }
            }
            catch
            {
                return View(taikhoan);
            }
        }
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public IActionResult Login(string returnUrl = null)
        {
            //var taikhoanID = HttpContext.Session.GetString("CustomerId");
            //if (taikhoanID != null)
            //{
            //    return RedirectToAction("Dashboard", "Accounts");
            //}
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public async Task<IActionResult> Login(LoginViewModel customer/*, string returnUrl*/)
        {
            var response = await _httpClient.GetAsync("/customer-get");
            var content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            _customers = JsonConvert.DeserializeObject<List<Customer>>(content);
           
            try
            {
                if (ModelState.IsValid)
                {
                    bool isEmail = Utilities.IsValidEmail(customer.UserName);
                    if (!isEmail) return View(customer);

                    var khachhang = _customers.SingleOrDefault(x => x.Email.Trim() == customer.UserName);

                    if (khachhang == null) return RedirectToAction("DangkyTaiKhoan");
                    string pass = (customer.Password + khachhang.Salt.Trim()).ToMD5();
                    if (khachhang.Password != pass)
                    {
                        // _notyfService.Success("Thông tin đăng nhập chưa chính xác");
                        return View(customer);
                    }
                    //kiem tra xem account co bi disable hay khong

                    if (khachhang.Active == false)
                    {
                        return RedirectToAction("ThongBao", "Accounts");
                    }

                    response = await _httpClient.PostAsJsonAsync("Account/login", customer);
                    content = await response.Content.ReadAsStringAsync();
                    JwtResponseToken responseToken = JsonConvert.DeserializeObject<JwtResponseToken>(content);
                    var handler = new JwtSecurityTokenHandler();
                    JwtSecurityToken secureToken = handler.ReadJwtToken(responseToken.Token);

                    if (secureToken != null)
                    {
                        //Luu Session MaKh
                        HttpContext.Session.SetString("CustomerId", secureToken.ToString() );
                        var taikhoanID = HttpContext.Session.GetString("CustomerId");
                    }   
                   
                }
            }
            catch
            {
                return RedirectToAction("DangkyTaiKhoan", "Accounts");
            }
            return View(customer);
        }
        [HttpGet]
        [Route("dang-xuat.html", Name = "DangXuat")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("CustomerId");
            return RedirectToAction("Index", "Home");
        }

    }
}
