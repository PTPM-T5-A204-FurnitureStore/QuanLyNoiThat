using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebsiteNoiThat.Models;

namespace WebsiteNoiThat.ViewComponents
{
    public class ListProductViewComponent : ViewComponent
    {
        Uri baseAddress = new Uri("https://localhost:7053/api");
        private readonly HttpClient _client;
        public ListProductViewComponent()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<SanPham> sanphamList = new List<SanPham>();

            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/SanPham/GetSanPham");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                sanphamList = JsonConvert.DeserializeObject<List<SanPham>>(data);
            }
            else
            {
                // Xử lý trường hợp không thành công (ví dụ: ném một ngoại lệ hoặc trả về một thông báo lỗi)
            }


            return View(sanphamList);
        }
    }
}
