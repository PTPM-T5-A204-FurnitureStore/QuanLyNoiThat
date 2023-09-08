using FurnitureStore_API_PM.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace FurnitureStore_API_PM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiHangController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public LoaiHangController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet("checkconnection")]
        public IActionResult CheckConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return Ok("Kết nối đến MySQL thành công!");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi kết nối đến MySQL: " + ex.Message);
                }
            }
        }


        [HttpGet]
        public IActionResult GetLoaiHang()
        {
            string query = @"SELECT MaLoai, TenLoai FROM loaihang";

            List<LoaiHang> loaiHangs = new List<LoaiHang>();

            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    using (MySqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            LoaiHang loaiHang = new LoaiHang
                            {
                                MaLoai = myReader.GetInt32("MaLoai"),
                                TenLoai = myReader.GetString("TenLoai")
                            };
                            loaiHangs.Add(loaiHang);
                        }
                    }
                }
            }

            return Ok(loaiHangs);
        }
    }
}
