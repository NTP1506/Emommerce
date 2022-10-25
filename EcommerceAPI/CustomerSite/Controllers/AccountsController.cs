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
using PagedList.Core;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
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
        Check _check;
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
           
            var taikhoanID_Register = HttpContext.Session.GetString("CustomerId_Register");
            //string UserName = taikhoanID.Claims.Where(c => c.Type == ClaimTypes.Name).SingleOrDefault().Value.ToString();
            var a = HttpContext.Session.GetString("CustomerId_LogIn"); 
            if(a!= null)
            {
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken UserName = handler.ReadJwtToken(a);
                string taikhoanID_Login = UserName.Claims.Where(c => c.Type == ClaimTypes.Name).SingleOrDefault().Value.ToString();
                if (taikhoanID_Login != null)
                {
                    var khachhang = _customers.SingleOrDefault(x => x.FullName == Convert.ToString(taikhoanID_Login));
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
            //var taikhoanID_Login = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID_Register != null)
            {
                
                var taikhoan =  Convert.ToString(taikhoanID_Register);
                var khachhang = _customers.SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taikhoanID_Register));
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
                        HttpContext.Session.SetString("CustomerId_Register", khachhangID.CustomerId.ToString());
                        var taikhoanid = HttpContext.Session.GetString("CustomerId_Register");

                        //Identity
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,khachhang.FullName),
                            new Claim("CustomerId", khachhang.CustomerId.ToString())
                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);


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

                    response = await _httpClient.PostAsJsonAsync("Account/Login", customer);
                    content = await response.Content.ReadAsStringAsync();
                    JwtResponseToken responseToken = JsonConvert.DeserializeObject<JwtResponseToken>(content);
                    var handler = new JwtSecurityTokenHandler();
                    JwtSecurityToken secureToken = handler.ReadJwtToken(responseToken.Token);
                    //identity
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,khachhang.FullName),
                            new Claim("CustomerId", khachhang.CustomerId.ToString())
                        };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);

                    if (secureToken != null)
                    {
                        //Luu Session MaKh
                        //string UserName = secureToken.Claims.Where(c => c.Type == ClaimTypes.Name).SingleOrDefault().Value.ToString();
                        HttpContext.Session.SetString("CustomerId_LogIn", responseToken.Token);
                        //var taikhoanID = HttpContext.Session.GetString("CustomerId");
                        //string UserName = secureToken.Claims.Where(c => c.Type == ClaimTypes.Name).SingleOrDefault().Value.ToString();
                        
                        return RedirectToAction("Dashboard", "Accounts");
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
        [AllowAnonymous]
        [Route("dang-xuat.html", Name = "DangXuat")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("CustomerId_LogIn");
            HttpContext.Session.Remove("CustomerId_Register");
            return RedirectToAction("Index", "Home");
        }

        //[HttpPut]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var response = await _httpClient.GetAsync("/customer-get");
                var content = await response.Content.ReadAsStringAsync();  
                var _customers = JsonConvert.DeserializeObject<List<Customer>>(content);
                var taikhoan_ID = HttpContext.Session.GetString("CustomerId_LogIn");
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken UserName = handler.ReadJwtToken(taikhoan_ID);
                //lay attribute name cua nguoi dang nhap.
                string taikhoanID = UserName.Claims.Where(c => c.Type == ClaimTypes.Name).SingleOrDefault().Value.ToString();
                
                if (taikhoanID == null)
                {
                    return RedirectToAction("Login", "Accounts");
                }
                if (ModelState.IsValid)
                {
                    var khachhang = _customers.SingleOrDefault(x => x.FullName == Convert.ToString(taikhoanID));
                    if (khachhang == null) return RedirectToAction("Login", "Accounts");
                    var pass = (model.PasswordNow.Trim() + khachhang.Salt.Trim()).ToMD5();
                    {
                        string passnew = (model.Password.Trim() + khachhang.Salt.Trim()).ToMD5();
                        khachhang.Password = passnew;
                        var id = khachhang.CustomerId;
                        response = await _httpClient.PutAsJsonAsync($"/{id}", khachhang);
                        //_context.Update(taikhoan);
                        //_context.SaveChanges();
                        // _notyfService.Success("Đổi mật khẩu thành công");
                        return RedirectToAction("Dashboard", "Accounts");
                    }
                }
            }
            catch
            {
                // _notyfService.Success("Thay đổi mật khẩu không thành công");
                return RedirectToAction("Dashboard", "Accounts");
            }
            //_notyfService.Success("Thay đổi mật khẩu không thành công");
            return RedirectToAction("Dashboard", "Accounts");
        }

    }
}
