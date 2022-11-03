using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FoodMenuAPITest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FoodMenuAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableNumberController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TableNumberController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetTableNames()
        {
            string getid = "Select * from dbo.AndroidTableNumber";
            string sqlDataSource = _configuration.GetConnectionString("Database");
            string IfExist = "";
            SqlDataReader dr;
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(getid, conn))
                {
                    dr = cmd.ExecuteReader();
                    table.Load(dr);
                    dr.Close();
                    conn.Close();
                }
            }
            return new JsonResult(IfExist);
        }

        [HttpGet("{id}")]
        public JsonResult GetAndroidID(string id)
        {
            string getid = @"Select * from dbo.AndroidTableNumber where TableDeviceName = @TableDevicename";
            string sqlDataSource = _configuration.GetConnectionString("Database");
            //string IfExist = "";
            string TableNo = "";
            string Result = "";
            SqlDataReader dr;
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();

                using(SqlCommand cmd = new SqlCommand(getid, conn))
                {
                    cmd.Parameters.AddWithValue("@TableDeviceName", id);
                    dr = cmd.ExecuteReader();
                    if(dr.Read())
                    {
                        TableNo = dr["TableNo"].ToString();
                        Result = TableNo;
                        //IfExist = "Device is registered";
                    }
                    else
                    {
                        //IfExist = "Device is not registered";
                        Result = "Device is not registered";
                    }
                    dr.Close();
                    conn.Close();

                    //return new JsonResult(IfExist);
                    return new JsonResult(Result);
                }
            }
        }

        [HttpPost]
        public JsonResult SaveNumberController(AndroidTableNumber androidtableNumber)
        {
            string savetablenumber = @"Insert into dbo.AndroidTableNumber (TableNo, TableDeviceName)VALUES(@TableNo, @TableDeviceName)";
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            DataTable table = new DataTable(); ;
            using(SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();

                using(SqlCommand cmd = new SqlCommand(savetablenumber, conn))
                {
                    cmd.Parameters.AddWithValue("@TableNo", androidtableNumber.TableNo);
                    cmd.Parameters.AddWithValue("@TableDeviceName", androidtableNumber.TableDeviceName);
                    dr = cmd.ExecuteReader();
                    table.Load(dr);
                    dr.Close();
                    conn.Close();
                }
            }
            return new JsonResult("Table Number is Saved");
        }

        [HttpPut]
        public JsonResult UpdateTableNumber(AndroidTableNumber androidtableNumber)
        {
            string updatetablenumber = @"Update dbo.AndroidTableNumber set TableNo=@TableNo where TableDeviceName = @TableDeviceName";
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            DataTable table = new DataTable(); ;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(updatetablenumber, conn))
                {
                    cmd.Parameters.AddWithValue("@TableDeviceName", androidtableNumber.TableDeviceName);
                    cmd.Parameters.AddWithValue("@TableNo", androidtableNumber.TableNo);
                    dr = cmd.ExecuteReader();
                    table.Load(dr);
                    dr.Close();
                    conn.Close();
                }
            }
            return new JsonResult("Table Number is Updated");
        }
    }
}
