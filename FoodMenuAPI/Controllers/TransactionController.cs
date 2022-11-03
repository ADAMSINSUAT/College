using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FoodMenuAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TransactionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select Transactionno from dbo.TransactionDefaultID";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    dr = cmd.ExecuteReader();
                    table.Load(dr);
                    dr.Close();
                    conn.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}