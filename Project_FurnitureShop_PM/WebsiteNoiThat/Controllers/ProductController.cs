using FurnitureStore_API_PM.Model;
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
		public async Task<IActionResult> FilterProducts(string idLoaiHang, decimal? minPrice, decimal? maxPrice, int? maChatLieu, int? maXuatXu, string? sortOrder)
		{
			// Lấy danh mục sản phẩm dựa trên idLoaiHang
			HttpResponseMessage categoryResponse = await _client.GetAsync(_client.BaseAddress + $"/LoaiHang/GetLoaiHangbyID?id={idLoaiHang}");
			List<LoaiHang> listLoaiHang = new List<LoaiHang>();

			if (categoryResponse.IsSuccessStatusCode)
			{
				string categoryData = await categoryResponse.Content.ReadAsStringAsync();
				if (!string.IsNullOrEmpty(categoryData) && categoryData != "[]")
				{
					listLoaiHang = JsonConvert.DeserializeObject<List<LoaiHang>>(categoryData);
				}
			}

			// Lấy sản phẩm đã lọc dựa trên các giá trị filter
			HttpResponseMessage filteredResponse = await _client.GetAsync(_client.BaseAddress + $"/SanPham/GetSanPhamByFilters/Filter?idloai={idLoaiHang}&minPrice={minPrice}&maxPrice={maxPrice}&maChatLieu={maChatLieu}&maXuatXu={maXuatXu}&sortOrder={sortOrder}");
			List<SanPham> filteredSanPhams = new List<SanPham>();

			if (filteredResponse.IsSuccessStatusCode)
			{
				string filteredData = await filteredResponse.Content.ReadAsStringAsync();
				if (!string.IsNullOrEmpty(filteredData) && filteredData != "[]")
				{
					filteredSanPhams = JsonConvert.DeserializeObject<List<SanPham>>(filteredData);
				}
			}

			// Gửi danh sách sản phẩm đã lọc và danh sách danh mục sang View
			ViewBag.LoaiHang = listLoaiHang; // Sử dụng ViewBag để truyền dữ liệu sang View
			ViewBag.SanPham = filteredSanPhams;

			return View("ListProductByID");
		}
		[HttpGet]
		public async Task<IActionResult> GetChatLieu()
		{
			List<ChatLieu> chatlieus = new List<ChatLieu>();
			HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/ChatLieu/GetChatLieu");

			if (response.IsSuccessStatusCode)
			{

				string data = await response.Content.ReadAsStringAsync();
				if (!string.IsNullOrEmpty(data) || data != "[0]")
				{
					chatlieus = JsonConvert.DeserializeObject<List<ChatLieu>>(data);

				}
				else
				{

				}

			}
			ViewBag.ChatLieu = chatlieus;
			return View(chatlieus);
		}

		[HttpGet]
		public async Task<IActionResult> GetXuatXu()
		{
			List<XuatXu> xuats = new List<XuatXu>();
			HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/XuatXu/GetXuatXu");

			if (response.IsSuccessStatusCode)
			{

				string data = await response.Content.ReadAsStringAsync();
				if (!string.IsNullOrEmpty(data) || data != "[0]")
				{
					xuats = JsonConvert.DeserializeObject<List<XuatXu>>(data);

				}
				else
				{

				}

			}
			ViewBag.Xuats = xuats;
			return View(xuats);
		}

	}
}
