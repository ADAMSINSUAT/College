using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FoodMenuAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminVerifyController : ControllerBase
    {
        private readonly IConfiguration _configuration;


        public AdminVerifyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{username},{password}")]
        public JsonResult VerifyAdmin(string username, string password)
        {
            string Verify = "Select * from AdminTable where AdminUsername=@AdminUsername and AdminPassword=@AdminPassword";
            string sqlDataSource = _configuration.GetConnectionString("Database");
            string Authorization = "";
            SqlDataReader dr;
            SqlCommand cmd;

            using(SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using(cmd = new SqlCommand(Verify, conn))
                {
                    cmd.Parameters.AddWithValue("@AdminUsername", username);
                    cmd.Parameters.AddWithValue("@AdminPassword", password);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        Authorization = "Access Granted";
                    }
                    else
                    {
                        Authorization = "Access Denied";
                    }
                    dr.Close();
                    conn.Close();
                    return new JsonResult(Authorization);
                }
            }
        }
    }

}
