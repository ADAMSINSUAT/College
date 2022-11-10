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
    public partial class FoodOrderedDetails : ContentPage
    {
        decimal foodprice;
        decimal foodamount;
        decimal foodcalculate;
        string amountvalue;
        string newamountvalue;
        string replacepriceamount;
        string replace;
        decimal? foodorderprice;
        decimal? foodorderpriceamount;
        decimal newprice;
        decimal finalprice;
        int? foodquantity;
        byte[] foodpic;
        int Count = 0;
        int newcount;
        public FoodOrderedDetails(byte[] pic, string name, string category, decimal? price, decimal? priceamount, int? quantity)
        {
            InitializeComponent();
            //CheckMessage();
            txtFoodQuantity.Text = Convert.ToString(quantity);
            foodpic = pic; //Byte holder for Food picture
            foodorderprice = price; //Decimal holder for Food price

            foodorderpriceamount = priceamount; //Decimal holder for Food price amount
            foodquantity = quantity; //Int holder for Food quantity

            ImgFoodPic.Source = ImageSource.FromStream(() => new MemoryStream(pic)); //Food picture
            txtFoodName.Text = name; //Food name
            txtOrderedCategory.Text = category; //Food category

            //Default price
            txtFoodPrice.Text = Convert.ToString("P" + foodorderprice);
            amountvalue = txtFoodPrice.Text;
            replace = amountvalue.Replace("P", string.Empty);
            foodprice = Convert.ToDecimal(replace);
            //\Default price

            //Price amount
            txtPriceAmount.Text = Convert.ToString("P" + foodorderpriceamount);
            newamountvalue = txtPriceAmount.Text;
            replacepriceamount = newamountvalue.Replace("P", string.Empty);
            newprice = Convert.ToDecimal(replacepriceamount);
            //finalprice = Convert.ToDecimal(replace);
            //\Price amount
            //stepper.Value = Convert.ToDouble(quantity);
        }

        private async void BtnChangeQuantity_Clicked(object sender, EventArgs e)
        {
            var cartlist = Foodcart.Instance;
            //var cartlistitem = cartlist.Where(a => a.FoodOrderItem == txtFoodName.Text);
            //var item = cartlist.Where(a => a.FoodOrderItem == txtFoodName.Text);
            foreach (var item in cartlist.Where(a => a.FoodOrderItem == txtFoodName.Text))
            {
                item.FoodOrderPriceAmount = foodcalculate;
                item.FoodOrderQuantity = Convert.ToInt32(txtFoodQuantity.Text);
            }
            await DisplayAlert("Confirmation Message", "Item quantity has been updated", "Ok");
            await Navigation.PopModalAsync();
        }

        private async void SwipeGestureRecognizer_DownSwiped(object sender, SwipedEventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void txtFoodQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtFoodQuantity.Text != "" && txtFoodQuantity.Text != "0")
            {
                foodamount = Convert.ToDecimal(txtFoodQuantity.Text); //Decimal holder for quantity
                foodcalculate = foodprice * foodamount;
                txtPriceAmount.Text = Convert.ToString("P" + Convert.ToDecimal(foodcalculate));
                BtnChangeQuantity.IsEnabled = true;
                //foodcalculate = foodamount * foodprice;
                //newprice += foodcalculate;
                //txtPriceAmount.Text = Convert.ToString("P" + newprice);
                BtnChangeQuantity.IsEnabled = true;

            }
            else if(txtFoodQuantity.Text=="")
            {
                txtPriceAmount.Text = Convert.ToString("P" + foodorderpriceamount);
                BtnChangeQuantity.IsEnabled = false;
            }
            else if(e.OldTextValue == e.NewTextValue)
            {
                BtnChangeQuantity.IsEnabled = false;
            }
        }
    }
}