using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FoodMenuAPITest.Controllers;
using FoodMenuAPITest.Hubs;
using FoodMenuAPITest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace FoodMenuAPITest.Repository
{
    //public class TemporaryInvoiceTableRepository : ITemporaryInvoiceTableRepository
    public class TemporaryInvoiceTableRepository : ITemporaryInvoiceTableRepository
    {
        private readonly IHubContext<ChatHub> _context;
        string connectionstring = "";

        DataTable table = new DataTable();
        int orderid;
        public TemporaryInvoiceTableRepository(IConfiguration configuration,
                                                IHubContext<ChatHub> context)
        {
            connectionstring = configuration.GetConnectionString("Database");
            _context = context;
        }
        //public List<TemporaryInvoiceTable> GetAllOrders(int id)
        public JsonResult GetAllOrders(int id)
        {
            //var orders = new List<TemporaryInvoiceTable>();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlDependency.Start(connectionstring);

                string commandText = "select TempDeviceId, TempOrderNumber, " +
                    "TempInvoiceItem, TempInvoiceQuantity, TempOrderPrice, " +
                    "TempOrderPriceAmount, TempInvoiceSubTotal, " +
                    "TempInvoiceTime, TempInvoiceMonth, TempInvoiceDay, TempInvoiceYear, TempOrderCommand FROM dbo.TemporaryInvoiceTable WHERE TempDeviceID=@TempDeviceID";

                SqlCommand cmd = new SqlCommand(commandText, conn);

                cmd.Parameters.AddWithValue("@TempDeviceID", id);

                SqlDependency dependency = new SqlDependency(cmd);

                dependency.OnChange += new OnChangeEventHandler(dbchangenotification);

                var reader = cmd.ExecuteReader();
                //var read = reader.Read();

                table.Load(reader);
                //while (read)
                //{
                //var order = new TemporaryInvoiceTable
                //{
                //    TempDeviceId = Convert.ToInt32(reader["TempDeviceId"]),
                //    TempOrderNumber = Convert.ToInt32(reader["TempOrderNumber"]),
                //    TempInvoiceItem = Convert.ToString(reader["TempInvoiceItem"]),
                //    TempInvoiceQuantity = Convert.ToInt32(reader["TempInvoiceQuantity"]),
                //    TempOrderPrice = Convert.ToDecimal(reader["TempOrderPrice"]),
                //    TempOrderPriceAmount = Convert.ToDecimal(reader["TempOrderPriceAmount"]),
                //    TempInvoiceSubTotal = Convert.ToDecimal(reader["TempInvoiceSubTotal"]),
                //    TempInvoiceTime = Convert.ToString(reader["TempInvoiceTime"]),
                //    TempInvoiceMonth = Convert.ToString(reader["TempInvoiceMonth"]),
                //    TempInvoiceDay = Convert.ToString(reader["TempInvoiceDay"]),
                //    TempInvoiceYear = Convert.ToString(reader["TempInvoiceYear"]),
                //};
                // orders.Add(order);
                //}
            }
            orderid = id;
            return new JsonResult(table);
        }

        //public JsonResult GetAllOrders(int id)
        //{
        //    //var orders = new List<TemporaryInvoiceTable>();

        //    DataTable table = new DataTable();

        //    using (SqlConnection conn = new SqlConnection(connectionstring))
        //    {
        //        conn.Open();
        //        SqlDependency.Start(connectionstring);

        //        string commandText = "select TempDeviceId, TempOrderNumber, " +
        //            "TempInvoiceItem, TempInvoiceQuantity, TempOrderPrice, " +
        //            "TempOrderPriceAmount, TempInvoiceSubTotal, " +
        //            "TempInvoiceTime, TempInvoiceMonth, TempInvoiceDay, TempInvoiceYear FROM dbo.TemporaryInvoiceTable WHERE TempDeviceID=@TempDeviceID";

        //        SqlCommand cmd = new SqlCommand(commandText, conn);

        //        cmd.Parameters.AddWithValue("@TempDeviceID", id);

        //        SqlDependency dependency = new SqlDependency(cmd);

        //        dependency.OnChange += new OnChangeEventHandler(dbchangenotification);

        //        var reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            //var order = new TemporaryInvoiceTable
        //            //{
        //            //    TempDeviceId = Convert.ToInt32(reader["TempDeviceId"]),
        //            //    TempOrderNumber = Convert.ToInt32(reader["TempOrderNumber"]),
        //            //    TempInvoiceItem = Convert.ToString(reader["TempInvoiceItem"]),
        //            //    TempInvoiceQuantity = Convert.ToInt32(reader["TempInvoiceQuantity"]),
        //            //    TempOrderPrice = Convert.ToDecimal(reader["TempOrderPrice"]),
        //            //    TempOrderPriceAmount = Convert.ToDecimal(reader["TempOrderPriceAmount"]),
        //            //    TempInvoiceSubTotal = Convert.ToDecimal(reader["TempInvoiceSubTotal"]),
        //            //    TempInvoiceTime = Convert.ToString(reader["TempInvoiceTime"]),
        //            //    TempInvoiceMonth = Convert.ToString(reader["TempInvoiceMonth"]),
        //            //    TempInvoiceDay = Convert.ToString(reader["TempInvoiceDay"]),
        //            //    TempInvoiceYear = Convert.ToString(reader["TempInvoiceYear"]),
        //            //};
        //            table.Load(reader);
        //        }
        //    }
        //    return new JsonResult(table);
        //}

        private void dbchangenotification(object sender, SqlNotificationEventArgs e)
        {
            //var notification = e.ToString();
            //_context.Clients.All.SendAsync("refreshOrders");
            SqlDependency dependency = sender as SqlDependency;
            dependency.OnChange -= dbchangenotification;
            string getorders = Convert.ToString(orderid);
            if (e.Info == SqlNotificationInfo.Insert)
            {
                //_context.Clients.User(userId:getorders).SendAsync("getAllOrders", GetAllOrders(Convert.ToInt32(getorders)));
                //_context.Clients.User(user).SendAsync
                ChatHub.SendMessage(getorders, GetAllOrders(Convert.ToInt32(getorders)));
            }
            else if (e.Info == SqlNotificationInfo.Delete)
            {
                ChatHub.SendMessage(getorders, GetAllOrders(Convert.ToInt32(getorders)));
                // _context.Clients.User(userId: getorders).SendAsync("getAllOrders", GetAllOrders(Convert.ToInt32(getorders)));
            }
            else if (e.Info == SqlNotificationInfo.Update)
            {
                ChatHub.SendMessage(getorders, GetAllOrders(Convert.ToInt32(getorders)));
                // _context.Clients.User(userId: getorders).SendAsync("getAllOrders", GetAllOrders(Convert.ToInt32(getorders)));
            }
        }
    }
}
