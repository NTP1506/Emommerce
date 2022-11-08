using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using AspNetCoreHero.ToastNotification.Abstractions;
using CustomerSite.Extension;
using CustomerSite.Helpper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Share_Models;
using CustomerSite.Extension;
using CustomerSite.Helpper;
using CustomerSite.Models;
using CustomerSite.ModelViews;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.VisualBasic;
using NuGet.Protocol.Plugins;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerSite.Controllers
{
    
    public class CheckoutController : Controller
    {
        //string Numrd_str;
        //Random rd = new Random();
        //public int check ;
        private HttpClient _httpClient;
        List<Product> _products;
        //public INotyfService _notyfService { get; }
        public CheckoutController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7137");
        }
        public List<CartItem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;
            }
        }
        [Route("checkout.html", Name = "Checkout")]
        public async Task<IActionResult> Index(string returnUrl = null)
        {

            var response = await _httpClient.GetAsync("/customer-get");
            var content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            var _customers = JsonConvert.DeserializeObject<List<Customer>>(content);
            //Lay gio hang ra de xu ly
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            //lay thong tin customer dang login 
            //var taikhoan_ID = HttpContext.Session.GetString("CustomerId_LogIn");
            var taikhoan_ID = Request.Cookies["token"];
            if (taikhoan_ID == null)
            {
                return Redirect("dang-nhap.html");
            }
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken UserName = handler.ReadJwtToken(taikhoan_ID);
           
            //lay attribute name cua nguoi dang nhap.
            string taikhoanID= UserName.Claims.Where(c => c.Type == ClaimTypes.Name).SingleOrDefault().Value.ToString();
            MuaHangVM model = new MuaHangVM();
            if (taikhoanID != null)
            {
                var khachhang = _customers.SingleOrDefault(x => x.FullName == Convert.ToString(taikhoanID));
                model.CustomerId = khachhang.CustomerId;
                model.FullName = khachhang.FullName;
                model.Email = khachhang.Email;
                model.Phone = khachhang.Phone;
                model.Address = khachhang.Address;
            }
            //ViewData["lsTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderBy(x => x.Type).ToList(), "Location", "Name");
            ViewBag.GioHang = cart;
            return View(model);
        }

        [HttpPost]
        [Route("checkout.html", Name = "Checkout")]
        public async Task<IActionResult> Index(MuaHangVM muaHang)
        {
           

            var response = await _httpClient.GetAsync("/customer-get");
            var content = await response.Content.ReadAsStringAsync();  
            var _customers = JsonConvert.DeserializeObject<List<Customer>>(content);

            response = await _httpClient.GetAsync("/Order/OrderlastID");
            content = await response.Content.ReadAsStringAsync();  
            var order_lastID = JsonConvert.DeserializeObject<Order>(content);
           
            //Lay ra gio hang de xu ly
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");

            //var taikhoan_ID = HttpContext.Session.GetString("");
            var taikhoan_ID = Request.Cookies["token"];
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken UserName = handler.ReadJwtToken(taikhoan_ID);
            string taikhoanID = UserName.Claims.Where(c => c.Type == ClaimTypes.Name).SingleOrDefault().Value.ToString();
            MuaHangVM model = new MuaHangVM();
            if (taikhoanID != null)
            {
                var khachhang = _customers.SingleOrDefault(x => x.FullName == Convert.ToString(taikhoanID));
                model.CustomerId = khachhang.CustomerId;
                model.FullName = khachhang.FullName;
                model.Email = khachhang.Email;
                model.Phone = khachhang.Phone;
                model.Address = khachhang.Address;

                //khachhang.LocationId = muaHang.TinhThanh;
                //khachhang.District = muaHang.QuanHuyen;
                //khachhang.Ward = muaHang.PhuongXa;
                khachhang.Address = muaHang.Address;
                //response = await _httpClient.PostAsJsonAsync("customerpost", khachhang);
                //content = await response.Content.ReadAsStringAsync();  
               
            }
            try
            {
                if (ModelState.IsValid)
                {
                    //Khoi tao don hang
                    Order donhang = new Order();
                    donhang.OrderId = (int)order_lastID.OrderId + 1;
                    donhang.CustomerId = model.CustomerId;
                    //donhang.Address = model.Address;
                    //donhang.LocationId = model.TinhThanh;
                    //donhang.District = model.QuanHuyen;
                    //donhang.Ward = model.PhuongXa;
                    donhang.ShipDate = DateTime.Now;
                    donhang.OrderDate = DateTime.Now;
                    donhang.TransactStatusId = 1;//Don hang moi
                    donhang.Deleted = false;
                    donhang.Paid = false;
                    donhang.Note = Utilities.StripHTML(model.Note);
                    donhang.TotalMoney = Convert.ToInt32(cart.Sum(x => x.TotalMoney));
                    //_context.Add(donhang);
                    //_context.SaveChanges();
                    //tao danh sach don hang
                    response = await _httpClient.PostAsJsonAsync("Order", donhang);
                    var count = 0;
                    foreach (var item in cart)
                    {
                       
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderDetailId = donhang.OrderId * 100 + (count++);
                        orderDetail.OrderId = donhang.OrderId;
                        orderDetail.ProductId = item.product.ProductId;
                        orderDetail.ProductName = item.product.ProductName;
                        orderDetail.Amount = item.amount;
                        //orderDetail.Total = donhang.TotalMoney;
                        orderDetail.Total = item.product.Price;
                        if(orderDetail.Amount > 1)
                        {
                            orderDetail.Total = orderDetail.Total.Value * orderDetail.Amount.Value;
                        }    
                        //orderDetail.Price = item.product.Price;
                        //orderDetail.CreateDate = DateTime.Now;
                        response = await _httpClient.PostAsJsonAsync("OrderDetail", orderDetail);
                        //_context.Add(orderDetail);
                    }
                    //_context.SaveChanges();
                    //clear gio hang
                    HttpContext.Session.Remove("GioHang");
                    //Xuat thong bao
                    //_notyfService.Success("Đơn hàng đặt thành công");
                    //cap nhat thong tin khach hang
                    return RedirectToAction("Success");


                }
            }
            catch
            {
                //ViewData["lsTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderBy(x => x.Type).ToList(), "Location", "Name");
                ViewBag.GioHang = cart;
                return View(model);
            }
           // ViewData["lsTinhThanh"] = new SelectList(_context.Locations.Where(x => x.Levels == 1).OrderBy(x => x.Type).ToList(), "Location", "Name");
            ViewBag.GioHang = cart;
            return View(model);
        }
        [Route("dat-hang-thanh-cong.html", Name = "Success")]
        public async Task<IActionResult> Success()
        {
            var response = await _httpClient.GetAsync("/customer-get");
            var content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            var _customers = JsonConvert.DeserializeObject<List<Customer>>(content);

            response = await _httpClient.GetAsync("Order");
            content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            var _orders = JsonConvert.DeserializeObject<List<Order>>(content);
            try
            {
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                if (string.IsNullOrEmpty(taikhoanID))
                {
                    return RedirectToAction("Login", "Accounts", new { returnUrl = "/dat-hang-thanh-cong.html" });
                }
                var khachhang = _customers.SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taikhoanID));
                var donhang = _orders
                    .Where(x => x.CustomerId == Convert.ToInt32(taikhoanID))
                    .OrderByDescending(x => x.OrderDate)
                    .FirstOrDefault();
                MuaHangSuccessVM successVM = new MuaHangSuccessVM();
                successVM.FullName = khachhang.FullName;
                successVM.DonHangID = donhang.OrderId;
                successVM.Phone = khachhang.Phone;
                successVM.Address = khachhang.Address;
                //successVM.PhuongXa = GetNameLocation(donhang.Ward.Value);
                //successVM.TinhThanh = GetNameLocation(donhang.District.Value);
                return View(successVM);
            }
            catch
            {
                return View();
            }
        }
        //public string GetNameLocation(int idlocation)
        //{
        //    try
        //    {
        //        var location = _context.Locations.AsNoTracking().SingleOrDefault(x => x.LocationId == idlocation);
        //        if (location != null)
        //        {
        //            return location.NameWithType;
        //        }
        //    }
        //    catch
        //    {
        //        return string.Empty;
        //    }
        //    return string.Empty;
        //}
    }
}
