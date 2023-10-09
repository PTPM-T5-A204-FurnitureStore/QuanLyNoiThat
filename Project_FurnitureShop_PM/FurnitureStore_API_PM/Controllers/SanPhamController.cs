using FurnitureStore_API_PM.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;


namespace FurnitureStore_API_PM.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SanPhamController(IConfiguration configuration)
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

        //Get
        [HttpGet]
        public IActionResult GetSanPham()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT * FROM sanpham";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            List<SanPham> sanPhams = new List<SanPham>();

                            while (dataReader.Read())
                            {
                                SanPham sanPham = new SanPham
                                {
                                    MaSP = dataReader.GetInt32("MaSP"),
                                    GiaBan = dataReader.GetDecimal("GiaBan"),
                                    TenSanPham = dataReader.GetString("TenSanPham"),
                                    MauSac = dataReader.GetString("MauSac"),
                                    MoTa = dataReader.GetString("MoTa"),
                                    SoLuongTon = dataReader.GetInt32("SoLuongTon"),
                                    Hinh1 = !dataReader.IsDBNull(dataReader.GetOrdinal("Hinh1")) ? dataReader.GetString("Hinh1") : "default_path.jpg",
                                    Hinh2 = !dataReader.IsDBNull(dataReader.GetOrdinal("Hinh2")) ? dataReader.GetString("Hinh2") : "default_path.jpg",
                                    Hinh3 = !dataReader.IsDBNull(dataReader.GetOrdinal("Hinh3")) ? dataReader.GetString("Hinh3") : "default_path.jpg",
                                    KichThuoc = dataReader.GetString("KichThuoc"),
                                    MaLoai = dataReader.GetInt32("MaLoai"),
                                    MaXuatXu = dataReader.GetInt32("MaXuatXu"),
                                    MaChatLieu = dataReader.GetInt32("MaChatLieu")
                                };

                                sanPhams.Add(sanPham);
                            }

                            return Ok(sanPhams);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi truy xuất dữ liệu: " + ex.Message);
            }
        }



        //Get
        [HttpGet]
        public IActionResult GetSanPhambyidLoaiHang(string idloai)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT * FROM sanpham WHERE MaLoai=@id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", idloai);
                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            List<SanPham> sanPhams = new List<SanPham>();

                            while (dataReader.Read())
                            {
                                SanPham sanPham = new SanPham
                                {
                                    MaSP = dataReader.GetInt32("MaSP"),
                                    GiaBan = dataReader.GetDecimal("GiaBan"),
                                    TenSanPham = dataReader.GetString("TenSanPham"),
                                    MauSac = dataReader.GetString("MauSac"),
                                    MoTa = dataReader.GetString("MoTa"),
                                    SoLuongTon = dataReader.GetInt32("SoLuongTon"),
                                    Hinh1 = !dataReader.IsDBNull(dataReader.GetOrdinal("Hinh1")) ? dataReader.GetString("Hinh1") : "default_path.jpg",
                                    Hinh2 = !dataReader.IsDBNull(dataReader.GetOrdinal("Hinh2")) ? dataReader.GetString("Hinh2") : "default_path.jpg",
                                    Hinh3 = !dataReader.IsDBNull(dataReader.GetOrdinal("Hinh3")) ? dataReader.GetString("Hinh3") : "default_path.jpg",
                                    KichThuoc = dataReader.GetString("KichThuoc"),
                                    MaLoai = dataReader.GetInt32("MaLoai"),
                                    MaXuatXu = dataReader.GetInt32("MaXuatXu"),
                                    MaChatLieu = dataReader.GetInt32("MaChatLieu")
                                };

                                sanPhams.Add(sanPham);
                            }

                            return Ok(sanPhams);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi truy xuất dữ liệu: " + ex.Message);
            }
        }


        [HttpPost]
        public IActionResult CreateSanPham([FromBody] SanPham sanPham)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO sanpham (GiaBan, TenSanPham, MauSac, MoTa, SoLuongTon, Hinh1, Hinh2, Hinh3, KichThuoc, MaLoai, MaXuatXu, MaChatLieu)
                             VALUES (@GiaBan, @TenSanPham, @MauSac, @MoTa, @SoLuongTon, @Hinh1, @Hinh2, @Hinh3, @KichThuoc, @MaLoai, @MaXuatXu, @MaChatLieu)";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@GiaBan", sanPham.GiaBan);
                        cmd.Parameters.AddWithValue("@TenSanPham", sanPham.TenSanPham);
                        cmd.Parameters.AddWithValue("@MauSac", sanPham.MauSac);
                        cmd.Parameters.AddWithValue("@MoTa", sanPham.MoTa);
                        cmd.Parameters.AddWithValue("@SoLuongTon", sanPham.SoLuongTon);
                        cmd.Parameters.AddWithValue("@Hinh1", sanPham.Hinh1);
                        cmd.Parameters.AddWithValue("@Hinh2", sanPham.Hinh2);
                        cmd.Parameters.AddWithValue("@Hinh3", sanPham.Hinh3);
                        cmd.Parameters.AddWithValue("@KichThuoc", sanPham.KichThuoc);
                        cmd.Parameters.AddWithValue("@MaLoai", sanPham.MaLoai);
                        cmd.Parameters.AddWithValue("@MaXuatXu", sanPham.MaXuatXu);
                        cmd.Parameters.AddWithValue("@MaChatLieu", sanPham.MaChatLieu);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok("Sản phẩm đã được tạo thành công.");
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi tạo sản phẩm.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi thêm sản phẩm: " + ex.Message);
            }
        }

        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateSanPham(int id, [FromBody] SanPham sanPham)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"UPDATE sanpham
                             SET GiaBan = @GiaBan, TenSanPham = @TenSanPham, MauSac = @MauSac, MoTa = @MoTa,
                                 SoLuongTon = @SoLuongTon, Hinh1 = @Hinh1, Hinh2 = @Hinh2, Hinh3 = @Hinh3,
                                 KichThuoc = @KichThuoc, MaLoai = @MaLoai, MaXuatXu = @MaXuatXu, MaChatLieu = @MaChatLieu
                             WHERE MaSP = @Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@GiaBan", sanPham.GiaBan);
                        cmd.Parameters.AddWithValue("@TenSanPham", sanPham.TenSanPham);
                        cmd.Parameters.AddWithValue("@MauSac", sanPham.MauSac);
                        cmd.Parameters.AddWithValue("@MoTa", sanPham.MoTa);
                        cmd.Parameters.AddWithValue("@SoLuongTon", sanPham.SoLuongTon);
                        cmd.Parameters.AddWithValue("@Hinh1", sanPham.Hinh1);
                        cmd.Parameters.AddWithValue("@Hinh2", sanPham.Hinh2);
                        cmd.Parameters.AddWithValue("@Hinh3", sanPham.Hinh3);
                        cmd.Parameters.AddWithValue("@KichThuoc", sanPham.KichThuoc);
                        cmd.Parameters.AddWithValue("@MaLoai", sanPham.MaLoai);
                        cmd.Parameters.AddWithValue("@MaXuatXu", sanPham.MaXuatXu);
                        cmd.Parameters.AddWithValue("@MaChatLieu", sanPham.MaChatLieu);
                        cmd.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok("Sản phẩm đã được cập nhật thành công.");
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi cập nhật sản phẩm.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi cập nhật sản phẩm: " + ex.Message);
            }
        }

        //Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteSanPham(int id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM sanpham WHERE MaSP = @Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok("Sản phẩm đã được xóa thành công.");
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi xóa sản phẩm.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi xóa sản phẩm: " + ex.Message);
            }
        }
		//Filter 
		[HttpGet("Filter")]
		public IActionResult GetSanPhamByFilters(string idloai, decimal? minPrice, decimal? maxPrice, int? maChatLieu, int? maXuatXu, string? sortOrder)
		{
			try
			{
				string connectionString = _configuration.GetConnectionString("DefaultConnection");
				using (MySqlConnection connection = new MySqlConnection(connectionString))
				{
					connection.Open();

					string query = @"SELECT * FROM sanpham WHERE MaLoai = @idloai";

					if (minPrice.HasValue)
					{
						query += " AND GiaBan >= @minPrice";
					}

					if (maxPrice.HasValue)
					{
						query += " AND GiaBan <= @maxPrice";
					}

					if (maChatLieu.HasValue)
					{
						query += " AND MaChatLieu = @maChatLieu";
					}

					if (maXuatXu.HasValue)
					{
						query += " AND MaXuatXu = @maXuatXu";
					}
					if (!string.IsNullOrEmpty(sortOrder))
					{
						if (sortOrder == "asc")
						{
							query += " ORDER BY GiaBan ASC";
						}
						else if (sortOrder == "desc")
						{
							query += " ORDER BY GiaBan DESC";
						}
					}

					using (MySqlCommand cmd = new MySqlCommand(query, connection))
					{
						cmd.Parameters.AddWithValue("@idloai", idloai);
						cmd.Parameters.AddWithValue("@minPrice", minPrice ?? (object)DBNull.Value);
						cmd.Parameters.AddWithValue("@maxPrice", maxPrice ?? (object)DBNull.Value);
						cmd.Parameters.AddWithValue("@maChatLieu", maChatLieu ?? (object)DBNull.Value);
						cmd.Parameters.AddWithValue("@maXuatXu", maXuatXu ?? (object)DBNull.Value);

						using (MySqlDataReader dataReader = cmd.ExecuteReader())
						{
							List<SanPham> sanPhams = new List<SanPham>();

							while (dataReader.Read())
							{
								SanPham sanPham = new SanPham
								{
									MaSP = dataReader.GetInt32(dataReader.GetOrdinal("MaSP")),
									GiaBan = dataReader.GetDecimal(dataReader.GetOrdinal("GiaBan")),
									TenSanPham = dataReader.GetString(dataReader.GetOrdinal("TenSanPham")),
									MauSac = dataReader.GetString(dataReader.GetOrdinal("MauSac")),
									MaLoai = dataReader.GetInt32(dataReader.GetOrdinal("MaLoai")),
									MaXuatXu = dataReader.GetInt32(dataReader.GetOrdinal("MaXuatXu")),
									MaChatLieu = dataReader.GetInt32(dataReader.GetOrdinal("MaChatLieu")),
								};

								sanPhams.Add(sanPham);
							}

							return Ok(sanPhams);
						}
					}
				}
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi truy xuất dữ liệu: " + ex.Message);
			}
		}
		//thêm các phương thức khác nếu có...
	}
}