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
    public class InvoiceTableController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public InvoiceTableController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetInvoiceTable()
        {
            string getorder = "Select * from dbo.InvoiceTable";
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(getorder, conn))
                {
                    dr = cmd.ExecuteReader();
                    table.Load(dr);
                    dr.Close();
                    conn.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet("{id}")]
        public JsonResult GetReferenceNumber(int referencenumber)
        {
            string getreferencenumber = @"Select * from dbo.InvoiceTable where InvoiceTransactionNumber=@InvoiceTransactionNumber";
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();

                using(SqlCommand cmd = new SqlCommand(getreferencenumber, conn))
                {
                    cmd.Parameters.AddWithValue("@InvoiceTransactionNumber", referencenumber);
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
