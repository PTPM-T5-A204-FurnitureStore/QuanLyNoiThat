using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebsiteNoiThat.Models;

namespace WebsiteNoiThat.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        Uri baseAddress = new Uri("https://localhost:7053/api");
        private readonly HttpClient _client;
        public CategoryViewComponent()
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
                // Xử lý trường hợp không thành công (ví dụ: ném một ngoại lệ hoặc trả về một thông báo lỗi)
            }


            return View(loaihangList);
        }
    }
}

