using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Exer1
{
    public partial class PracticeInterest : Form
    {
        public PracticeInterest()
        {
            InitializeComponent();
        }
      

        private void Calculate_Click(object sender, EventArgs e)
        {
            //h = Convert.ToDouble(Dateloansum.Text);
            
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please input amount");
            }
            else
            {
            double a, b, c, d, f1, f2;
            a = Convert.ToDouble(textBox1.Text);
            b = 0.08;
            c = 0.02;
            d = Convert.ToDouble(Dateloanmonths.Text);
            f1 = a;
            int loan = DuedateofLoan.Value.Month - DateofLoan.Value.Month;
            dateTimePicker1.Value = DateofLoan.Value.AddMonths(loan);
           
                int year = DuedateofLoan.Value.Year - DateofLoan.Value.Year;

                int add = year * 12;

                if (DateofLoan.Value.AddMonths(loan) <= DuedateofLoan.Value)
                {
                    MessageBox.Show("+1");
                    loan = loan + add;
                    Dateloanmonths.Text = loan.ToString();
                }
                else if (dateTimePicker1.Value.ToString() == DuedateofLoan.Value.ToString())
                {
                    loan = loan + add;
                    MessageBox.Show("+2");

                    Dateloanmonths.Text = loan.ToString();
                }
                else if (DateofLoan.Value.AddMonths(loan) > DuedateofLoan.Value)
                {
                    //loan = add+(loan - 1);
                    loan = 0;
                    //Dateloanmonths.Text = loan.ToString();
                    MessageBox.Show("Alon, Amino, Bedz, Buenbrazo, Mac, Moja, Salik is here!!! Za WARUDO!!!!!!! ORA ORA ORA ORA ORA ORA ORA ORA ORA ORA ORA ORA ORA!!!!!!! MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MUDA MU...DA!!!!!!....!!!!!!!");
                }
               

            

            //if (DuedateofLoan.Value.AddMonths(loan) > DateofLoan.Value)
                
            f2 = loan;
            if (loan == 0)
            {
                sum2.Text = "0";
            }
            else if (f2 > 2)
                {
                    f2--;
                    f1 = f1 + (f1 * b) + (f1 * b * c);
                    while (f2 > 0)
                    {
                        f2--;
                        f1 = f1 + (f1 * b);
                        sum2.Text = string.Format("{0:0.00}", f1);
                    }
                }        
                else if(f2<=2)
                {
                    f1 = a * b+ a/(f2);
                    sum2.Text = f1.ToString();
                }
            }
        }

        private void PracticeInterest_Load(object sender, EventArgs e)
        {
            Interest.Text = "0.08";
            Penalty.Text = "0.02";
            textBox1.Text = "";
            Dateloanmonths.Text = "0";
            Dateloansum.Text = "0";
        }

        private void DateofLoan_ValueChanged(object sender, EventArgs e)
        {
        }

        //private void DuedateofLoan_ValueChanged(object sender, EventArgs e)
        //{
        //    int loan = DuedateofLoan.Value.Month - DateofLoan.Value.Month;
        //    if (DuedateofLoan.Value.AddMonths(loan) > DateofLoan.Value)
        //        //loan--; 
        //        Dateloanmonths.Text = loan.ToString();
        //}

        private void clear()
        {
            textBox1.Text = "";

        }
    }
}
