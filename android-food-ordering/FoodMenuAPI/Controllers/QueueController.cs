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
using System.Collections;
using FoodMenuAPITest.Models;
//using Microsoft.Data.SqlClient;

namespace FoodMenuAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public QueueController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            //int month = Convert.ToInt32(DateTime.Now.Month.ToString());
            //int year = Convert.ToInt32(DateTime.Now.Year.ToString());
            //string newmonth = DateTime.Now.ToString("MM");
            //string getdefaulttransactno = @"select Transactionno from dbo.TransactionDefaultID";
            string gettempinvoiceid = @"select OrderNumber from dbo.QueueTable where OrderNumber =(SELECT MAX(OrderNumber) from dbo.QueueTable)";
            string getdefaultinvoiceid = @"Select Transactionno from dbo.TransactionDefaultID";
            int defaultid = 0, tempinvoiceid = 0, currentid=0;
            //var result="";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            SqlCommand cmd;

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (cmd = new SqlCommand(getdefaultinvoiceid, conn))
                {
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    defaultid = Convert.ToInt32(dr["Transactionno"].ToString());
                    dr.Close();
                }
                using (cmd = new SqlCommand(gettempinvoiceid, conn))
                {
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        tempinvoiceid = Convert.ToInt32(dr["OrderNumber"].ToString());
                        tempinvoiceid++;
                        //result = Convert.ToString(tempinvoiceid);
                        dr.Close();
                    }
                    else
                    {
                        dr.Close();
                        //result = "None";
                    }
                }

                int[] array = { defaultid, tempinvoiceid};

                for(int i = 0; i<array.Length; i++)
                {
                    if(array[i]>currentid)
                    {
                        currentid = array[i];
                    }
                }
                conn.Close();
                dr.Close();

                return new JsonResult(currentid);
            }
        }
        [HttpGet("{id}")]
        public JsonResult GetId(int id) //To get the order for the customer that ordered
        {
            string query = @"select * from dbo.QueueTable where TableNumber=@TableNumber";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            string result = "";
            int ordernumber = 0;
            //int min = 0, max = 0;

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TableNumber", id);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        ordernumber = Convert.ToInt32(dr["OrderNumber"].ToString());
                        result = Convert.ToString(ordernumber);
                    }
                    else
                    {
                        result = "";
                    }
                    //table.Load(dr);
                    dr.Close();
                    conn.Close();
                }
            }
            return new JsonResult(result);
        }

        [HttpGet("{id},{item}")]
        public JsonResult GetIdwithItem(int id, string item) //To get the order for the customer that ordered
        {
            string query = @"select * from dbo.QueueTable where TableNumber=@TableNumber AND Item=@Item";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            string result = "";
            //int min = 0, max = 0;

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TableNumber", id);
                    cmd.Parameters.AddWithValue("@Item", item);
                    dr = cmd.ExecuteReader();
                    if(dr.Read())
                    {
                        result = "Item exist";
                    }
                    else
                    {
                        result = "Item does not exist";
                    }
                    //table.Load(dr);
                    dr.Close();
                    conn.Close();
                }
            }
            return new JsonResult(result);
        }

        //[HttpPost]
        //public JsonResult Post(TemporaryInvoiceTable tit)
        //{
        //    string query = @"Insert into dbo.TemporaryInvoiceTable(TempInvoiceID, TempInvoiceItem, TempInvoiceQuantity, TempInvoiceSubTotal, TempInvoiceTime, TempInvoiceMonth, TempInvoiceDay, TempInvoiceYear)VALUES(@TempInvoiceID, @TempInvoiceItem, @TempInvoiceQuantity, @TempInvoiceSubTotal, @TempInvoiceTime, @TempInvoiceMonth, @TempInvoiceDay, @TempInvoiceYear)";

        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("Database");
        //    SqlDataReader dr;
        //    using (SqlConnection conn = new SqlConnection(sqlDataSource))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@TempInvoiceItem", tit.TempInvoiceItem);
        //            cmd.Parameters.AddWithValue("@TempInvoiceQuantity", tit.TempInvoiceQuantity);
        //            cmd.Parameters.AddWithValue("@TempInvoiceSubTotal", tit.TempInvoiceSubTotal);
        //            cmd.Parameters.AddWithValue("@TempInvoiceTime", tit.TempInvoiceTime);
        //            cmd.Parameters.AddWithValue("@TempInvoiceMonth", tit.TempInvoiceMonth);
        //            cmd.Parameters.AddWithValue("@TempInvoiceDay", tit.TempInvoiceDay);
        //            cmd.Parameters.AddWithValue("@TempInvoiceYear", tit.TempInvoiceYear);
        //            dr = cmd.ExecuteReader();
        //            table.Load(dr);
        //            dr.Close();
        //            conn.Close();
        //        }
        //    }
        //    return new JsonResult("Save Successfully");
        //}
        [HttpPost]
        public JsonResult Post(QueueTable qt)
        {
            string save = @"Insert into dbo.QueueTable (TableNumber, OrderNumber, Item, Quantity, Price, PriceAmount, OrderType, TransactionNumber)VALUES(@TableNumber, @OrderNumber, @Item, @Quantity, @Price, @PriceAmount, @OrderType, @TransactionNumber)";
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader dr;
            SqlCommand cmd;

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (cmd = new SqlCommand(save, conn))
                {
                    cmd.Parameters.AddWithValue("@TableNumber", qt.TableNumber);
                    cmd.Parameters.AddWithValue("@OrderNumber", qt.OrderNumber);
                    cmd.Parameters.AddWithValue("@Item", qt.Item);
                    cmd.Parameters.AddWithValue("@Quantity", qt.Quantity);
                    cmd.Parameters.AddWithValue("@Price", qt.Price);
                    cmd.Parameters.AddWithValue("@PriceAmount", qt.PriceAmount);
                    cmd.Parameters.AddWithValue("@OrderType", qt.OrderType);
                    cmd.Parameters.AddWithValue("@TransactionNumber", qt.TransactionNumber);
                    dr = cmd.ExecuteReader();
                    table.Load(dr);
                    dr.Close();
                    conn.Close();
                }
            }
            return new JsonResult("Save Successful");
        }

        //[HttpPost("{id},{item}")]
        //public JsonResult SaveIntoExistingRecord(int id, string item , QueueTable qt)
        //{
        //    string detectcurrentorder = "Select * from dbo.QueueTable where TableNumber=@TableNumber";
        //    string detectifitemexist = "Select * from dbo.QueueTable where Item=@Item AND TableNumber = @TableNumber";
        //    string save = @"Insert into dbo.QueueTable (TableNumber, Item, Quantity, Price, PriceAmount, OrderType, TransactionNumber)VALUES(@TableNumber, @Item, @Quantity, @Price, @PriceAmount, @OrderType, @TransactionNumber)";
        //    string update = "@Update QueueTable set Item = @Item, Quantity = @Quantity, where DeviceID = @DeviceID";
        //    int transactionnumber=0;
        //    string result = "";
        //    DataTable table = new DataTable();
        //    String sqlDataSource = _configuration.GetConnectionString("Database");
        //    SqlCommand cmd;
        //    SqlDataReader dr;
        //    using(SqlConnection conn = new SqlConnection(sqlDataSource))
        //    {
        //        conn.Open();
        //        using (cmd = new SqlCommand(detectcurrentorder, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@TableNumber", id);
        //            dr = cmd.ExecuteReader();

        //            if (dr.Read())
        //            {
        //                transactionnumber = Convert.ToInt32(dr["TransactionNumber"].ToString());
        //            }

        //            dr.Close();

        //            using (cmd = new SqlCommand(detectifitemexist, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@Item", item);
        //                cmd.Parameters.AddWithValue("@TableNumber", id);

        //                dr = cmd.ExecuteReader();
        //                if (dr.Read())
        //                {
        //                    result = "Item already exist";
        //                    dr.Close();
        //                }
        //                else
        //                {
        //                    result = "Item does not exist";
        //                    dr.Close();
        //                }
        //            }
        //            if(result == "Item does not exist")
        //            {
        //                using (cmd = new SqlCommand(save, conn))
        //                {
        //                    cmd.Parameters.AddWithValue("@TableNumber", qt.TableNumber);
        //                    cmd.Parameters.AddWithValue("@Item", qt.Item);
        //                    cmd.Parameters.AddWithValue("@Quantity", qt.Quantity);
        //                    cmd.Parameters.AddWithValue("@Price", qt.Price);
        //                    cmd.Parameters.AddWithValue("@PriceAmount", qt.PriceAmount);
        //                    cmd.Parameters.AddWithValue("@OrderType", qt.OrderType);
        //                    cmd.Parameters.AddWithValue("@TransactionNumber", transactionnumber);
        //                    dr = cmd.ExecuteReader();
        //                    table.Load(dr);
        //                    dr.Close();
        //                    conn.Close();
        //                    result = "Save successful";
        //                }
        //            }
        //        }
        //    }
        //    return new JsonResult(result);
        //}

        //[HttpDelete]
        //public JsonResult Delete(int id)
        //{
        //    string query = @"Delete from dbo.TemporaryInvoiceTable where TempInvoiceID=@TempInvoiceID";

        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("Database");
        //    SqlDataReader dr;
        //    using (SqlConnection conn = new SqlConnection(sqlDataSource))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@TempInvoiceID", id);
        //            dr = cmd.ExecuteReader();
        //            table.Load(dr);
        //            dr.Close();
        //            conn.Close();
        //        }
        //    }
        //    return new JsonResult("Deleted Successfully");
    }
}