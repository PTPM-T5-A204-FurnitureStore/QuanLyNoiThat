using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebsiteNoiThat.Models;

namespace WebsiteNoiThat.Controllers
{
	public class ProductController : Controller
	{

		Uri baseAddress = new Uri("https://localhost:7053/api");
		private readonly HttpClient _client;
		public ProductController()
		{
			_client = new HttpClient();
			_client.BaseAddress = baseAddress;

		}



		public async Task<IActionResult> Index(string idLoaiHang)
		{
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/LoaiHang/GetLoaiHangbyID?id={idLoaiHang}");
            List<LoaiHang> listSP = new List<LoaiHang>();
            if (response.IsSuccessStatusCode)
            {

                string data = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(data) || data!="[0]")
                {
                    listSP = JsonConvert.DeserializeObject<List<LoaiHang>>(data);


                }
                

            }
            return View(listSP);
        }



	}
}
