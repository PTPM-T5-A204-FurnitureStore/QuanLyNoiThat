using FurnitureStore_API_PM.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace FurnitureStore_API_PM.Controllers
{
	[Route("api/[controller]/[Action]")]
	[ApiController]
	public class ChatLieuController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public ChatLieuController(IConfiguration configuration)
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
		public IActionResult GetChatLieu()
		{
			string query = @"SELECT MaChatLieu, TenChatLieu FROM chatlieu";

			List<ChatLieu> chatLieus = new List<ChatLieu>();

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
							ChatLieu chatlieus = new ChatLieu
							{
								MaChatLieu = myReader.GetInt32("MaChatLieu"),
								TenChatLieu = myReader.GetString("TenChatLieu")
							};
							chatLieus.Add(chatlieus);
						}
					}
				}
			}
			return Ok(chatLieus);
		}
	}
}
