using FurnitureStore_API_PM.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.Common;
using System;
using System.ComponentModel;

namespace FurnitureStore_API_PM.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public KhachHangController(IConfiguration configuration)
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
                    return Ok("Kết nối đến MySQL thành công !");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi kết nối đến MySQL:" + ex.Message);
                }
            }
        }
        //Get
        [HttpGet]
        public IActionResult GetKhachHang()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT *FROM khachhang";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            List<KhachHang> khachHangs = new List<KhachHang>();
                            while (dataReader.Read())
                            {
                                KhachHang khachHang = new KhachHang
                                {
                                    MaKH = dataReader.GetInt32("MaKH"),
                                    TenKH = dataReader.GetString("TenKH"),
                                    SDT = dataReader.GetString("SDT"),
                                    DiaChi = dataReader.GetString("DiaChi"),
                                    TaiKhoan = dataReader.GetString("TaiKhoan"),
                                    MatKhau = dataReader.GetString("MatKhau")
                                };
                                khachHangs.Add(khachHang);
                            }

                            return Ok(khachHangs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi truy xuất dữ liệu :" + ex.Message);
            }
        }
        //Post
        [HttpPost]
        public IActionResult CreateKhachHang([FromBody] KhachHang khachHang)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO khachhang(TenKH,SDT,DiaChi,TaiKhoan,MatKhau) VALUES(@TenKH,@SDT,@DiaChi,@TaiKhoan,@MatKhau)";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@TenKH", khachHang.TenKH);
                        cmd.Parameters.AddWithValue("@SDT", khachHang.SDT);
                        cmd.Parameters.AddWithValue("@DiaChi", khachHang.TenKH);
                        cmd.Parameters.AddWithValue("@TenKH", khachHang.TenKH);
                        cmd.Parameters.AddWithValue("@TenKH", khachHang.TenKH);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok("Thêm khách hàng thành công");

                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi thêm khách hàng ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi thêm khách hàng: " + ex.Message);
            }

        }
        //Put
        [HttpPut]
        public IActionResult UpdateKhachHang(int id, [FromBody] KhachHang khachHang)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"UPDATE khachhang SET TenKH=@TenKH, SDT=@SDT, DiaChi=@DiaChi,TaiKhoan=@TaiKhoan, MatKhau=@MatKhau WHERE MaKH=@Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@TenKH", khachHang.TenKH);
                        cmd.Parameters.AddWithValue("@SDT", khachHang.SDT);
                        cmd.Parameters.AddWithValue("@DiaChi", khachHang.DiaChi);
                        cmd.Parameters.AddWithValue("@TaiKhoan", khachHang.TaiKhoan);
                        cmd.Parameters.AddWithValue("@MatKhau", khachHang.MatKhau);
                        cmd.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok("Khách hàng đã được cập nhật thành công");
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi cập nhật khách hàng");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi cập nhật khách hàng : " + ex.Message);
            }
        }

        //Delete
        [HttpDelete]
        public IActionResult DeleteKhachHang(int id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM khachhang WHERE MaKH = @Id ";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        int rowAffected = cmd.ExecuteNonQuery();

                        if (rowAffected > 0)
                        {
                            return Ok("Đã thành công xóa khách hàng");
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi không thể xóa khách hàng");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi xóa khách hàng" + ex.Message);
            }
        }
    }

    // các phương thức khác ..........
}



