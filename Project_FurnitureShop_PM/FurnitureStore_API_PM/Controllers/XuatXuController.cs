using FurnitureStore_API_PM.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace FurnitureStore_API_PM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XuatXuController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public XuatXuController(IConfiguration configuration)
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
        public IActionResult GetXuatXu()
        {
            string query = @"SELECT MaXuatXu, TenXuatXu FROM xuatxu";

            List<XuatXu> xuatXus = new List<XuatXu>();

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
                            XuatXu xuatxu = new XuatXu
                            {
                                MaXuatXu = myReader.GetInt32("MaXuatXu"),
                                TenXuatXu = myReader.GetString("TenXuatXu")
                            };
                            xuatXus.Add(xuatxu);
                        }
                    }
                }
            }
            return Ok(xuatXus);
        }

        [Route("api/xuatxu")]
        [HttpPost]
        public IActionResult PostXuatXu([FromBody] XuatXu xuatXu)
        {
            if (xuatXu == null)
            {
                return BadRequest("Không có dữ liệu xuất xứ");
            }

            string query = @"INSERT INTO xuatxu (maXuatXu, tenXuatXu) VALUES (@maXuatXu, @tenXuatXu)";

            using (MySqlConnection mycon = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                mycon.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@maXuatXu", xuatXu.MaXuatXu);
                    myCommand.Parameters.AddWithValue("@tenXuatXu", xuatXu.TenXuatXu);

                    using (MySqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            return Ok("Xuất xứ được thêm thành công!");
                        }
                        else
                        {
                            return BadRequest("Không thể thêm xuất xứ.");
                        }
                    }
                }
            }
        }

        [Route("api/xuatxu/{maXuatXu}")]
        [HttpDelete]
        public IActionResult DeleteXuatXu(int maXuatXu)
        {
            if (maXuatXu == 0)
            {
                return BadRequest("Mã xuất xứ không hợp lệ.");
            }

            string query = @"DELETE FROM xuatxu WHERE maXuatXu = @maXuatXu";

            using (MySqlConnection mycon = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                mycon.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@maXuatXu", maXuatXu);

                    int result = myCommand.ExecuteNonQuery();

                    if (result == 1)
                    {
                        return Ok("Xuất xứ đã được xóa thành công!");
                    }
                    else
                    {
                        return NotFound("Xuất xứ không tồn tại.");
                    }
                }
            }
        }

        [Route("api/xuatxu/{maXuatXu}")]
        [HttpPut]
        public IActionResult PutXuatXu(int maXuatXu, [FromBody] XuatXu xuatXu)
        {
            if (xuatXu == null)
            {
                return BadRequest("Không có dữ liệu xuất xứ");
            }

            string query = @"UPDATE xuatxu SET tenXuatXu = @tenXuatXu WHERE maXuatXu = @maXuatXu";

            using (MySqlConnection mycon = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                mycon.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@maXuatXu", maXuatXu);
                    myCommand.Parameters.AddWithValue("@tenXuatXu", xuatXu.TenXuatXu);

                    int result = myCommand.ExecuteNonQuery();

                    if (result == 1)
                    {
                        return Ok("Xuất xứ được cập nhật thành công!");
                    }
                    else
                    {
                        return BadRequest("Không thể cập nhật xuất xứ.");
                    }
                }
            }
        }
    }

}
