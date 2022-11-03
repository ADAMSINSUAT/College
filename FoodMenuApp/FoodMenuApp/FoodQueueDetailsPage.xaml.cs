using FoodMenuApp.Foodcartmodel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodMenuApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodQueueDetailsPage : ContentPage
    {
        byte[] queuepic;
        string queuename;
        int queuequantity;
        decimal queuesrp;
        decimal queueamount;
        string queuepesosrp, queuepesoamount;

        int foodquantity;
        decimal foodcalculate, foodprice;
        public FoodQueueDetailsPage(byte[] pic, string item, int? quantity, decimal? srp, decimal? amount)
        {
            InitializeComponent();

            queuepic = pic;
            ImgFoodPic.Source = ImageSource.FromStream(() => new MemoryStream(pic));

            queuename = item;
            txtFoodName.Text = Convert.ToString(queuename);

            queuequantity = Convert.ToInt32(quantity);
            txtFoodQuantity.Text = Convert.ToString(queuequantity);

            queuesrp = Convert.ToDecimal(srp);
            foodprice = queuesrp;
            queuepesosrp = "P" + queuesrp;
            txtFoodPrice.Text = queuepesosrp;

            queueamount = Convert.ToDecimal(amount);
            queuepesoamount = "P" + queueamount;
            txtPriceAmount.Text = queuepesoamount;
        }

        private void txtFoodQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtFoodQuantity.Text != "" && txtFoodQuantity.Text != "0")
            {
                foodquantity = Convert.ToInt32(txtFoodQuantity.Text); //Decimal holder for quantity
                foodcalculate = foodprice * foodquantity;
                txtPriceAmount.Text = Convert.ToString("P" + Convert.ToDecimal(foodcalculate));
                BtnChangeQuantity.IsEnabled = true;
                BtnChangeQuantity.IsEnabled = true;

            }
            else if (txtFoodQuantity.Text == "")
            {
                txtPriceAmount.Text = Convert.ToString("P" + queueamount);
                BtnChangeQuantity.IsEnabled = false;
            }
            else if (e.OldTextValue == e.NewTextValue)
            {
                BtnChangeQuantity.IsEnabled = false;
            }
        }

        private async void BtnChangeQuantity_Clicked(object sender, EventArgs e)
        {
            var queuelist = QueueOrder.Instance;

            foreach(var item in queuelist.Where(a=> a.FoodOrderItem == txtFoodName.Text))
            {
                item.FoodOrderPriceAmount = foodcalculate;
                item.FoodOrderQuantity = Convert.ToInt32(txtFoodQuantity.Text);
            }
            await DisplayAlert("Confirmation Message", "Item quantity has been updated", "Ok");
            await Navigation.PopModalAsync();
        }

        private async void SwipeGestureRecognizer_Swiped_Down(object sender, SwipedEventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}