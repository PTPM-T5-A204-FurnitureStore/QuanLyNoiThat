using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using WebsiteNoiThat.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebsiteNoiThat.Controllers
{
    public class AccountController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7053/api");
        private readonly HttpClient _client;
        public AccountController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }
       

        public async Task<ActionResult> Login(string currentUrl)
        {
            HttpContext.Session.SetString("returnCurrentUrl", currentUrl);
            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string account1, string pass1)
        {
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/KhachHang/GetLoginKhachHang?account={account1}&password={pass1}");

            if (response.IsSuccessStatusCode)
            {
                List<KhachHang> listKH = new List<KhachHang>(); 
                string data = await response.Content.ReadAsStringAsync();
                if(data != "[]")
                {
                    listKH = JsonConvert.DeserializeObject<List<KhachHang>>(data);
                    HttpContext.Session.SetString("IDCustomer", listKH[0].MaKH.ToString());

                    string url = HttpContext.Session.GetString("returnCurrentUrl");

                    return Redirect(url);
                }
                else
                {
                    ViewBag.UserName = "Tài khoản hoặc mật khẩu sai";
                    return View();
                }
                
            }
           

            return View();
        }


    }
}

