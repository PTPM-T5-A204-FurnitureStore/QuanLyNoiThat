using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebsiteNoiThat.Models;

namespace WebsiteNoiThat.ViewComponents
{
    public class Category1ViewComponent : ViewComponent
    {
        Uri baseAddress = new Uri("https://localhost:7053/api");
        private readonly HttpClient _client;
        public Category1ViewComponent()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<LoaiHang> loaihangList = new List<LoaiHang>();

            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/LoaiHang/GetLoaiHang");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                loaihangList = JsonConvert.DeserializeObject<List<LoaiHang>>(data);
            }
            else
            {
               
            }


            return View(loaihangList);
        }
    }
}
