using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Drawing.Imaging;
using FoodMenuAPITest.Models;

namespace FoodMenuAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodMenuController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public FoodMenuController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.FoodMenu where FoodAvailability=@FoodAvailability";
            string availability = "Available";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FoodAvailability", availability);
                    dr = cmd.ExecuteReader();
                    table.Load(dr);
                    dr.Close();
                    conn.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet("{id}")]
        public JsonResult GetId(string id)
        {
            string query = @"select * from dbo.FoodMenu where FoodID=@FoodID";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FoodID", id);
                    dr = cmd.ExecuteReader();
                    table.Load(dr);
                    dr.Close();
                    conn.Close();
                }
            }
            return new JsonResult(table);
        }

        //[HttpPost]
        //public JsonResult Post(FoodMenu fm)
        //{
        //    string query = @"Insert into dbo.FoodMenu(FoodID, FoodName, FoodPrice, FoodCategory, FoodAvailability, FoodPic)VALUES(@FoodID, @FoodName, @FoodPrice, @FoodCategory, @FoodAvailability, @FoodPic)";

        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("Database");
        //    SqlDataReader dr;
        //    using (SqlConnection conn = new SqlConnection(sqlDataSource))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@FoodID", fm.FoodId);
        //            cmd.Parameters.AddWithValue("@FoodName", fm.FoodName);
        //            cmd.Parameters.AddWithValue("@FoodPrice", fm.FoodPrice);
        //            cmd.Parameters.AddWithValue("@FoodCategory", fm.FoodCategory);
        //            cmd.Parameters.AddWithValue("@FoodAvailability", fm.FoodAvailability);
        //            cmd.Parameters.AddWithValue("@FoodPic", fm.FoodPic);
        //            dr = cmd.ExecuteReader();
        //            table.Load(dr);
        //            dr.Close();
        //            conn.Close();
        //        }
        //    }
        //    return new JsonResult("Save Successfully");
        //}

        //[HttpPut]
        //public JsonResult Put(FoodMenu fm)
        //{
        //    string query = @"Update FoodMenu set FoodID = @FoodID, FoodName = @FoodName, FoodPrice=@FoodPrice, FoodCategory=@FoodCategory, FoodAvailability=@FoodAvailability, FoodPic=@FoodPic where FoodID =@FoodID";

        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("Database");
        //    SqlDataReader dr;
        //    using (SqlConnection conn = new SqlConnection(sqlDataSource))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@FoodID", fm.FoodId);
        //            cmd.Parameters.AddWithValue("@FoodName", fm.FoodName);
        //            cmd.Parameters.AddWithValue("@FoodPrice", fm.FoodPrice);
        //            cmd.Parameters.AddWithValue("@FoodCategory", fm.FoodCategory);
        //            cmd.Parameters.AddWithValue("@FoodAvailability", fm.FoodAvailability);
        //            cmd.Parameters.AddWithValue("@FoodPic", fm.FoodPic);
        //            dr = cmd.ExecuteReader();
        //            table.Load(dr);
        //            dr.Close();
        //            conn.Close();
        //        }
        //    }
        //    return new JsonResult("Updated Successfully");
        //}

        //[HttpDelete]
        //public JsonResult Delete(string id)
        //{
        //    string query = @"Delete from dbo.FoodMenu where FoodID=@FoodID";

        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("Database");
        //    SqlDataReader dr;
        //    using (SqlConnection conn = new SqlConnection(sqlDataSource))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@FoodID", id);
        //            dr = cmd.ExecuteReader();
        //            table.Load(dr);
        //            dr.Close();
        //            conn.Close();
        //        }
        //    }
        //    return new JsonResult("Deleted Successfully");
        //}
    }
}