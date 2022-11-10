using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Android_Menu_Selection_System__Admin_
{
    public partial class DailySalesForm : Form
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlConnection conn;
        SqlDataReader dr;
        bool printwithdate;
        string printdailysales;
        public DailySalesForm()
        {
            InitializeComponent();
        }

        private void DailySalesForm_Load(object sender, EventArgs e)
        {
            LoadDailySales();
            TransactionHistory();
            BestSeller();
        }

        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }

        private void LoadDailySales()
        {
            string GetDailySales = "Select* from DailySalesTable";
            Database();
            da = new SqlDataAdapter(GetDailySales, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dGVDailySales.Rows.Clear();
            foreach(DataRow item in dt.Rows)
            {
                int n = dGVDailySales.Rows.Add();

                dGVDailySales.Rows[n].Cells[0].Value = item["DSDate"].ToString();
                dGVDailySales.Rows[n].Cells[1].Value = item["DSTime"].ToString();
                dGVDailySales.Rows[n].Cells[2].Value = item["DSTax"].ToString();
                dGVDailySales.Rows[n].Cells[3].Value = item["DSVatableSales"].ToString();
                dGVDailySales.Rows[n].Cells[4].Value = item["DSPayment"].ToString();
                dGVDailySales.Rows[n].Cells[5].Value = item["DSTotal"].ToString();
                dGVDailySales.Rows[n].Cells[6].Value = item["DSChange"].ToString();
                dGVDailySales.Rows[n].Cells[7].Value = item["DSDeviceID"].ToString();
                dGVDailySales.Rows[n].Cells[8].Value = item["DSTransactionNumber"].ToString();
            }
            printdailysales = GetDailySales;
        }

        private void GetDailySaleswithDate()
        {
            string GetDailySalesWithDate = "Select* from DailySalesTable WHERE DSDate BETWEEN '"+dTPDateFrom.Value.ToString("MM/dd/yyyy")+ "' AND '" + dTPDateTo.Value.ToString("MM/dd/yyyy") + "'";
            Database();
            da = new SqlDataAdapter(GetDailySalesWithDate, conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dGVDailySales.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dGVDailySales.Rows.Add();

                dGVDailySales.Rows[n].Cells[0].Value = item["DSDate"].ToString();
                dGVDailySales.Rows[n].Cells[1].Value = item["DSTime"].ToString();
                dGVDailySales.Rows[n].Cells[2].Value = item["DSTax"].ToString();
                dGVDailySales.Rows[n].Cells[3].Value = item["DSVatableSales"].ToString();
                dGVDailySales.Rows[n].Cells[4].Value = item["DSPayment"].ToString();
                dGVDailySales.Rows[n].Cells[5].Value = item["DSTotal"].ToString();
                dGVDailySales.Rows[n].Cells[6].Value = item["DSChange"].ToString();
                dGVDailySales.Rows[n].Cells[7].Value = item["DSDeviceID"].ToString();
                dGVDailySales.Rows[n].Cells[8].Value = item["DSTransactionNumber"].ToString();
            }
            printdailysales = GetDailySalesWithDate;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetDailySaleswithDate();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dTPDateFrom.Value = DateTime.Now;
            dTPDateTo.Value = DateTime.Now;
            LoadDailySales();
        }

        private void TransactionHistory()
        {
            string GetDailySales = "Select  * from InvoiceTable";
            Database();
            da = new SqlDataAdapter(GetDailySales, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dGVTransactionHistory.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dGVTransactionHistory.Rows.Add();
                dGVTransactionHistory.Rows[n].Cells[0].Value = item["InvoiceTableNumber"].ToString();
                dGVTransactionHistory.Rows[n].Cells[1].Value = item["InvoiceItem"].ToString();
                dGVTransactionHistory.Rows[n].Cells[2].Value = item["InvoiceQuantity"].ToString();
                dGVTransactionHistory.Rows[n].Cells[3].Value = item["InvoicePrice"].ToString();
                dGVTransactionHistory.Rows[n].Cells[4].Value = item["InvoicePriceAmount"].ToString();
                dGVTransactionHistory.Rows[n].Cells[5].Value = item["InvoiceVatableSales"].ToString();
                dGVTransactionHistory.Rows[n].Cells[6].Value = item["InvoiceTaxTotal"].ToString();
                dGVTransactionHistory.Rows[n].Cells[7].Value = item["InvoiceSeniorCitizenPWDDiscount"].ToString();
                dGVTransactionHistory.Rows[n].Cells[8].Value = item["InvoiceHolidayDiscount"].ToString();
                dGVTransactionHistory.Rows[n].Cells[9].Value = item["InvoiceTotal"].ToString();
                dGVTransactionHistory.Rows[n].Cells[10].Value = item["InvoicePayment"].ToString();
                dGVTransactionHistory.Rows[n].Cells[11].Value = item["InvoiceChange"].ToString();
                dGVTransactionHistory.Rows[n].Cells[12].Value = item["InvoiceTime"].ToString();
                dGVTransactionHistory.Rows[n].Cells[13].Value = item["InvoiceDate"].ToString();
                dGVTransactionHistory.Rows[n].Cells[14].Value = item["InvoiceCashier"].ToString();
                dGVTransactionHistory.Rows[n].Cells[15].Value = item["InvoiceTransactionNumber"].ToString();
            }
        }

        private void DailySalesForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                if(pnlTransactionHistory.Visible == true)
                {
                    pnlTransactionHistory.Visible = false;
                }
            }
        }

        private void btnTransactionHistory_Click(object sender, EventArgs e)
        {
            pnlTransactionHistory.Visible = true;
        }

        private void btnPrintDailySales_Click(object sender, EventArgs e)
        {
            DailySalesViewer dailySalesViewer = new DailySalesViewer();
            dailySalesViewer.print = printdailysales;
            dailySalesViewer.Show();
        }

        private void BestSeller()
        {
            Database();
            //string getbestseller = "Select InvoiceTable.InvoiceItem, InvoiceTable.InvoiceQuantity, FoodMenu.FoodPic from InvoiceTable INNER JOIN FoodMenu ON InvoiceTable.InvoiceItem=FoodMenu.FoodName where InvoiceQuantity = (SELECT MAX(InvoiceQuantity) from InvoiceTable)";
            //string getbestseller = "Select InvoiceTable.InvoiceItem, InvoiceTable.InvoiceQuantity, FoodMenu.FoodPic from InvoiceTable INNER JOIN FoodMenu ON InvoiceTable.InvoiceItem=FoodMenu.FoodName where InvoiceQuantity = (SELECT MAX(InvoiceQuantity)from InvoiceTable)";
            string getbestseller = "SELECT     InvoiceItem, MAX(SumQuantity) AS Expr1 FROM(SELECT SUM(InvoiceQuantity) AS SumQuantity, InvoiceItem FROM InvoiceTable GROUP BY InvoiceItem) AS foo GROUP BY InvoiceItem";
            cmd = new SqlCommand(getbestseller, conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lblBestSellingName.Text = dr[0].ToString();
                lblBestSellingAmountSold.Text = dr[1].ToString();
                dr.Close();

                string getbestsellerpicture = "SELECT FoodPic from FoodMenu where FoodName = '" + lblBestSellingName.Text + "'";
                cmd = new SqlCommand(getbestsellerpicture, conn);
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                byte[] photo_array = (byte[])dataReader[0];
                MemoryStream ms = new MemoryStream(photo_array);
                ms.Position = 0;
                ms.Read(photo_array, 0, photo_array.Length);
                pctrbxFoodPic.Image = Image.FromStream(ms);
            }
            dr.Close();
            conn.Close();
        }
    }
}
