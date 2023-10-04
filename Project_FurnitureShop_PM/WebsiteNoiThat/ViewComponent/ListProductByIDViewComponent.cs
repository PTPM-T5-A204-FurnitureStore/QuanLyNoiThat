using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebsiteNoiThat.Models;

namespace WebsiteNoiThat.ViewComponents
{
	public class ListProductByIDViewComponent : ViewComponent
	{
        Uri baseAddress = new Uri("https://localhost:7053/api");
        private readonly HttpClient _client;
        public ListProductByIDViewComponent()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(string categoryId)
        {
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/SanPham/GetSanPhambyidLoaiHang?idloai={categoryId}");
            List<SanPham> listKH = new List<SanPham>();
            if (response.IsSuccessStatusCode)
            {

                string data = await response.Content.ReadAsStringAsync();
                if (data != "[]")
                {
                    listKH = JsonConvert.DeserializeObject<List<SanPham>>(data);

                }
              

            }
            return View(listKH);
        }
    }
}
