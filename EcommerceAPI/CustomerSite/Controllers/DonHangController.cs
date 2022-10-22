using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Share_Models;
using CustomerSite.ModelViews;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CustomerSite.Controllers
{
    public class DonHangController : Controller
    {
        private HttpClient _httpClient;
        //List<Product> _products;
        List<Customer> _customers;
        public DonHangController(/*dbMarketsContext context, INotyfService notyfService*/)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7137");
        }
        [HttpPost]
        public async Task<IActionResult> Details(int? id)
        {
            var response = await _httpClient.GetAsync("/customer-get");
            var content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            _customers = JsonConvert.DeserializeObject<List<Customer>>(content);


            response = await _httpClient.GetAsync("Order");
            content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            var _orders = JsonConvert.DeserializeObject<List<Order>>(content);

            response = await _httpClient.GetAsync("/OrderDetail");
            content = await response.Content.ReadAsStringAsync();  // laasys body cua data
            var _orderdetails = JsonConvert.DeserializeObject<List<OrderDetail>>(content);

            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var taikhoan_ID = HttpContext.Session.GetString("CustomerId_LogIn");
                if (string.IsNullOrEmpty(taikhoan_ID)) return RedirectToAction("Login", "Accounts");
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken UserName = handler.ReadJwtToken(taikhoan_ID);
                string taikhoanID = UserName.Claims.Where(c => c.Type == ClaimTypes.Name).SingleOrDefault().Value.ToString();
                var khachhang = _customers.SingleOrDefault(x => x.FullName == Convert.ToString(taikhoanID));
               
                if (khachhang == null) return NotFound();
                var donhang = _orders.FirstOrDefault
                   (m => m.OrderId == id && Convert.ToInt32(khachhang.CustomerId) == m.CustomerId);
                if (donhang == null) return NotFound();

                var chitietdonhang = _orderdetails
                    .Where(x => x.OrderId == id)
                    .OrderBy(x => x.OrderDetailId)
                    .ToList();
                XemDonHang donHang = new XemDonHang();
                donHang.DonHang = donhang;
                donHang.ChiTietDonHang = chitietdonhang;
                return PartialView("Details", donHang);

            }
            catch
            {
                return NotFound();
            }
        }
    }
}
