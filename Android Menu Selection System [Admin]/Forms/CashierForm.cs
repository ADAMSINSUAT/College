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
    public partial class CashierForm : Form
    {
        SqlDependency dependency;
        SqlConnection conn;
        SqlDataAdapter da;
        SqlCommand cmd;
        SqlDataReader dr;
        PictureBox pictureBox;
        Label foodid;
        Button btnAdd;

        //Get Values from queuetable//
        int orderquantity; //Item price quantity
        int originalquantity; //Item price original quantity

        decimal priceamount; //Item price amount
        decimal vatablesales; //Item order vatablesales
        decimal totaldue; //Item order total
        string ordercmd; //Item order command

        int transactionnumber;

        bool iffrominvoice;
        //Get Values end//

        //Get Values from SeniorCitizensPWDsDiscountTable//
        decimal seniorcitizenandpwddiscount; //Discount price for senior citizens and pwds
        //Get Values from SeniorCitizensPWDsDiscountTable end//


        //Get Values from flpSearchResults panel of Food Menu//
        decimal oldflpSearchResultsprice; //Old price value of menu item
        string oldflpSearchResultspriceaddcurrencysign; //Peso string equivalent of the oldflpSearchResultsprice
        string oldflpSearchResultspriceremovecurrencysign; //String value to remove the "P" sign of the flpSearchResult item price
        decimal oldflpSearchResultspricepricewithpesosign; //Decimal value to convert string of oldflpSearchResultspriceremovecurrencysign and perform operations on it
        string oldflpSearchResultorderitem; //String value to hold the name of flpSearchResultpanel result
        decimal oldflpSearchResultfinalprice;  //Decimal value to hold the result of the final price after adding the quantity
                                               //Get Values from flpSearchResults panel of Food Menu end//

        //Get Values for tax/discount//
        bool iftax; //Bool for checking whether there is a tax or not
        bool ifdiscount; //Bool for checking whether there is a discount or not
        //Get Values for tax/discount end//

        //Get Values for voiding items//
        int voiditem;
        bool ifvoided;
        //Get Values for voiding items end//

        //Get Values for Ordering Items//
        string ordercommand;
        //Get Values for Ordering Items end//

        //Get Values for VAT//
        decimal vatitemprice;
        //Get Values for VAT end//
        string connectionstring = "Data Source=DESKTOP-2N78NV2;Initial Catalog=Android Food Menu DB;Persist Security Info=True;User ID=Restaurant Admin;Password=foodsotasty1999";

        //Get Value for login time//
        public string logintime;
        public string logindate;
        //Get Value for login time ennd//

        int TableNumber;

        int rowdelete; //Retrieves number of datagridview row to be deleted
        public CashierForm()
        {
            InitializeComponent();
        }
        private void CashierForm_Load(object sender, EventArgs e)
        {
            SqlDependency.Start(connectionstring);
            SqlDetect();
            refresh();
            //dGV();
            //ComputeTotalDue();
            ReceiptNumberFormat();
        }
        //Beginning of methods
        private void Database()
        {
            conn = ConnectDB.ConnectDatabase();
        }
        public void Clear()
        {
            if (dGVOrders.InvokeRequired)
            {
                dGVOrders.Invoke(new Action(() => dGVOrders.Rows.Clear()));
            }
            else
            {
                dGVOrders.Rows.Clear();
            }
            if (lblVatableSales.InvokeRequired)
            {
                lblVatableSales.Invoke(new Action(() => lblVatableSales.Text = "P0"));
            }
            else
            {
                lblVatableSales.Text = "P0";
            }
            if (lblTax.InvokeRequired)
            {
                lblTax.Invoke(new Action(() => lblTax.Text = "P0"));
            }
            else
            {
                lblTax.Text = "P0";
            }
            if(lblVATExemptSales.InvokeRequired)
            {
                lblVATExemptSales.Invoke(new Action(() => lblVATExemptSales.Text = "P0"));
            }
            else
            {
                lblVATExemptSales.Text = "P0";
            }
            if (lblSpecialDiscount.InvokeRequired)
            {
                lblSpecialDiscount.Invoke(new Action(() => lblSpecialDiscount.Text = "P0"));
            }
            else
            {
                lblSpecialDiscount.Text = "P0";
            }
            if (lblTotalDue.InvokeRequired)
            {
                lblTotalDue.Invoke(new Action(() => lblTotalDue.Text = "P0"));
            }
            else
            {
                lblTotalDue.Text = "P0";
            }
            if (NUPPayment.InvokeRequired)
            {
                NUPPayment.Invoke(new Action(() => NUPPayment.Value = 0));
            }
            else
            {
                NUPPayment.Value = 0;
            }
            if (lblChange.InvokeRequired)
            {
                lblChange.Invoke(new Action(() => lblChange.Text = "P0"));
            }
            else
            {
                lblChange.Text = "P0";
            }
            if(btnProcessOrder.InvokeRequired)
            {
                btnProcessOrder.Invoke(new Action(() => btnProcessOrder.Enabled = false));
            }
            else
            {
                btnProcessOrder.Enabled = false;
            }
        }
        private void SaveOrder()
        {
            Database();
            if(NUPPayment.Value>0 &&  Convert.ToDecimal(lblTotalDue.Text.Replace("P", string.Empty))>0)
            {
                if (iffrominvoice == false)
                {

                    int month = Convert.ToInt32(DateTime.Now.Month.ToString());
                    int year = Convert.ToInt32(DateTime.Now.Year.ToString());
                    int tablenumber = Convert.ToInt32(lblCurrentlyServing.Text);
                    int transactionumber;
                    string newmonth = DateTime.Now.ToString("MM");
                    decimal invoiceprice, invoicepriceamount, invoicevatablesales, invoiceseniorpwddiscount,
                        invoiceholidaydiscount, invoicetotal, invoicepayment, invoicechange, invoicetaxtotal;
                    string invoiceitem, invoicetime, invoicemonth, invoiceday, invoiceyear,
                         invoiceordercommand, invoicecashier, invoicetimetocook;
                    //DateTime invoicedate;
                    int invoicequantity, invoicetransactionnumber;

                    double itemquantity;
                    //DateTime timetocook;
                    string selectorder = "Select * from QueueTable where TableNumber='" + Convert.ToInt32(lblCurrentlyServing.Text) + "'";
                    string deletefromqueuetable = "Delete from QueueTable where TableNumber ='" + tablenumber + "'";
                    string saveorder = "Insert into InvoiceTable (InvoiceTableNumber,InvoiceItem,InvoiceQuantity,InvoicePrice," +
                        "InvoicePriceAmount,InvoiceVatableSales,InvoiceTaxTotal,InvoiceSeniorCitizenPWDDiscount,InvoiceHolidayDiscount," +
                        "InvoiceTotal,InvoicePayment,InvoiceChange,InvoiceTime,InvoiceMonth,InvoiceDay,InvoiceYear,InvoiceDate," +
                        "InvoiceOrderCommand,InvoiceCashier,InvoiceTimeToCook,InvoiceTransactionNumber)VALUES(@InvoiceTableNumber,@InvoiceItem,@InvoiceQuantity," +
                        "@InvoicePrice,@InvoicePriceAmount,@InvoiceVatableSales,@InvoiceTaxTotal,@InvoiceSeniorCitizenPWDDiscount," +
                        "@InvoiceHolidayDiscount,@InvoiceTotal,@InvoicePayment,@InvoiceChange,@InvoiceTime,@InvoiceMonth,@InvoiceDay," +
                        "@InvoiceYear,@InvoiceDate,@InvoiceOrderCommand,@InvoiceCashier,@InvoiceTimeToCook,@InvoiceTransactionNumber)";
                    //String verifycashier = "Select UserID, UserName, UserPassword, UserRole, UserStatus from UserList where UserName = '" + lblCashierName.Text + "' AND UserPassword = '" + txtBxCashierOrderPassword.Text + "'";
                    string saveintodailysales = "Insert into DailySalesTable (DSDate,DSTime,DSTax,DSVatableSales,DSPayment,DSTotal," +
                        "DSChange,DSDeviceID,DSTransactionNumber)VALUES(@DSDate,@DSTime,@DSTax,@DSVatableSales,@DSPayment,@DSTotal," +
                        "@DSChange,@DSDeviceID,@DSTransactionNumber)";
                    DataTable dt = new DataTable();

                    //invoicetransactionnumber = GetTransactionNumber();
                    da = new SqlDataAdapter(selectorder, conn);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        ordercommand = "Cook item";
                        tablenumber = Convert.ToInt32(dr[0].ToString());
                        invoiceitem = Convert.ToString(dr[2].ToString());
                        invoicequantity = Convert.ToInt32(dr[3].ToString());
                        invoiceprice = Convert.ToDecimal(dr[4].ToString());
                        invoicepriceamount = Convert.ToDecimal(dr[5].ToString());
                        invoicevatablesales = Convert.ToDecimal(lblVatableSales.Text.Replace("P", string.Empty));
                        invoicetaxtotal = Convert.ToDecimal(lblTax.Text.Replace("P", string.Empty));
                        invoiceseniorpwddiscount = Convert.ToDecimal(lblVATExemptSales.Text.Replace("P", string.Empty));
                        invoiceholidaydiscount = Convert.ToDecimal(lblSpecialDiscount.Text.Replace("P", string.Empty));
                        invoicetotal = Convert.ToDecimal(lblTotalDue.Text.Replace("P", string.Empty));
                        invoicepayment = Convert.ToDecimal(NUPPayment.Value);
                        invoicechange = Convert.ToDecimal(lblChange.Text.Replace("P", string.Empty));
                        invoicetime = Convert.ToString(DateTime.Now.ToString("h:mm tt"));
                        invoicemonth = Convert.ToString(DateTime.Now.ToString("MM"));
                        invoiceday = Convert.ToString(DateTime.Now.ToString("dd"));
                        invoiceyear = Convert.ToString(DateTime.Now.Year);
                        //invoicedate = DateTime.Now.ToString("dd/MM/yyyy");
                        invoiceordercommand = "Being cooked";
                        invoicecashier = lblCashierName.Text;
                        invoicetransactionnumber = Convert.ToInt32(dr[7].ToString());
                        conn.Close();

                        Database();
                        cmd = new SqlCommand("Select TimetoCook from FoodMenu where FoodName = '" + invoiceitem + "'", conn);
                        SqlDataReader dataReader;
                        dataReader = cmd.ExecuteReader();
                        dataReader.Read();
                        invoicetimetocook = Convert.ToString(dataReader["TimetoCook"].ToString());
                        dataReader.Close();

                        Database();
                        cmd = new SqlCommand(saveorder, conn);
                        cmd.Parameters.AddWithValue("@InvoiceTableNumber", tablenumber);
                        cmd.Parameters.AddWithValue("@InvoiceItem", invoiceitem);
                        cmd.Parameters.AddWithValue("@InvoiceQuantity", invoicequantity);
                        cmd.Parameters.AddWithValue("@InvoicePrice", invoiceprice);
                        cmd.Parameters.AddWithValue("@InvoicePriceAmount", invoicepriceamount);
                        cmd.Parameters.AddWithValue("@InvoiceVatableSales", invoicevatablesales);
                        cmd.Parameters.AddWithValue("@InvoiceTaxTotal", invoicetaxtotal);
                        cmd.Parameters.AddWithValue("@InvoiceSeniorCitizenPWDDiscount", invoiceseniorpwddiscount);
                        cmd.Parameters.AddWithValue("@InvoiceHolidayDiscount", invoiceholidaydiscount);
                        cmd.Parameters.AddWithValue("@InvoiceTotal", invoicetotal);
                        cmd.Parameters.AddWithValue("@InvoicePayment", invoicepayment);
                        cmd.Parameters.AddWithValue("@InvoiceChange", invoicechange);
                        cmd.Parameters.AddWithValue("@InvoiceTime", invoicetime);
                        cmd.Parameters.AddWithValue("@InvoiceMonth", invoicemonth);
                        cmd.Parameters.AddWithValue("@InvoiceDay", invoiceday);
                        cmd.Parameters.AddWithValue("@InvoiceYear", invoiceyear);
                        cmd.Parameters.AddWithValue("@InvoiceDate", DateTime.Now.ToString("MM/dd/yyyy"));
                        cmd.Parameters.AddWithValue("@InvoiceOrderCommand", invoiceordercommand);
                        cmd.Parameters.AddWithValue("@InvoiceCashier", invoicecashier);
                        cmd.Parameters.AddWithValue("@InvoiceTimeToCook", invoicetimetocook);
                        cmd.Parameters.AddWithValue("@InvoiceTransactionNumber", invoicetransactionnumber);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        TransactionNumber.transactionnumber = invoicetransactionnumber;
                    }

                    string time;
                    decimal taxtotal, vatablesales, payment, total, change;
                    Database();
                    string selectfrominvoice = "Select * from InvoiceTable where InvoiceTransactionNumber='" + TransactionNumber.transactionnumber + "'";
                    cmd = new SqlCommand(selectfrominvoice, conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    time = dr["InvoiceTime"].ToString();
                    taxtotal = Convert.ToDecimal(dr["InvoiceTaxTotal"].ToString());
                    vatablesales = Convert.ToDecimal(dr["InvoiceVatableSales"].ToString());
                    payment = Convert.ToDecimal(dr["InvoicePayment"].ToString());
                    total = Convert.ToDecimal(dr["InvoiceTotal"].ToString());
                    change = Convert.ToDecimal(dr["InvoiceChange"].ToString());
                    int newtransactionumber = Convert.ToInt32(dr["InvoiceTransactionNumber"].ToString());
                    dr.Close();
                    conn.Close();

                    cmd = new SqlCommand(saveintodailysales, conn);
                    if(conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    cmd.Parameters.AddWithValue("@DSDate", DateTime.Now.ToString("MM/dd/yyyy"));
                    cmd.Parameters.AddWithValue("@DSTime", time);
                    cmd.Parameters.AddWithValue("@DSTax", taxtotal);
                    cmd.Parameters.AddWithValue("@DSVatableSales", vatablesales);
                    cmd.Parameters.AddWithValue("@DSPayment", payment);
                    cmd.Parameters.AddWithValue("@DSTotal", total);
                    cmd.Parameters.AddWithValue("@DSChange", change);
                    cmd.Parameters.AddWithValue("@DSDeviceID", tablenumber);
                    cmd.Parameters.AddWithValue("@DSTransactionNumber", newtransactionumber);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    Database();
                    using (cmd = new SqlCommand(deletefromqueuetable, conn))
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    MessageBox.Show("Transaction successful");
                    CustomerInvoiceViewer civ = new CustomerInvoiceViewer();
                    civ.txtTransactionNumber.Text = Convert.ToString(TransactionNumber.transactionnumber);
                    civ.Show();
                    TransactionNumber.transactionnumber = 0;
                }
                else
                {
                    string VoidItem = "Update InvoiceTable set InvoiceVoided=@InvoiceVoided where InvoiceTransactionNumber='" + TransactionNumber.transactionnumber + "'";
                    cmd = new SqlCommand(VoidItem, conn);
                    cmd.Parameters.AddWithValue("@InvoiceVoided", "True");
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    {
                        CustomerInvoiceRefresh();
                        string UpDateTransaction = "Update DailySalesTable set DSTax=@DSTax, DSVatableSales=@DSVatableSales, DSPayment=@DSPayment, DSTotal=@DSTotal, DSChange=@DSChange where DSTransactionNumber='" + TransactionNumber.transactionnumber + "'";
                        cmd = new SqlCommand(UpDateTransaction, conn);
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        cmd.Parameters.AddWithValue("@DSTax", Math.Round(Convert.ToDecimal(lblTax.Text.Replace("P", string.Empty)), 2));
                        cmd.Parameters.AddWithValue("@DSVatableSales", Math.Round(Convert.ToDecimal(lblVatableSales.Text.Replace("P", string.Empty)), 2));
                        cmd.Parameters.AddWithValue("@DSPayment", Math.Round(NUPPayment.Value, 2));
                        cmd.Parameters.AddWithValue("@DSTotal", Math.Round(Convert.ToDecimal(lblTotalDue.Text.Replace("P", string.Empty)), 2));
                        cmd.Parameters.AddWithValue("@DSChange", Math.Round(Convert.ToDecimal(lblChange.Text.Replace("P", string.Empty)), 2));
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Payment cannot be zero or less!");
            }
        }
        private void voidsingleitem()
        {
            Database();
            if (iffrominvoice == false)
            {
                String verifycashier = "Select UserID, UserName, UserPassword, UserRole, UserStatus from UserList where UserName = '" + lblCashierName.Text + "' AND UserPassword = '" + txtCashierPassword.Text + "'";
                String getselectedorder = "Select * from QueueTable where Item ='" + dGVOrders.CurrentRow.Cells[0].Value.ToString() + "'";
                String deleteitem = "Delete from QueueTable where Item='" + dGVOrders.CurrentRow.Cells[0].Value.ToString() + "'";
                String voiditem = "Insert into VoidTable (VoidTableNumber, VoidInvoiceItem, VoidInvoiceQuantity, VoidOrderPrice, VoidOrderPriceAmount, VoidInvoiceTotal, VoidInvoiceTime, VoidInvoiceMonth, VoidInvoiceDay, VoidInvoiceYear, VoidInvoiceItemVat, VoidTransactionNumber)VALUES(@VoidTableNumber, @VoidInvoiceItem, @VoidInvoiceQuantity, @VoidOrderPrice, @VoidOrderPriceAmount, @VoidInvoiceTotal, @VoidInvoiceTime, @VoidInvoiceMonth, @VoidInvoiceDay, @VoidInvoiceYear, @VoidInvoiceItemVat, @VoidTransactionNumber)";
                int tablenumber, itemquantity, itemtransactionnumber;
                decimal itemprice, itempriceamount, itempricetotal, itemvat;
                string item, itemtime, itemmonth, itemday, itemyear;

                using (cmd = new SqlCommand(verifycashier, conn))
                {
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr["UserName"].ToString() == lblCashierName.Text && dr["UserPassword"].ToString() == txtCashierPassword.Text)
                        {
                            using (cmd = new SqlCommand(getselectedorder, conn))
                            {
                                dr.Close();
                                SqlDataReader drgetselectedorder = cmd.ExecuteReader();
                                if (drgetselectedorder.Read())
                                {
                                    tablenumber = Convert.ToInt32(drgetselectedorder[0].ToString());
                                    item = Convert.ToString(drgetselectedorder[2].ToString());
                                    itemquantity = Convert.ToInt32(drgetselectedorder[3].ToString());
                                    itemprice = Convert.ToDecimal(drgetselectedorder[4].ToString());
                                    itempriceamount = Convert.ToDecimal(drgetselectedorder[5].ToString());
                                    itempricetotal = Convert.ToDecimal(lblTotalDue.Text.Replace("P", string.Empty));
                                    itemtime = Convert.ToString(DateTime.Now.ToString("h:mm tt"));
                                    itemmonth = Convert.ToString(DateTime.Now.ToString("MM"));
                                    itemday = Convert.ToString(DateTime.Now.ToString("dd"));
                                    itemyear = Convert.ToString(DateTime.Now.Year);
                                    itemvat = Convert.ToDecimal(lblTax.Text.Replace("P", string.Empty));
                                    itemtransactionnumber = Convert.ToInt32(drgetselectedorder[7].ToString());
                                    drgetselectedorder.Close();

                                    using (cmd = new SqlCommand(voiditem, conn))
                                    {
                                        Database();
                                        cmd.Parameters.AddWithValue("@VoidTableNumber", tablenumber);
                                        cmd.Parameters.AddWithValue("@VoidInvoiceItem", item);
                                        cmd.Parameters.AddWithValue("@VoidInvoiceQuantity", itemquantity);
                                        cmd.Parameters.AddWithValue("@VoidOrderPrice", itemprice);
                                        cmd.Parameters.AddWithValue("@VoidOrderPriceAmount", itempriceamount);
                                        cmd.Parameters.AddWithValue("@VoidInvoiceTotal", itempricetotal);
                                        cmd.Parameters.AddWithValue("@VoidInvoiceTime", itemtime);
                                        cmd.Parameters.AddWithValue("@VoidInvoiceMonth", itemmonth);
                                        cmd.Parameters.AddWithValue("@VoidInvoiceDay", itemday);
                                        cmd.Parameters.AddWithValue("@VoidInvoiceYear", itemyear);
                                        cmd.Parameters.AddWithValue("@VoidInvoiceItemVat", itemvat);
                                        cmd.Parameters.AddWithValue("@VoidTransactionNumber", itemtransactionnumber);
                                        cmd.ExecuteNonQuery();
                                        conn.Close();
                                    }
                                    Database();
                                    using (cmd = new SqlCommand(deleteitem, conn))
                                    {
                                        cmd.ExecuteNonQuery();
                                        conn.Close();
                                        MessageBox.Show("Item has been voided");
                                        //dGVOrders.Rows.RemoveAt(rowdelete);
                                        pnlVoid.Hide();
                                        //dGV();
                                    }
                                }
                            }
                        }
                        else if (dr["UserName"].ToString() == lblCashierName.Text && dr["UserPassword"].ToString() != txtCashierPassword.Text)
                        {
                            MessageBox.Show("Incorrect password!");
                        }
                        else
                        {
                            MessageBox.Show("No such user exists!");
                        }
                    }
                }
            }
            else
            {
                string VoidItem = "Update InvoiceTable set InvoiceVoided=@InvoiceVoided where InvoiceTransactionNumber='" + TransactionNumber.transactionnumber + "' AND InvoiceItem='"+dGVOrders.CurrentRow.Cells[0].Value.ToString()+"'";
                cmd = new SqlCommand(VoidItem, conn);
                cmd.Parameters.AddWithValue("@InvoiceVoided", "True");
                cmd.ExecuteNonQuery();
                conn.Close();
                {
                    CustomerInvoiceRefresh();
                    string UpDateTransaction = "Update DailySalesTable set DSTax=@DSTax, DSVatableSales=@DSVatableSales, DSPayment=@DSPayment, DSTotal=@DSTotal, DSChange=@DSChange where DSTransactionNumber='" + TransactionNumber.transactionnumber + "'";
                    cmd = new SqlCommand(UpDateTransaction, conn);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    cmd.Parameters.AddWithValue("@DSTax", Math.Round(Convert.ToDecimal(lblTax.Text.Replace("P", string.Empty)), 2));
                    cmd.Parameters.AddWithValue("@DSVatableSales", Math.Round(Convert.ToDecimal(lblVatableSales.Text.Replace("P", string.Empty)), 2));
                    cmd.Parameters.AddWithValue("@DSPayment", Math.Round(NUPPayment.Value, 2));
                    cmd.Parameters.AddWithValue("@DSTotal", Math.Round(Convert.ToDecimal(lblTotalDue.Text.Replace("P", string.Empty)), 2));
                    cmd.Parameters.AddWithValue("@DSChange", Math.Round(Convert.ToDecimal(lblChange.Text.Replace("P", string.Empty)), 2));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        private void voidentireorder()
        {
            Database();

            int TableNumber = Convert.ToInt32(lblCurrentlyServing.Text);

            String verifycashier = "Select UserID, UserName, UserPassword, UserRole, UserStatus from UserList where UserName = '" + lblCashierName.Text + "' AND UserPassword = '" + txtCashierPassword.Text + "'";
            String getcurrentorders = "Select * from QueueTable where TableNumber='" + TableNumber + "'";
            String voidorder = "Insert into VoidTable (VoidTableNumber, VoidInvoiceItem, VoidInvoiceQuantity, VoidOrderPrice, VoidOrderPriceAmount, VoidInvoiceVatTotal, VoidInvoiceTime, VoidInvoiceMonth, VoidInvoiceDay, VoidInvoiceYear, VoidInvoiceItemVat, VoidTransactionNumber)VALUES(@VoidTableNumber, @VoidInvoiceItem, @VoidInvoiceQuantity, @VoidOrderPrice, @VoidOrderPriceAmount, @VoidInvoiceVatTotal, @VoidInvoiceTime, @VoidInvoiceMonth, @VoidInvoiceDay, @VoidInvoiceYear, @VoidInvoiceItemVat, @VoidTransactionNumber)";
            int tablenumber, itemquantity, itemtransactionnumber;
            decimal itemprice, itempriceamount, itempricetotal, itemvat;
            string item, itemtime, itemmonth, itemday, itemyear;

            DataTable dt = new DataTable();

            da = new SqlDataAdapter(getcurrentorders, conn);
            da.Fill(dt);
            using (cmd = new SqlCommand(verifycashier, conn))
            {
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["UserName"].ToString() == lblCashierName.Text && dr["UserPassword"].ToString() == txtCashierPassword.Text)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Database();

                            tablenumber = Convert.ToInt32(dr[0].ToString());
                            item = Convert.ToString(dr[2].ToString());
                            itemquantity = Convert.ToInt32(dr[3].ToString());
                            itemprice = Convert.ToDecimal(dr[4].ToString());
                            itempriceamount = Convert.ToDecimal(dr[5].ToString());
                            itempricetotal = Convert.ToDecimal(lblTotalDue.Text.Replace("P", string.Empty));
                            itemtime = Convert.ToString(DateTime.Now.ToString("h:mm tt"));
                            itemmonth = Convert.ToString(DateTime.Now.ToString("MM"));
                            itemday = Convert.ToString(DateTime.Now.ToString("dd"));
                            itemyear = Convert.ToString(DateTime.Now.Year);
                            itemvat = Convert.ToDecimal(lblTax.Text.Replace("P", string.Empty));
                            itemtransactionnumber = Convert.ToInt32(dr["TransactionNumber"].ToString());
                            conn.Close();

                            Database();
                            cmd = new SqlCommand(voidorder, conn);
                            cmd.Parameters.AddWithValue("@VoidTableNumber", tablenumber);
                            cmd.Parameters.AddWithValue("@VoidInvoiceItem", item);
                            cmd.Parameters.AddWithValue("@VoidInvoiceQuantity", itemquantity);
                            cmd.Parameters.AddWithValue("@VoidOrderPrice", itemprice);
                            cmd.Parameters.AddWithValue("@VoidOrderPriceAmount", itempriceamount);
                            cmd.Parameters.AddWithValue("@VoidInvoiceTotal", itempricetotal);
                            cmd.Parameters.AddWithValue("@VoidInvoiceTime", itemtime);
                            cmd.Parameters.AddWithValue("@VoidInvoiceMonth", itemmonth);
                            cmd.Parameters.AddWithValue("@VoidInvoiceDay", itemday);
                            cmd.Parameters.AddWithValue("@VoidInvoiceYear", itemyear);
                            cmd.Parameters.AddWithValue("@VoidInvoiceItemVat", itemvat);
                            cmd.Parameters.AddWithValue("@VoidTranscationNumber", itemtransactionnumber);
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            String deleteitem = "Delete from QueueTable where Item='" + item + "'";
                            cmd = new SqlCommand(deleteitem, conn);
                            Database();

                            cmd.ExecuteNonQuery();
                            ifvoided = true;
                            conn.Close();
                            pnlVoid.Hide();
                        }
                        MessageBox.Show("Order has been voided");
                    }
                    else if (dr["UserName"].ToString() == lblCashierName.Text && dr["UserPassword"].ToString() != txtCashierPassword.Text)
                    {
                        MessageBox.Show("Incorrect password!");
                    }
                    else
                    {
                        MessageBox.Show("No such user exists!");
                    }
                }
            }
        }

        private void ReceiptNumberFormat()
        {
            try
            {
                string pesosubtotal, pesotax, pesoseniorcitizenpwddiscount, pesospecialdiscount, pesototaldue, pesochange;

                pesosubtotal = lblVatableSales.Text; pesotax = lblTax.Text; pesoseniorcitizenpwddiscount = lblVATExemptSales.Text;
                pesospecialdiscount = lblSpecialDiscount.Text; pesototaldue = lblTotalDue.Text; pesochange = lblChange.Text; ;

                string removepesosubtotal, removepesotax, removepesoseniorcitizenpwddiscount, removepesospecialdiscount, removepesototaldue, removepesochange;

                removepesosubtotal = pesosubtotal.Replace("P", string.Empty); removepesotax = pesotax.Replace("P", string.Empty);
                removepesoseniorcitizenpwddiscount = pesoseniorcitizenpwddiscount.Replace("P", string.Empty);
                removepesospecialdiscount = pesospecialdiscount.Replace("P", string.Empty);
                removepesototaldue = pesototaldue.Replace("P", string.Empty); removepesochange = pesochange.Replace("P", string.Empty);

                double subtotal, tax, seniorcitizenpwddiscount, specialdiscount, totaldue, change;

                subtotal = Convert.ToDouble(removepesosubtotal);
                tax = Convert.ToDouble(removepesotax);
                seniorcitizenpwddiscount = Convert.ToDouble(removepesoseniorcitizenpwddiscount);
                specialdiscount = Convert.ToDouble(removepesospecialdiscount);
                totaldue = Convert.ToDouble(removepesototaldue);
                change = Convert.ToDouble(removepesochange);

                lblVatableSales.Text = subtotal.ToString("P"+"###,###,##0.00");
                lblTax.Text = tax.ToString("P" + "###,###,##0.00"); ;
                lblVATExemptSales.Text = seniorcitizenpwddiscount.ToString("P" + "###,###,##0.00");
                lblSpecialDiscount.Text = specialdiscount.ToString("P" + "###,###,##0.00");
                lblTotalDue.Text = totaldue.ToString("P" + "###,###,##0.00");
                lblChange.Text = change.ToString("P" + "###,###,##0.00");
            }
            catch
            {

            }
        }

        private void ComputeTotalDue()
        {
            try
            {
                string pesosubtotal, pesotax, pesoseniorcitizenpwddiscount, pesospecialdiscount, pesototaldue, pesochange;

                pesosubtotal = lblVatableSales.Text; pesotax = lblTax.Text; pesoseniorcitizenpwddiscount = lblVATExemptSales.Text;
                pesospecialdiscount = lblSpecialDiscount.Text; pesototaldue = lblTotalDue.Text; pesochange = lblChange.Text;

                string removepesosubtotal, removepesotax, removepesoseniorcitizenpwddiscount, removepesospecialdiscount, removepesototaldue, removepesochage;

                removepesosubtotal = pesosubtotal.Replace("P", string.Empty); removepesotax = pesotax.Replace("P", string.Empty);
                removepesoseniorcitizenpwddiscount = pesoseniorcitizenpwddiscount.Replace("P", string.Empty);
                removepesospecialdiscount = pesospecialdiscount.Replace("P", string.Empty);
                removepesototaldue = pesototaldue.Replace("P", string.Empty); removepesochage = pesochange.Replace("P", string.Empty);

                decimal subtotal, tax, taxvat, vat, vatexempt, seniorcitizenpwddiscount, specialdiscount, oldtotal, newtotal,  payment, change;

                subtotal = Convert.ToDecimal(removepesosubtotal);
                tax = Convert.ToDecimal(removepesotax);
                taxvat = 0;
                vat = 0;
                vatexempt = 0;
                seniorcitizenpwddiscount = Convert.ToDecimal(removepesoseniorcitizenpwddiscount);
                specialdiscount = Convert.ToDecimal(removepesospecialdiscount);
                oldtotal = Convert.ToDecimal(totaldue);
                newtotal = Convert.ToDecimal(removepesototaldue);
                payment = Convert.ToDecimal(NUPPayment.Value);
                change = Convert.ToDecimal(removepesochage);

                if(ifvoided == true)
                {
                    lblVatableSales.Text = "P0";
                    lblTax.Text = "P0";
                    lblVATExemptSales.Text = "P0";
                    lblSpecialDiscount.Text = "P0";
                    lblTotalDue.Text = "P0";
                    NUPPayment.Value = 0;
                    lblChange.Text = "P0";
                    ifvoided = false;
                    refresh();
                    return;
                }
                if (lblVatableSales.Text != "")
                {
                    subtotal = Math.Round(subtotal, 2);
                    lblVatableSales.Text = subtotal.ToString();
                }
                if (lblTax.Text != "")
                {
                    tax = Math.Round(tax, 2);
                    lblTax.Text = tax.ToString();
                }
                if (lblVATExemptSales.Text != "")
                {
                    seniorcitizenpwddiscount = Math.Round(seniorcitizenpwddiscount, 2);
                    lblVATExemptSales.Text = Convert.ToString(seniorcitizenpwddiscount);
                }
                if (lblSpecialDiscount.Text != "")
                {
                    specialdiscount = Math.Round(specialdiscount, 2);
                    lblSpecialDiscount.Text = Convert.ToString(specialdiscount);
                }
                if (lblTotalDue.Text != "")
                {
                    oldtotal = Math.Round(oldtotal, 2);
                    lblTotalDue.Text = Convert.ToString(oldtotal);
                }
                if(lblChange.Text != "")
                {
                    change = Math.Round(change, 2);
                    lblChange.Text = Convert.ToString(change);
                }

                if (subtotal != 0) //Check whether subtotal and tax is not empty
                {                //Get tax if it's not 0
                                 //If true, set tax to 0
                    if (iftax == false)
                    {
                        tax = 0;

                        if (seniorcitizenpwddiscount == Convert.ToDecimal(0.20))
                        {
                            taxvat = Convert.ToDecimal(1.12);
                            vatexempt = oldtotal / taxvat;
                            seniorcitizenpwddiscount = seniorcitizenpwddiscount * Math.Round(vatexempt, 2);
                            seniorcitizenpwddiscount = Math.Round(seniorcitizenpwddiscount, 2);
                        }
                        else if (seniorcitizenpwddiscount == 0 && tax != 0)
                        {
                            seniorcitizenpwddiscount = seniorcitizenandpwddiscount;
                            taxvat = Convert.ToDecimal(1.12);
                            vatexempt = oldtotal / taxvat;
                            seniorcitizenpwddiscount = seniorcitizenpwddiscount * Math.Round(vatexempt, 2);
                            seniorcitizenpwddiscount = Math.Round(seniorcitizenpwddiscount, 2);
                        }
                    }
                    else
                    {
                        if(ifdiscount == true)
                        {
                            tax = 0;
                        }
                        else
                        {
                            newtotal = oldtotal;
                            vat = subtotal * Convert.ToDecimal(0.12);
                            vat = Math.Round(vat, 2);
                            taxvat = 1 + tax;
                            taxvat = Math.Round(taxvat, 2);
                            seniorcitizenandpwddiscount = 0;
                        }
                    }

                    if (seniorcitizenpwddiscount > 0) //Check whether senior citizen/pwd value is 0
                    {
                        if (specialdiscount != 0) //Check whether there is a special discount
                        {                         //If true, compute total due where total due is equals to subtotal plus tax minus specialdiscount
                                                  //specialdiscount = specialdiscount * subtotal;
                            newtotal = oldtotal;
                            newtotal -= seniorcitizenpwddiscount + specialdiscount;
                            //if(vat>0)
                            //{
                            //    newtotal = subtotal + vat - specialdiscount;
                            //}
                            //else
                            //{
                            //    if(seniorcitizenpwddiscount>0)
                            //    {
                            //        newtotal -= specialdiscount - seniorcitizenpwddiscount;
                            //    }
                            //    else
                            //    {
                            //        newtotal -= specialdiscount;
                            //    }
                            //}
                            //}
                            if (payment !=0 && payment >= oldtotal)
                            {
                                change = payment - oldtotal;
                                lblChange.Text = Convert.ToString("P" + Math.Round(change, 2));
                            }
                            else if(payment != 0 && payment < oldtotal)
                            {
                                lblChange.Text = "P0";
                                change = oldtotal - payment;
                                MessageBox.Show("Your payment is not enough! You're short of: P" + change);
                            }
                            lblVatableSales.Text = Convert.ToString("P" + Math.Round(subtotal, 2));
                            lblTax.Text = Convert.ToString("P" + Math.Round(vat, 2));
                            lblVATExemptSales.Text = Convert.ToString("P" + Math.Round(seniorcitizenpwddiscount, 2));
                            lblSpecialDiscount.Text = Convert.ToString("P" + Math.Round(specialdiscount, 2));
                            lblTotalDue.Text = Convert.ToString("P" + Math.Round(newtotal, 2));
                        }
                        else
                        {                         //If false, compute total due where  total due is equals to subtotal + tax
                            if(vat!=0)
                            {
                                newtotal = subtotal + vat;
                            }
                            newtotal -= seniorcitizenpwddiscount;
                            if (payment != 0 && payment >= oldtotal)
                            {
                                change = payment - oldtotal;
                                lblChange.Text = Convert.ToString("P" + Math.Round(change, 2));
                            }
                            else if (payment != 0 && payment < oldtotal)
                            {
                                lblChange.Text = "P0";
                                change = oldtotal - payment;
                                MessageBox.Show("Your payment is not enough! You're short of: P" + change);
                            }
                            lblVatableSales.Text = Convert.ToString("P" + Math.Round(subtotal, 2));
                            lblTax.Text = Convert.ToString("P" + Math.Round(vat, 2));
                            lblVATExemptSales.Text = Convert.ToString("P" + Math.Round(seniorcitizenpwddiscount, 2));
                            lblSpecialDiscount.Text = Convert.ToString("P" + Math.Round(specialdiscount, 2));
                            lblTotalDue.Text = Convert.ToString("P" + Math.Round(newtotal, 2));
                        }
                    }
                    else
                    {                             //If senior citizen/pwd is is not zero, then...
                        if (specialdiscount != 0)//Check whether there is a special discount
                        {                        //If true, compute total due where total due is equals to subtotal plug tax - specialdiscount
                            //specialdiscount = specialdiscount * subtotal;
                            if(vat==0)
                            {
                                newtotal -= specialdiscount;
                            }
                            else
                            {
                                newtotal = subtotal + vat - specialdiscount;
                            }
                            if (payment != 0 && payment >= oldtotal)
                            {
                                change = payment - oldtotal;
                                lblChange.Text = Convert.ToString("P" + Math.Round(change, 2));
                            }
                            else if (payment != 0 && payment < oldtotal)
                            {
                                lblChange.Text = "P0";
                                change = oldtotal - payment;
                                MessageBox.Show("Your payment is not enough! You're short of: P" + change);
                            }
                            lblVatableSales.Text = Convert.ToString("P" + Math.Round(subtotal, 2));
                            lblTax.Text = Convert.ToString("P" + Math.Round(vat, 2));
                            lblVATExemptSales.Text = Convert.ToString("P" + Math.Round(seniorcitizenpwddiscount, 2));
                            lblSpecialDiscount.Text = Convert.ToString("P" + Math.Round(specialdiscount, 2));
                            lblTotalDue.Text = Convert.ToString("P" + Math.Round(newtotal, 2));
                        }
                        else
                        {                     // If false, compute total due where  total due is equals to subtotal + tax
                            if(vat<=0)
                            {
                                newtotal = subtotal + vat;
                            }
                            if (payment != 0 && payment >= oldtotal)
                            {
                                change = payment - oldtotal;
                                lblChange.Text = Convert.ToString("P" + Math.Round(change, 2));
                            }
                            else if (payment != 0 && payment < oldtotal)
                            {
                                lblChange.Text = "P0";
                                change = oldtotal - payment;
                                MessageBox.Show("Your payment is not enough! You're short of: P" + change);
                            }
                            lblVatableSales.Text = Convert.ToString("P" + Math.Round(subtotal, 2));
                            lblTax.Text = Convert.ToString("P" + Math.Round(vat, 2));
                            lblVATExemptSales.Text = Convert.ToString("P" + Math.Round(seniorcitizenpwddiscount, 2));
                            lblSpecialDiscount.Text = Convert.ToString("P" + Math.Round(specialdiscount, 2));
                            lblTotalDue.Text = Convert.ToString("P" + Math.Round(newtotal, 2));
                        }
                    }
                }
                ReceiptNumberFormat();
            }
            catch
            {

            }
        }
        private void SqlDetect()
        {
            //String getcmdorder = "select TempDeviceId, TempOrderNumber, TempInvoiceItem, TempInvoiceQuantity, TempOrderPrice, TempOrderPriceAmount, TempInvoiceVatTotal, TempInvoiceTime, TempInvoiceMonth, TempInvoiceDay, TempInvoiceYear FROM dbo.TemporaryInvoiceTable";
            using (SqlConnection connstring = new SqlConnection(connectionstring))
            {
                using (SqlCommand dependencycommand = new SqlCommand("select TableNumber, OrderNumber, Item, Quantity, Price, PriceAmount, OrderType FROM dbo.QueueTable", connstring))
                {
                    dependency = new SqlDependency(dependencycommand);

                    dependency.OnChange += new OnChangeEventHandler(dbchangenotification);

                    if(connstring.State == ConnectionState.Closed)
                    {
                        connstring.Open();
                    }

                    using (SqlDataReader dataReader = dependencycommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                        }
                    }
                }
            }
        }

        private void SqlDetectDiscounts()
        {
            //String getcmdorder = "select TempDeviceId, TempOrderNumber, TempInvoiceItem, TempInvoiceQuantity, TempOrderPrice, TempOrderPriceAmount, TempInvoiceVatTotal, TempInvoiceTime, TempInvoiceMonth, TempInvoiceDay, TempInvoiceYear FROM dbo.TemporaryInvoiceTable";
            using (SqlConnection connstring = new SqlConnection(connectionstring))
            {
                using (SqlCommand dependencycommand = new SqlCommand("select PromoName, PromoType, PromoPrice FROM PromoTable", connstring))
                {
                    dependency = new SqlDependency(dependencycommand);

                    dependency.OnChange += new OnChangeEventHandler(dbdiscountchange);

                    if (connstring.State == ConnectionState.Closed)
                    {
                        connstring.Open();
                    }

                    using (SqlDataReader dataReader = dependencycommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                        }
                    }
                }
            }
        }

        private void refresh()
        {
            Database();
            if (iffrominvoice == false)
            {
                if (ListViewOrderNumber.InvokeRequired)
                {
                    String getorders = "Select TOP(1) OrderNumber, TableNumber from QueueTable";
                    da = new SqlDataAdapter(getorders, conn);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    DataTable table = new DataTable();
                    da.Fill(table);


                    ListViewOrderNumber.Invoke(new Action(() => ListViewOrderNumber.Items.Clear()));
                    if (table != null && table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            DataRow dr = table.Rows[i];
                            ListViewItem lv = new ListViewItem(dr[0].ToString());
                            lv.SubItems.Add(dr[1].ToString());
                            ListViewOrderNumber.Invoke(new Action(() => ListViewOrderNumber.Items.Add(lv)));

                            lblCurrentlyServing.Invoke(new Action(() => lblCurrentlyServing.Text = dr[0].ToString()));
                            conn.Close();
                            dGV();
                        }
                    }
                    else
                    {
                        lblCurrentlyServing.Invoke(new Action(() => lblCurrentlyServing.Text = "0"));
                        Clear();
                    }
                }
                else
                {
                    Database();
                    String getorders = "Select TOP(1) OrderNumber, TableNumber from QueueTable";

                    da = new SqlDataAdapter(getorders, conn);
                    DataTable table = new DataTable();
                    da.Fill(table);


                    ListViewOrderNumber.Items.Clear();
                    if (table != null && table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            DataRow dr = table.Rows[i];
                            ListViewItem lv = new ListViewItem(dr[0].ToString());
                            lv.SubItems.Add(dr[1].ToString());
                            ListViewOrderNumber.Items.Add(lv);

                            object deviceid = dr[0], ordernumber = dr[1];
                            if (deviceid == DBNull.Value && ordernumber == DBNull.Value)
                            {
                                lblCurrentlyServing.Text = "0";
                            }
                            else
                            {
                                lblCurrentlyServing.Text = dr[0].ToString();
                                conn.Close();
                                dGV();
                            }
                        }
                    }
                    else
                    {
                        lblCurrentlyServing.Text = "0";
                        Clear();
                    }
                }
            }
        }

        private void dGV() //For displaying ordered food in a DataGridView
        {
            int TableNumber = Convert.ToInt32(lblCurrentlyServing.Text);
            if(iffrominvoice == false)
            {
                if (dGVOrders.InvokeRequired)
                {
                    Database();
                    String gettax = "Select * from DefaultTaxTable";
                    cmd = new SqlCommand(gettax, conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    lblTax.Invoke(new Action(() => lblTax.Text = dr["FoodMenuTax"].ToString()));
                    dr.Close();
                    conn.Close();

                    String getorders = "Select Item, Quantity, Price, PriceAmount, OrderType from QueueTable where TableNumber='" + TableNumber + "'";
                    da = new SqlDataAdapter(getorders, conn);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dGVOrders.Invoke(new Action(() => dGVOrders.Rows.Clear()));
                    foreach (DataRow item in dt.Rows)
                    {
                        dGVOrders.Invoke(new Action(() =>
                        {
                            Database();
                            int n = dGVOrders.Rows.Add();
                            dGVOrders.Rows[n].Cells[0].Value = item["Item"].ToString();
                            dGVOrders.Rows[n].Cells[1].Value = item["Quantity"].ToString();
                            dGVOrders.Rows[n].Cells[2].Value = item["Price"].ToString();
                            dGVOrders.Rows[n].Cells[3].Value = item["PriceAmount"].ToString();
                            lblOrderCommand.Text = item["OrderType"].ToString();
                        }));

                        conn.Close(); 
                    }
                    decimal[] columnVatableSales = new decimal[dGVOrders.Rows.Count];
                    columnVatableSales = (from DataGridViewRow row in dGVOrders.Rows
                                          where row.Cells[3].FormattedValue.ToString() != string.Empty
                                          select Convert.ToDecimal(row.Cells[3].FormattedValue)).ToArray();
                    lblTotalDue.Invoke(new Action(() => lblTotalDue.Text = columnVatableSales.Sum().ToString()));

                    totaldue = columnVatableSales.Sum();

                    string pesosubtotal, pesototal, pesotax;
                    decimal vatablesales, total, tax, vat, taxvat;
                    pesosubtotal = lblVatableSales.Text;
                    pesosubtotal = pesosubtotal.Replace("P", string.Empty);
                    pesototal = lblTotalDue.Text;
                    pesototal = pesototal.Replace("P", string.Empty);
                    pesotax = lblTax.Text;
                    pesotax = pesotax.Replace("P", string.Empty);

                    vatablesales = Convert.ToDecimal(pesosubtotal);
                    total = Convert.ToDecimal(pesototal);
                    tax = Convert.ToDecimal(pesotax);

                    taxvat = 1 + tax;
                    vatablesales = total / taxvat;

                    vat = vatablesales * tax;

                    lblVatableSales.Invoke(new Action(() => lblVatableSales.Text = Convert.ToString("P" + Math.Round(vatablesales, 2))));
                    lblTax.Invoke(new Action(() => lblTax.Text = Convert.ToString("P" + Math.Round(vat, 2))));
                    ordercommand = lblOrderCommand.Text;

                    ReceiptNumberFormat();
                }
                else
                {
                    Database();
                    String gettax = "Select * from DefaultTaxTable";
                    cmd = new SqlCommand(gettax, conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    lblTax.Text = dr["FoodMenuTax"].ToString();
                    dr.Close();
                    conn.Close();

                    String getorders = "Select Item, Quantity, Price, PriceAmount, OrderType from QueueTable where TableNumber='" + TableNumber + "'";
                    da = new SqlDataAdapter(getorders, conn);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dGVOrders.Rows.Clear();
                    foreach (DataRow item in dt.Rows)
                    {
                        Database();
                        int n = dGVOrders.Rows.Add();
                        dGVOrders.Rows[n].Cells[0].Value = item["Item"].ToString();
                        dGVOrders.Rows[n].Cells[1].Value = item["Quantity"].ToString();
                        dGVOrders.Rows[n].Cells[2].Value = item["Price"].ToString();
                        dGVOrders.Rows[n].Cells[3].Value = item["PriceAmount"].ToString();
                        lblOrderCommand.Text = item["OrderType"].ToString();
                    }
                    conn.Close();

                    decimal[] columnVatableSales = new decimal[dGVOrders.Rows.Count];
                    columnVatableSales = (from DataGridViewRow row in dGVOrders.Rows
                                          where row.Cells[3].FormattedValue.ToString() != string.Empty
                                          select Convert.ToDecimal(row.Cells[3].FormattedValue)).ToArray();
                    lblTotalDue.Text = columnVatableSales.Sum().ToString();

                    totaldue = columnVatableSales.Sum();

                    string pesosubtotal, pesototal, pesotax;
                    decimal vatablesales, total, tax, vat, taxvat;
                    pesosubtotal = lblVatableSales.Text;
                    pesosubtotal = pesosubtotal.Replace("P", string.Empty);
                    pesototal = lblTotalDue.Text;
                    pesototal = pesototal.Replace("P", string.Empty);
                    pesotax = lblTax.Text;
                    pesotax = pesotax.Replace("P", string.Empty);

                    vatablesales = Convert.ToDecimal(pesosubtotal);
                    total = Convert.ToDecimal(pesototal);
                    tax = Convert.ToDecimal(pesotax);

                    taxvat = 1 + tax;
                    vatablesales = total / taxvat;

                    vat = vatablesales * tax;

                    lblVatableSales.Text = Convert.ToString("P" + Math.Round(vatablesales, 2));
                    lblTax.Text = Convert.ToString("P" + Math.Round(vat, 2));
                    ordercommand = lblOrderCommand.Text;

                    ReceiptNumberFormat();
                }
            }
        }
        public void toolstripbuttons() //toolstripmenu buttons for easy searching of food category
        {
            tStripCategory.Items.Clear();
            try
            {
                Database();
                String selectcategory = "Select * from FoodCategory";
                da = new SqlDataAdapter(selectcategory, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ToolStripButton filterbutton = new ToolStripButton();
                    filterbutton.AutoSize = false;
                    filterbutton.Width = 100;
                    filterbutton.Name = "btn" + dt.Rows[i][0].ToString();
                    filterbutton.Text = dt.Rows[i][0].ToString();
                    filterbutton.TextImageRelation = TextImageRelation.ImageAboveText;
                    System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
                    byte[] photo_array = (byte[])dt.Rows[i][1];
                    Image img = (Image)converter.ConvertFrom(photo_array);
                    MemoryStream ms = new MemoryStream();
                    ms.Position = 0;
                    ms.Read(photo_array, 0, photo_array.Length);
                    filterbutton.Image = img;
                    filterbutton.ImageScaling = ToolStripItemImageScaling.SizeToFit;
                    filterbutton.Dock = DockStyle.Left;
                    tStripCategory.Items.Add(filterbutton);
                }
            }
            catch
            {

            }
        }

        public void dbchangenotification(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency dependency = sender as SqlDependency;
            dependency.OnChange -= dbchangenotification;
            if (e.Info == SqlNotificationInfo.Insert)
            {
                SqlDetect();
                refresh();
            }
            else if (e.Info == SqlNotificationInfo.Delete)
            {
                SqlDetect();
                refresh();
            }
            else if (e.Info == SqlNotificationInfo.Update)
            {
                //dGVOrders.Invoke(new Action(() => dGVOrders.Rows.Clear()));
                //dependency.OnChange += dbchangenotification;
                SqlDetect();
                refresh();
                //dGV();
            }
        }
        public void dbdiscountchange(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency dependency = sender as SqlDependency;
            dependency.OnChange -= dbchangenotification;
            if (e.Info == SqlNotificationInfo.Insert)
            {
                SqlDetectDiscounts();
                refreshdiscount();
            }
            else if (e.Info == SqlNotificationInfo.Delete)
            {
                SqlDetectDiscounts();
                refreshdiscount();
            }
            else if (e.Info == SqlNotificationInfo.Update)
            {
                SqlDetectDiscounts();
                refreshdiscount();
            }
        }

        private void refreshdiscount()
        {
            Database();
            if(conn.State == ConnectionState.Closed)
            {
                Database();
            }
            String getspecialdiscounttax = "Select * from PromoTable";

            using (da = new SqlDataAdapter(getspecialdiscounttax, conn))
            {
                DataTable dt = new DataTable();

                da.Fill(dt);

                dGVDiscount.Rows.Clear();

                foreach (DataRow item in dt.Rows)
                {
                    int n = dGVDiscount.Rows.Add();

                    dGVDiscount.Rows[n].Cells[0].Value = item[0].ToString();
                    dGVDiscount.Rows[n].Cells[1].Value = item[2].ToString();
                    dGVDiscount.Rows[n].Cells[2].Value = item[1].ToString();
                }
            }
            conn.Close();

        }
        //End of methods

        private void btnRemoveDiscounts_Click(object sender, EventArgs e)
        {
            lblVATExemptSales.Text = "0";
            lblSpecialDiscount.Text = "0";

            //decimal[] columnTax = new decimal[dGVOrders.Rows.Count];
            //columnTax = (from DataGridViewRow row in dGVOrders.Rows
            //             where row.Cells[4].FormattedValue.ToString() != string.Empty
            //             select Convert.ToDecimal(row.Cells[4].FormattedValue)).ToArray();
            //lblTax.Text = columnTax.Sum().ToString();
            string gettax = "Select FoodMenuTax from DefaultTaxTable";
            Database();
            using(cmd = new SqlCommand(gettax, conn))
            {
                dr = cmd.ExecuteReader();
                dr.Read();
                lblTax.Text = "P"+dr["FoodMenuTax"].ToString();
                dr.Close();
                conn.Close();
            }
            iftax = true;
            ifdiscount = false;
            ComputeTotalDue();
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            Database();
            String getseniorcitizenpwddiscount = "Select * from SeniorCitizensPWDsDiscountTable";
            using(cmd = new SqlCommand(getseniorcitizenpwddiscount ,conn))
            {
                dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    lblVATExemptSales.Text = Convert.ToString("P" + dr[1].ToString());
                    string discountpesosign = lblVATExemptSales.Text;
                    string removediscountpesosign = discountpesosign.Replace("P", string.Empty);
                    seniorcitizenandpwddiscount = Convert.ToDecimal(removediscountpesosign);
                    iftax = false;
                    ifdiscount = true;
                    dr.Close();
                    conn.Close();
                    ComputeTotalDue();
                }
            }
        }

        private void lblCurrentlyServing_TextChanged(object sender, EventArgs e) //For showing the orders of the customer in a
        {
            //Database();                                                              //dataGridView
            //if (dGVOrders.InvokeRequired)
            //{
                
            //    if (conn.State == ConnectionState.Closed)
            //    {
            //        conn.Open();
            //    }
            //    String getTax = "Select FoodMenuTax from DefaultTaxTable";
            //    cmd = new SqlCommand(getTax, conn);
            //    dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        lblTax.Invoke(new Action(() =>lblTax.Text = Convert.ToString(dr[0].ToString())));
            //        //numbertax = Convert.ToDecimal(dr["FoodMenuTax"].ToString());
            //        //finaltax = taxsubtotal * numbertax;
            //        //decimal decimaltax = Math.Round(finaltax, 2);
            //        //tax = Convert.ToString(decimaltax);
            //        //lblTax.Text = "P" + tax;
            //        //totalamountdue = Math.Round(finaltax,2);
            //        //lblTotalDue.Text = Convert.ToString("P" + totalamountdue);
            //    }
            //    dr.Close();
            //}
            //else
            //{
            //    if(conn.State == ConnectionState.Closed)
            //    {
            //        conn.Open();
            //    }
            //    String getTax = "Select FoodMenuTax from DefaultTaxTable";
            //    cmd = new SqlCommand(getTax, conn);
            //    dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        lblTax.Invoke(new Action(() => lblTax.Text = Convert.ToString(dr[0].ToString())));
            //    }
            //    dr.Close();
            //}
            
        }

        private void btnSearch_Click(object sender, EventArgs e) //For searching for menu items to add to the order list
        {
            Database();
            String foodsearch = txtbxSearchItems.Text;
            String searchitems = "Select * from FoodMenu where FoodName LIKE '%" + foodsearch + "%'";

            if (txtbxSearchItems.Text != "")
            {
                fLPSearchResult.Controls.Clear();
                fLPSearchResult.SuspendLayout();
                da = new SqlDataAdapter(searchitems, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
                    byte[] photo_array = (byte[])dt.Rows[i][7];
                    Image img = (Image)converter.ConvertFrom(photo_array);
                    MemoryStream ms = new MemoryStream();
                    ms.Position = 0;
                    ms.Read(photo_array, 0, photo_array.Length);
                    pictureBox = new PictureBox();
                    pictureBox.Image = img;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.Height = 150;
                    pictureBox.Width = 150;
                    pictureBox.Tag = i;

                    foodid = new Label();
                    foodid.Text = dt.Rows[i][0].ToString();
                    foodid.Tag = i;
                    foodid.Name = dt.Rows[i][0].ToString();

                    Label foodname = new Label();
                    foodname.Text = dt.Rows[i][1].ToString();
                    foodname.AutoSize = true;
                    foodname.ForeColor = Color.White;
                    oldflpSearchResultorderitem = foodname.Text;
                    foodid.Visible = false;

                    Label foodprice = new Label();
                    decimal price;
                    price = Convert.ToDecimal(dt.Rows[i][4].ToString());
                    oldflpSearchResultsprice = Convert.ToDecimal(Math.Round(price, 2));
                    oldflpSearchResultspriceaddcurrencysign = Convert.ToString("P" + oldflpSearchResultsprice);
                    oldflpSearchResultspriceremovecurrencysign = oldflpSearchResultspriceaddcurrencysign.Replace("P", string.Empty);
                    oldflpSearchResultspricepricewithpesosign = Convert.ToDecimal(oldflpSearchResultspriceremovecurrencysign);
                    foodprice.Text = Convert.ToString("P"+ oldflpSearchResultspricepricewithpesosign);
                    foodprice.ForeColor = Color.White;
                    foodprice.Name = dt.Rows[i][0].ToString();
                    foodprice.Tag = "foodprice" + i;

                    Label foodcategory = new Label();
                    foodcategory.Text = dt.Rows[i][3].ToString();
                    foodcategory.Visible = false;


                    Label itemquantity = new Label();
                    itemquantity.Text = "Total quantity:";

                    NumericUpDown nupquantity = new NumericUpDown();
                    nupquantity.Name = dt.Rows[i][0].ToString();
                    nupquantity.Value = 1;
                    nupquantity.Maximum = 100;
                    nupquantity.Minimum = 1;
                    nupquantity.ForeColor = Color.Black;
                    nupquantity.ValueChanged += new EventHandler(nupquantity_Changed);
                    nupquantity.Tag = "foodprice" + i;

                    btnAdd = new Button();
                    btnAdd.Text = "Add item";
                    btnAdd.BackColor = Color.FromName("ControlLight");
                    btnAdd.Name = foodname.Text;
                    btnAdd.Width = 75;
                    btnAdd.Height = 25;
                    //btnAdd.Location = new Point(3, 0);
                    btnAdd.Tag = i;
                    btnAdd.Click += new EventHandler(btnAdd_Click);
                    btnAdd.Dock = DockStyle.Top;

                    //Button btnRemove = new Button();
                    //btnRemove.Text = "Remove";
                    //btnRemove.BackColor = Color.FromName("ControlLight");
                    //btnRemove.Name = foodid.Text;
                    //btnRemove.Width = 75;
                    //btnRemove.Height = 25;
                    //btnRemove.Location = new Point(77, 0);
                    //btnRemove.Tag = i;
                    //btnRemove.Click += new EventHandler(btnRemove_Click);

                   // Panel paneladd = new Panel();
                   // paneladd.Height = 25;
                   // paneladd.Width = 155;
                   // //paneladd.AutoSize = true;
                   //// paneladd.Controls.Add(nupquantity);
                   // paneladd.Controls.Add(btnAdd);
                   // //paneladd.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                   // paneladd.BackColor = SystemColors.ControlLight;
                    //paneladd.Dock = DockStyle.Fill;

                    //Panel panelquantity = new Panel();
                    //panelquantity.Height = 25;
                    //panelquantity.Width = 155;
                    //panelquantity.BackColor = SystemColors.ControlLight;
                    //panelquantity.Controls.Add(btnAdd);
                    //panel.Controls.Add(btnRemove);
                    //btnRemove.Dock = DockStyle.Right;
                    //panel.AutoSize = true;
                    //panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;


                    //pictureBox.Controls.Add(foodname);
                    //pictureBox.Controls.Add(foodprice);
                    foodprice.Dock = DockStyle.Fill;
                    foodname.Dock = DockStyle.Bottom;
                    //btnUpdate.Anchor = AnchorStyles;
                    //btnRemove.Dock = DockStyle.Bottom;

                    fLPSearchResult.Controls.Add(pictureBox);
                    fLPSearchResult.Controls.Add(foodname);
                    fLPSearchResult.Controls.Add(foodprice);
                    fLPSearchResult.Controls.Add(nupquantity);
                    fLPSearchResult.Controls.Add(btnAdd);
                    //fLPSearchResult.Controls.Add(foodid);
                    //fLPSearchResult.Controls.Add(foodcategory);
                    //btnUpdate.Dock = DockStyle.Bottom;
                }
                fLPSearchResult.ResumeLayout();
            }
            else
            {
                MessageBox.Show("No such food exists!");
                fLPSearchResult.Controls.Clear();
            }
        }

        private void nupquantity_Changed(object sender, EventArgs e)
        {
            NumericUpDown quantity = sender as NumericUpDown;
            orderquantity = Convert.ToInt32(quantity.Value);
            foreach (Label lb in fLPSearchResult.Controls.OfType<Label>())
            {
                if (Convert.ToString(lb.Tag) == Convert.ToString(quantity.Tag))
                {
                    if (Convert.ToString(quantity.Value) != "")
                    {
                        decimal newprice = oldflpSearchResultspricepricewithpesosign;
                        oldflpSearchResultfinalprice = orderquantity * newprice;
                        lb.Text = Convert.ToString("P" + oldflpSearchResultfinalprice);
                    }
                    else
                    {

                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ordercmd = lblOrderCommand.Text;
            Button ID = sender as Button;
            String id;
            fLPSearchResult.SuspendLayout();
            DialogResult check = MessageBox.Show("Are you sure you want to add this menu item?", "Confirmation message...", MessageBoxButtons.YesNo);
            if (check == DialogResult.Yes)
            {
                foreach (Label lb in fLPSearchResult.Controls.OfType<Label>())
                {
                    if (lb.Text == ID.Name)
                    {
                        //id = lb.Name;
                        int TableNumber = Convert.ToInt32(lblCurrentlyServing.Text);
                        int OrderNumber = 0;
                        int TransactionNumber = 0;
                        Database();
                        String detectitem = "Select * from QueueTable where Item ='" + lb.Text + "' AND TableNumber='"+ TableNumber + "'";
                        String getallorders = "Select * from QueueTable where TableNumber ='" + TableNumber + "'";
                        //String getlastitem = "Select TOP(1) TempInvoiceVatTotal from CashierIDList where TempOrderNumber='" + OrderNumber + "' AND TempDeviceID='" + DeviceID + "'";
                        String updateitem = "Update QueueTable set Quantity=@Quantity, PriceAmount=@PriceAmount where Item ='" + lb.Text + "' AND TableNumber ='" + TableNumber + "'";
                        String insertitem = "Insert into QueueTable (TableNumber, OrderNumber, Item, Quantity, Price, PriceAmount, OrderType) VALUES (@TableNumber, @OrderNumber, @Item, @Quantity, @Price, @PriceAmount, @OrderType)";
                        using (cmd = new SqlCommand(detectitem, conn))
                        {
                            dr = cmd.ExecuteReader();
                            if(dr.Read())
                            {
                                using (cmd = new SqlCommand(getallorders, conn))
                                {
                                    priceamount = Convert.ToDecimal(dr["PriceAmount"].ToString());
                                    originalquantity = Convert.ToInt32(dr["Quantity"].ToString());
                                    ordercmd = lblOrderCommand.Text;
                                    TransactionNumber = Convert.ToInt32(dr["TransactionNumber"].ToString());
                                }
                                using (cmd = new SqlCommand(updateitem, conn))
                                {
                                    dr.Close();
                                    originalquantity = originalquantity + orderquantity;
                                    cmd.Parameters.AddWithValue("@Quantity", originalquantity);
                                    priceamount += oldflpSearchResultfinalprice;
                                    vatablesales += oldflpSearchResultfinalprice;
                                    cmd.Parameters.AddWithValue("@PriceAmount", priceamount);
                                    cmd.ExecuteNonQuery();

                                    dGV();
                                    MessageBox.Show("Item quantity has been updated");
                                }
                            }
                            else
                            {
                                dr.Close();
                                using (cmd = new SqlCommand(getallorders, conn))
                                {
                                    SqlDataReader dataRead;
                                    dataRead = cmd.ExecuteReader();
                                    if(dataRead.Read())
                                    {
                                        OrderNumber = Convert.ToInt32(dataRead["OrderNumber"].ToString());
                                        vatablesales = Convert.ToDecimal(lblVatableSales.Text.Replace("P", string.Empty));
                                        TransactionNumber = Convert.ToInt32(dr["TransactionNumber"].ToString());
                                    }
                                    else
                                    {
                                        MessageBox.Show("No such data exists!");
                                    }

                                    using (cmd = new SqlCommand(insertitem, conn))
                                    {
                                        dataRead.Close();
                                        cmd.Parameters.AddWithValue("@TableNumber", TableNumber);
                                        cmd.Parameters.AddWithValue("@OrderNumber", OrderNumber);
                                        cmd.Parameters.AddWithValue("@Item", oldflpSearchResultorderitem);
                                        cmd.Parameters.AddWithValue("@Quantity", orderquantity);
                                        cmd.Parameters.AddWithValue("@Price", oldflpSearchResultsprice);
                                        cmd.Parameters.AddWithValue("@PriceAmount", oldflpSearchResultfinalprice);
                                        cmd.Parameters.AddWithValue("@OrderType", ordercmd);
                                        cmd.Parameters.AddWithValue("@TransactionNumber", TransactionNumber);
                                        cmd.ExecuteNonQuery();

                                        dGV();
                                        MessageBox.Show("Food is added");
                                    }
                                    conn.Close();
                                }
                            }
                        }
                    }
                }
            }
            else if (check == DialogResult.No)
            {

            }
            fLPSearchResult.ResumeLayout();
        }
        private void dGVOrders_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnProcessOrder_Click(object sender, EventArgs e)
        {
            SaveOrder();
        }

        private void CashierForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                if (pnlGetTransactionNumber.Visible == true)
                {

                }
                else
                {
                    Database();
                    cmd = new SqlCommand("Update LoginTable set LogoutTime=@LogoutTime where LoginID='" + lblCashierName.Text + "' AND LoginTime='" + logintime + "' AND LoginDate='" + logindate + "'", conn);
                    cmd.Parameters.AddWithValue("@LogoutTime", DateTime.Now.ToLongTimeString());
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    this.Hide();
                    LoginForm lfm = new LoginForm();
                    this.Hide();
                    lfm.Show();
                }
            }
        }

        private void dGVOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var sendergrid = (DataGridView)sender;
            
            if(sendergrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                rowdelete = e.RowIndex;
                voiditem = 0;
                pnlVoid.Show();
                txtCashierPassword.Focus();
            }
        }

        private void CashierForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SqlDependency.Stop(connectionstring);
        }

        private void btnVoidItem_Click(object sender, EventArgs e)
        {
            if(voiditem == 0)
            {
                voidsingleitem();
            }
            else if (voiditem == 1)
            {
                voidentireorder();
            }
        }

        private void btnHidePanel_Click(object sender, EventArgs e)
        {
            txtCashierPassword.Text = "";
            pnlVoid.Hide();
        }

        private void btnShowPanelMenu_Click(object sender, EventArgs e)
        {
            txtbxSearchItems.Text = "";
            fLPSearchResult.Controls.Clear();
            pnlSearchMenu.Visible = true;
        }
        private void btnExitDiscountPanel_Click(object sender, EventArgs e)
        {
            dGVDiscount.Rows.Clear();
            pnlDiscounts.Hide();
        }

        private void btnHolidayDiscounts_Click(object sender, EventArgs e)
        {
            refreshdiscount();
            pnlDiscounts.Visible = true;
        }

        private void dGVOrders_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double quantity = Convert.ToDouble(dGVOrders.CurrentRow.Cells[1].Value);
            double price = Convert.ToDouble(dGVOrders.CurrentRow.Cells[2].Value);
            double result = quantity * price;
            dGVOrders.CurrentRow.Cells[3].Value = result;
            Database();
            String updateitemprice = "Update QueueTable set Quantity=@Quantity, Price=@Price, PriceAmount = @PriceAmount where Item='" + dGVOrders.CurrentRow.Cells[0].Value.ToString() + "' AND TableNumber='"+Convert.ToInt32(lblCurrentlyServing.Text)+"'";
            using (cmd = new SqlCommand(updateitemprice, conn))
            {
                cmd.Parameters.AddWithValue("@Quantity", dGVOrders.CurrentRow.Cells[1].Value.ToString());
                cmd.Parameters.AddWithValue("@Price", dGVOrders.CurrentRow.Cells[2].Value.ToString());
                cmd.Parameters.AddWithValue("@PriceAmount", dGVOrders.CurrentRow.Cells[3].Value.ToString());
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void dGVDiscount_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lblSpecialDiscount.Text = dGVDiscount.CurrentRow.Cells[1].Value.ToString();
            ifdiscount = true;
            ComputeTotalDue();
        }

        private void btnVoidOrder_Click(object sender, EventArgs e)
        {
            if(dGVOrders.Rows.Count!=0)
            {
                voiditem = 1;
                pnlVoid.Show();
            }
        }

        private void btnComputePay_Click(object sender, EventArgs e)
        {
            if(NUPPayment.Value!= 0 && Convert.ToDecimal(lblTotalDue.Text.Replace("P", string.Empty))>0)
            {
                string removepesosigntotal;
                double result, payment, totaldue;
                removepesosigntotal = lblTotalDue.Text;
                removepesosigntotal = removepesosigntotal.Replace("P", string.Empty);
                payment = Convert.ToDouble(NUPPayment.Value); totaldue = Convert.ToDouble(removepesosigntotal);
                if (payment >= totaldue)
                {
                    result = totaldue - payment;
                    result = -1 * result;
                    lblChange.Text = result.ToString("P" + "###,###,##0.00");
                    btnProcessOrder.Enabled = true;
                }
                else
                {
                    result = totaldue - payment;
                    btnProcessOrder.Enabled = false;
                    MessageBox.Show("Payment is not enough! You are  short of: " + result);
                }
            }
            else
            {
                btnProcessOrder.Enabled = false;
                MessageBox.Show("Payment is zero/total due is less than or equals to zero", "Error");
            }
        }

        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            SaveOrder();
            btnProcessOrder.Enabled = false;
        }

        private void btnHideMenu_Click(object sender, EventArgs e)
        {
            txtbxSearchItems.Text = "";
            fLPSearchResult.Controls.Clear();
            pnlSearchMenu.Visible = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public int GetTransactionNumber()
        {
            string selectdefaultreferencenumber = @"Select * from dbo.OrderReferenceNumber";
            string selectinvoicereferencenumber = @"Select InvoiceTransactionNumber from dbo.InvoiceTable where InvoiceTransactionNumber= (SELECT MAX (InvoiceTransactionNumber) from dbo.InvoiceTable)";
            string selectqueuereferencenumber = @"Select TransactionNumber from dbo.QueueTable where TransactionNumber= (SELECT MAX(TransactionNumber) from dbo.QueueTable)";

            int defaulttransacationnumber = 0, queuetabletransactionnumber = 0, invoicetransactionnumber = 0, currenttransactionnumber = 0;

            Database();
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
                if (dr.Read())
                {
                    invoicetransactionnumber = Convert.ToInt32(dr["InvoiceTransactionNumber"].ToString());
                    invoicetransactionnumber++;
                }
                dr.Close();
            }
            using (cmd = new SqlCommand(selectqueuereferencenumber, conn))
            {
                dr = cmd.ExecuteReader();
                if (dr.Read())
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
            return currenttransactionnumber;
        }

        private void txtTransactionNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Database();
                string GetCustomerOrder = "Select * from InvoiceTable where InvoiceTransactionNumber = '" + txtTransactionNumber.Text + "' AND InvoiceVoided IS NULL";
                cmd = new SqlCommand(GetCustomerOrder, conn);
                DataTable dt = new DataTable();
                dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    lblCurrentlyServing.Text = Convert.ToString(dr["InvoiceTableNumber"].ToString());
                    dr.Close();
                    da = new SqlDataAdapter(GetCustomerOrder, conn);
                    da.Fill(dt);
                    dGVOrders.Rows.Clear();

                    foreach(DataRow item in dt.Rows)
                    {
                        int n = dGVOrders.Rows.Add();
                        dGVOrders.Rows[n].Cells[0].Value = item["InvoiceItem"].ToString();
                        dGVOrders.Rows[n].Cells[1].Value = item["InvoiceQuantity"].ToString();
                        dGVOrders.Rows[n].Cells[2].Value = item["InvoicePrice"].ToString();
                        dGVOrders.Rows[n].Cells[3].Value = item["InvoicePriceAmount"].ToString();
                        lblVatableSales.Text = item["InvoiceVatableSales"].ToString();
                        lblTax.Text = item["InvoiceTaxTotal"].ToString();
                        lblVATExemptSales.Text = item["InvoiceSeniorCitizenPWDDiscount"].ToString();
                        lblSpecialDiscount.Text = item["InvoiceHolidayDiscount"].ToString();
                        lblTotalDue.Text = item["InvoiceTotal"].ToString();
                        NUPPayment.Value = Convert.ToDecimal(item["InvoicePayment"].ToString());
                        lblChange.Text = item["InvoiceChange"].ToString();
                        lblOrderCommand.Text = item["InvoiceOrderCommand"].ToString();
                        TransactionNumber.transactionnumber = Convert.ToInt32(item["InvoiceTransactionNumber"].ToString());

                        ordercommand = lblOrderCommand.Text;

                        ReceiptNumberFormat();

                    }
                    decimal[] columnVatableSales = new decimal[dGVOrders.Rows.Count];
                    columnVatableSales = (from DataGridViewRow row in dGVOrders.Rows
                                          where row.Cells[3].FormattedValue.ToString() != string.Empty
                                          select Convert.ToDecimal(row.Cells[3].FormattedValue)).ToArray();
                    lblTotalDue.Text = columnVatableSales.Sum().ToString();

                    totaldue = columnVatableSales.Sum();

                    string pesosubtotal, pesototal, pesotax, pesodiscount, pesopromo;
                    decimal vatablesales, total, tax, vat, taxvat, discount, promo;
                    pesosubtotal = lblVatableSales.Text;
                    pesosubtotal = pesosubtotal.Replace("P", string.Empty);
                    pesototal = lblTotalDue.Text;
                    pesototal = pesototal.Replace("P", string.Empty);
                    pesotax = lblTax.Text;
                    pesotax = pesotax.Replace("P", string.Empty);
                    pesodiscount = lblVATExemptSales.Text.Replace("P", string.Empty);
                    pesopromo = lblSpecialDiscount.Text.Replace("P", string.Empty);

                    vatablesales = Convert.ToDecimal(pesosubtotal);
                    total = Convert.ToDecimal(pesototal);
                    tax = Convert.ToDecimal(pesotax);
                    discount = Convert.ToDecimal(pesodiscount);
                    promo = Convert.ToDecimal(pesopromo);

                    taxvat = 1 + Convert.ToDecimal(0.12);
                    vatablesales = total / taxvat;

                    vat = vatablesales * Convert.ToDecimal(0.12);

                    lblVatableSales.Text = Convert.ToString("P" + Math.Round(vatablesales, 2));
                    lblTax.Text = Convert.ToString("P" + Math.Round(vat, 2));
                    total = vatablesales + vat - promo - discount;
                    lblTotalDue.Text = Convert.ToString("P"+Math.Round(total, 2));
                    iffrominvoice = true;
                    pnlGetTransactionNumber.Visible = false;
                    txtTransactionNumber.Text = "";
                }
                else
                {
                    MessageBox.Show("No such record exists!");
                }
                dr.Close();
            }
            if(e.KeyCode == Keys.Escape)
            {
                pnlGetTransactionNumber.Visible = false;
                txtTransactionNumber.Text = "";
            }
        }

        private void lblGetTransactionNumber_Click(object sender, EventArgs e)
        {
            pnlGetTransactionNumber.Visible = true;
        }

        private void CustomerInvoiceRefresh()
        {
            Database();
            string GetCustomerOrder = "Select * from InvoiceTable where InvoiceTransactionNumber = '" + TransactionNumber.transactionnumber + "' AND InvoiceVoided IS NULL";
            cmd = new SqlCommand(GetCustomerOrder, conn);
            DataTable dt = new DataTable();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lblCurrentlyServing.Text = Convert.ToString(dr["InvoiceTableNumber"].ToString());
                dr.Close();
                da = new SqlDataAdapter(GetCustomerOrder, conn);
                da.Fill(dt);
                dGVOrders.Rows.Clear();

                foreach (DataRow item in dt.Rows)
                {
                    int n = dGVOrders.Rows.Add();
                    dGVOrders.Rows[n].Cells[0].Value = item["InvoiceItem"].ToString();
                    dGVOrders.Rows[n].Cells[1].Value = item["InvoiceQuantity"].ToString();
                    dGVOrders.Rows[n].Cells[2].Value = item["InvoicePrice"].ToString();
                    dGVOrders.Rows[n].Cells[3].Value = item["InvoicePriceAmount"].ToString();
                    lblVatableSales.Text = item["InvoiceVatableSales"].ToString();
                    lblTax.Text = item["InvoiceTaxTotal"].ToString();
                    lblVATExemptSales.Text = item["InvoiceSeniorCitizenPWDDiscount"].ToString();
                    lblSpecialDiscount.Text = item["InvoiceHolidayDiscount"].ToString();
                    lblTotalDue.Text = item["InvoiceTotal"].ToString();
                    NUPPayment.Value = Convert.ToDecimal(item["InvoicePayment"].ToString());
                    lblChange.Text = item["InvoiceChange"].ToString();
                    lblOrderCommand.Text = item["InvoiceOrderCommand"].ToString();
                    TransactionNumber.transactionnumber = Convert.ToInt32(item["InvoiceTransactionNumber"].ToString());

                    ordercommand = lblOrderCommand.Text;

                    ReceiptNumberFormat();

                }
                decimal[] columnVatableSales = new decimal[dGVOrders.Rows.Count];
                columnVatableSales = (from DataGridViewRow row in dGVOrders.Rows
                                      where row.Cells[3].FormattedValue.ToString() != string.Empty
                                      select Convert.ToDecimal(row.Cells[3].FormattedValue)).ToArray();
                lblTotalDue.Text = columnVatableSales.Sum().ToString();

                totaldue = columnVatableSales.Sum();

                string pesosubtotal, pesototal, pesotax, pesodiscount, pesopromo;
                decimal vatablesales, total, tax, vat, taxvat, discount, promo, change, payment;
                pesosubtotal = lblVatableSales.Text;
                pesosubtotal = pesosubtotal.Replace("P", string.Empty);
                pesototal = lblTotalDue.Text;
                pesototal = pesototal.Replace("P", string.Empty);
                pesotax = lblTax.Text;
                pesotax = pesotax.Replace("P", string.Empty);
                pesodiscount = lblVATExemptSales.Text.Replace("P", string.Empty);
                pesopromo = lblSpecialDiscount.Text.Replace("P", string.Empty);

                vatablesales = Convert.ToDecimal(pesosubtotal);
                total = Convert.ToDecimal(pesototal);
                tax = Convert.ToDecimal(pesotax);
                discount = Convert.ToDecimal(pesodiscount);
                promo = Convert.ToDecimal(pesopromo);

                taxvat = 1 + Convert.ToDecimal(0.12);
                vatablesales = total / taxvat;

                vat = vatablesales * Convert.ToDecimal(0.12);

                lblVatableSales.Text = Convert.ToString("P" + Math.Round(vatablesales, 2));
                lblTax.Text = Convert.ToString("P" + Math.Round(vat, 2));
                total = vatablesales + vat - promo - discount;
                lblTotalDue.Text = Convert.ToString("P" + Math.Round(total, 2));
                //iffrominvoice = true;
                //pnlGetTransactionNumber.Visible = false;
                //txtTransactionNumber.Text = "";
            }
            conn.Close();
        }
    }
}
