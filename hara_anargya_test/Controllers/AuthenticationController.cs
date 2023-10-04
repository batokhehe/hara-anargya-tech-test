using hara_anargya_test.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace hara_anargya_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("create")]
        public string Create(Authentication authentication)
        {
            var con = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());
            var cmd = new SqlCommand("INSERT INTO Users(UserName, Password, Email, IsActive, LoggedIn) VALUES('" + authentication.UserName + "', '" + authentication.Password + "', '" + authentication.Email + "', 1, 0)", con);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result > 0 ? "Data Inserted" : "Error";
        }

        [HttpPost]
        [Route("update/{id:int}")]
        public string Update(int id, Authentication authentication)
        {
            var con = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());
            var cmd = new SqlCommand("UPDATE Users Set Username = '" + authentication.UserName + "'," +
                " Password = '" + authentication.Password + "', " +
                " Email = '" + authentication.Email + "' " +
                " WHERE Id = '" + id + "'", con);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result > 0 ? "Data Updated" : "Error";
        }

        [HttpPost]
        [Route("update/password/{id:int}")]
        public string UpdatePassword(int id, Authentication authentication)
        {
            var con = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());
            var cmd = new SqlCommand("UPDATE Users Set Password = '" + authentication.Password + "'" +
                " WHERE Id = '" + id + "'", con);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result > 0 ? "Password Updated" : "Error";
        }

        [HttpGet]
        [Route("delete/{id:int}")]
        public string Delete(int id)
        {
            var con = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());
            var cmd = new SqlCommand("DELETE From Users WHERE Id = '" + id + "'", con);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result > 0 ? "Data Deleted" : "Error";
        }

        [HttpPost]
        [Route("login")]
        public string Login(Authentication authentication)
        {
            var con = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());
            var da = new SqlDataAdapter("SELECT * FROM Users WHERE Email = '" + authentication.Email + "' AND Password = '" + authentication.Password + "'", con);
            var dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0) {
                var cmd = new SqlCommand("UPDATE Users Set LoggedIn = '1' WHERE Email = '" + authentication.Email + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close(); 
                return "Valid User";
            }
            else return "Invalid User";
        }

        [HttpPost]
        [Route("logout")]
        public string Logout(Authentication authentication)
        {
            var con = new SqlConnection(_configuration.GetConnectionString("DBCon").ToString());
            var da = new SqlDataAdapter("SELECT * FROM Users WHERE Email = '" + authentication.Email + "' AND Password = '" + authentication.Password + "'", con);
            var dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                var cmd = new SqlCommand("UPDATE Users Set LoggedIn = '0' WHERE Email = '" + authentication.Email + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "Logout User";
            }
            else return "Invalid User";
        }
    }
}
