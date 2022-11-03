using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using FoodMenuAPITest.Models;

namespace FoodMenuAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCustomerOrderController : ControllerBase
    {
        int ordernumber;

        private readonly IConfiguration _configuration;

        public GetCustomerOrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetOrderNumber()
        {
            string query = @"select * from dbo.QueueTable where OrderNumber=(SELECT MIN(OrderNumber) from dbo.QueueTable)";
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;

            using(SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    dr = cmd.ExecuteReader();
                    if(dr.Read())
                    {
                        ordernumber = Convert.ToInt32(dr["TableNumber"].ToString());
                    }
                    else
                    {
                        ordernumber = 0;
                    }
                    dr.Close();
                    conn.Close();
                }
                return new JsonResult(ordernumber);
            }
        }

        [HttpGet("{id}")]
        public JsonResult GetId(int id)
        {
            string query = @"select * from dbo.QueueTable where TableNumber=@TableNumber";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TableNumber", id);
                    dr = cmd.ExecuteReader();
                    table.Load(dr);
                    dr.Close();
                    conn.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPut]
        public JsonResult Put(QueueTable qt)
        {
            string query = @"Update QueueTable set Item = @Item, Quantity=@Quantity, Price=@Price, PriceAmount=@PriceAmount  where TableNumber = @TableNumber AND Item=@Item";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TableNumber", qt.TableNumber);
                    cmd.Parameters.AddWithValue("@Item", qt.Item);
                    cmd.Parameters.AddWithValue("@Quantity", qt.Quantity);
                    cmd.Parameters.AddWithValue("@Price", qt.Price);
                    cmd.Parameters.AddWithValue("@PriceAmount", qt.PriceAmount);
                    dr = cmd.ExecuteReader();
                    table.Load(dr);
                    dr.Close();
                    conn.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{tablenumber},{item}")]
        public JsonResult Delete(int tablenumber, string item)
        {
            string query = @"Delete from dbo.QueueTable where TableNumber=@TableNumber AND Item=@Item";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TableNumber", tablenumber);
                    cmd.Parameters.AddWithValue("@Item", item);
                    dr = cmd.ExecuteReader();
                    table.Load(dr);
                    dr.Close();
                    conn.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
    }
}