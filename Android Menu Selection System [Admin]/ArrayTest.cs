using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Android_Menu_Selection_System__Admin_
{
    public partial class ArrayTest : Form
    {
        List<int>array;
        int addnumber;
        public ArrayTest()
        {
            InitializeComponent();
            array = new List<int>();
        }

        private void btnRemoveNumber_Click(object sender, EventArgs e)
        {
            array.RemoveAt(array.Count() - 1);
            listArray.Items.RemoveAt(listArray.Items.Count - 1);
        }

        private void btnAddNumber_Click(object sender, EventArgs e)
        {
            int add = 0;
            addnumber = Convert.ToInt32(txtAddNumber.Text);
            array.Add(addnumber);

            //int i;
            listArray.Items.Add(txtAddNumber.Text);
            ////array[i] = Convert.ToInt32(txtAddNumber.Text);
            //for(int i =0; i<array.Count; i++)
            //{
            //    addnumber++;
            //}
            //txtAddNumber.Focus();
        }

        private void btnGetMaxNumber_Click(object sender, EventArgs e)
        {
            //int[] array= new int[] { };

            //for(int i = 0; i<listArray.Items.Count; i++)
            //{
            //array[i] = listArray.Items.IndexOf(i);

            //}
            int maxnumber = 0;
            //for(int i =0; i<listArray.Items.Count; i++)
            //{
            //    //maxnumber += listArray.Items.IndexOf(item);
            //    //maxnumber += Convert.ToInt32(listArray.Items[i]);
            //    if (maxnumber < Convert.ToInt32(listArray.Items[i]))
            //    {
            //        maxnumber = Convert.ToInt32(listArray.Items[i]);
            //    }
            //}
            //for(int i =0; i<array.Count; i++)
            //{
            //    MessageBox.Show("Items: " + array[i].ToString());
            //}
            int allitems = 0;
            foreach(int item in array)
            {
                //allitems += item;
                if(allitems<item)
                {
                    allitems = item;
                }
            }
            MessageBox.Show("Items: " + Convert.ToString(allitems));
        }

        private void txtAddNumber_Click(object sender, EventArgs e)
        {
            txtAddNumber.Text = "";
        }
    }
}
