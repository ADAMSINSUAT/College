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
    public class TransactionNumberController : ControllerBase
    {
        int transactionnumber;

        private readonly IConfiguration _configuration;

        public TransactionNumberController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetTransactionNumber()
        {
            string selectdefaultreferencenumber = @"Select * from dbo.OrderReferenceNumber";
            string selectinvoicereferencenumber = @"Select InvoiceTransactionNumber from dbo.InvoiceTable where InvoiceTransactionNumber= (SELECT MAX (InvoiceTransactionNumber) from dbo.InvoiceTable)";
            string selectqueuereferencenumber = @"Select TransactionNumber from dbo.QueueTable where TransactionNumber= (SELECT MAX(TransactionNumber) from dbo.QueueTable)";

            int defaulttransacationnumber = 0, queuetabletransactionnumber = 0, invoicetransactionnumber = 0, currenttransactionnumber = 0;

            String sqlDataSource = _configuration.GetConnectionString("Database");
            SqlCommand cmd;
            SqlDataReader dr;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (cmd = new SqlCommand(selectdefaultreferencenumber, conn))
                {
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    defaulttransacationnumber = Convert.ToInt32(dr["OrderReferenceNumber"].ToString());
                    dr.Close();
                }
                using (cmd = new SqlCommand(selectinvoicereferencenumber, conn))
                {
                    dr = cmd.ExecuteReader();
                    if(dr.Read())
                    {
                        invoicetransactionnumber = Convert.ToInt32(dr["InvoiceTransactionNumber"].ToString());
                        invoicetransactionnumber++;
                    }
                    dr.Close();
                }
                using (cmd = new SqlCommand(selectqueuereferencenumber, conn))
                {
                    dr = cmd.ExecuteReader();
                    if(dr.Read())
                    {
                        queuetabletransactionnumber = Convert.ToInt32(dr["TransactionNumber"].ToString());
                        queuetabletransactionnumber++;
                    }
                    dr.Close();
                }

                int[] array = { defaulttransacationnumber, queuetabletransactionnumber, invoicetransactionnumber };

                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] > currenttransactionnumber)
                    {
                        currenttransactionnumber = array[i];
                    }
                }
            }
            return new JsonResult(currenttransactionnumber);
        }

        [HttpGet("{tablenumber}")]
        public JsonResult GetCurrentTransactionNumber(int tablenumber)
        {
            string query = @"Select * from dbo.QueueTable where TableNumber=@TableNumber";
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TableNumber", tablenumber);
                    dr = cmd.ExecuteReader();
                    if(dr.Read())
                    {
                        transactionnumber = Convert.ToInt32(dr["TransactionNumber"].ToString());
                    }
                    dr.Close();
                    conn.Close();
                }
                return new JsonResult(transactionnumber);
            }
        }
    }
}
