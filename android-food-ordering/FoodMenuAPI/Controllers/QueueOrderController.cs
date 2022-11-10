using System;
using System.Collections.Generic;
using System.Data;
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
    public class QueueOrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        DataTable table = new DataTable();
        string connectionstring = "";
        int orderid;
        SqlDependency dependency;
        public QueueOrderController(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionstring = configuration.GetConnectionString("Database");
        }
        // GET: api/QueueOrder
        [HttpGet("{id}")]
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

                dependency = new SqlDependency(cmd);

                dependency.OnChange += new OnChangeEventHandler(dbchangenotification);

                var reader = cmd.ExecuteReader();

                table.Load(reader);
            }
            orderid = id;
            return new JsonResult(table);
        }

        private void dbchangenotification(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency dependency = sender as SqlDependency;
            dependency.OnChange -= dbchangenotification;
            if (e.Info == SqlNotificationInfo.Insert)
            {

            }
            else if (e.Info == SqlNotificationInfo.Delete)
            {

            }
            else if (e.Info == SqlNotificationInfo.Update)
            {
                var getorders = orderid;
                GetAllOrders(getorders);
            }
        }

        // GET: api/QueueOrder/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/QueueOrder
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/QueueOrder/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
