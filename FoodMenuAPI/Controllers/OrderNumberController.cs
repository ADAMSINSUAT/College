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
    public class OrderNumberController : ControllerBase
    {
        int result;
        int first;
        int current;

        private readonly IConfiguration _configuration;

        public OrderNumberController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{tablenumber}")]
        public JsonResult GetOrderNumber(int tablenumber)
        {
            string query = @"Select * from dbo.QueueTable where OrderNumber=(SELECT MIN(OrderNumber) from dbo.QueueTable)";
            string getcurrentordernumber = @"Select * from dbo.QueueTable where TableNumber=@TableNumber";
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            using(SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();

                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    dr = cmd.ExecuteReader();
                    dr.Read();

                    first = Convert.ToInt32(dr["OrderNumber"].ToString());
                    dr.Close();
                }
                using (SqlCommand cmd = new SqlCommand(getcurrentordernumber, conn))
                {
                    cmd.Parameters.AddWithValue("@TableNumber", tablenumber);
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        current = Convert.ToInt32(dr["OrderNumber"].ToString());
                    }
                    dr.Close();
                    //}
                    //int[] array = { first, current };

                    //for(int i = 0; i< array.Length; i++)
                    //{
                    //    if(result<array[i])
                    //    {
                    //        result = array[i];
                    //    }
                    //}
                }
                result = current - first;
                return new JsonResult(result);
            }
        }
    }
}
