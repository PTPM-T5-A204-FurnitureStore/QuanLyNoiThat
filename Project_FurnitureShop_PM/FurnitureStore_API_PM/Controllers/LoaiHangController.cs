using FurnitureStore_API_PM.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace FurnitureStore_API_PM.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LoaiHangController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _sqlDataSource;

        public LoaiHangController(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
        }


        [HttpPut("{id}")]
        public IActionResult UpdateLoaiHang(int id, [FromBody] LoaiHang updatedLoaiHang)
        {
            if (updatedLoaiHang == null)
            {
                return BadRequest(); 
            }

            string query = @"UPDATE loaihang SET TenLoai = @TenLoai WHERE MaLoai = @MaLoai";

          

            using (MySqlConnection mycon = new MySqlConnection(_sqlDataSource))
            {
                mycon.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaLoai", id);
                    myCommand.Parameters.AddWithValue("@TenLoai", updatedLoaiHang.TenLoai);

                    int rowsAffected = myCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound(); 
                    }
                }
            }
        }


        [HttpGet("checkconnection")]
        public IActionResult CheckConnection()
        {
            using (MySqlConnection connection = new MySqlConnection(_sqlDataSource))
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
            string query = @"SELECT MaLoai, TenLoai, Anh FROM loaihang";

            List<LoaiHang> loaiHangs = new List<LoaiHang>();

           

            using (MySqlConnection mycon = new MySqlConnection(_sqlDataSource))
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
                                TenLoai = myReader.GetString("TenLoai"),
                                Anh= myReader.GetString("Anh")
                            };
                            loaiHangs.Add(loaiHang);
                        }
                    }
                }
            }

            return Ok(loaiHangs);
        }


        [HttpGet]
        public IActionResult GetLoaiHangbyID(string id)
        {
            string query = @"SELECT MaLoai, TenLoai, Anh FROM loaihang WHERE MaLoai=@id";

            List<LoaiHang> loaiHangs = new List<LoaiHang>();



            using (MySqlConnection mycon = new MySqlConnection(_sqlDataSource))
            {
                mycon.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            LoaiHang loaiHang = new LoaiHang
                            {
                                MaLoai = myReader.GetInt32("MaLoai"),
                                TenLoai = myReader.GetString("TenLoai"),
                                Anh = myReader.GetString("Anh")
                            };
                            loaiHangs.Add(loaiHang);
                        }
                    }
                }
            }

            return Ok(loaiHangs);
        }


        [HttpPost]
        public IActionResult CreateLoaiHang([FromBody] LoaiHang newLoaiHang)
        {
            if (newLoaiHang == null)
            {
                return BadRequest(); 
            }

            string query = @"INSERT INTO loaihang (MaLoai, TenLoai) VALUES (@MaLoai, @TenLoai)";

          

            using (MySqlConnection mycon = new MySqlConnection(_sqlDataSource))
            {
                mycon.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaLoai", newLoaiHang.MaLoai);
                    myCommand.Parameters.AddWithValue("@TenLoai", newLoaiHang.TenLoai);

                    int rowsAffected = myCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok(); 
                    }
                    else
                    {
                        return StatusCode(500); 
                    }
                }
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteLoaiHang(int id)
        {
            string query = @"DELETE FROM loaihang WHERE MaLoai = @MaLoai";


            using (MySqlConnection mycon = new MySqlConnection(_sqlDataSource))
            {
                mycon.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaLoai", id);

                    int rowsAffected = myCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok(); 
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }


    }
}
