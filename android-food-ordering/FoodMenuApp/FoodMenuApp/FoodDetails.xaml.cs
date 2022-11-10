using FoodMenuApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodMenuApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodDetails : ContentPage
    {
        private FoodOrder _FoodOrder;
        private FoordOrderList _FoodOrderList;
        private static BadgeCount _badgeCount = new BadgeCount();
        decimal foodprice;
        decimal foodamount;
        decimal foodcalculate;
        string amountvalue;
        decimal? foodorderprice;
        byte[] foodpic;
        int Count = 0;
        int newcount;
        //double txtfoodamount;
        //string txtid, txtfoodname, txtfoodprice, txtfoodcategory, txtamountvalue;
        //public FoodDetails(Image pic, string id, string name, string price, string category)
        public  FoodDetails(byte[] pic, string id, string name, decimal? price, string category)
        {
            InitializeComponent();
            //CheckMessage();
            foodpic = pic;
            foodorderprice = price;
            ImgFoodPic.Source = ImageSource.FromStream(() => new MemoryStream(pic));
            //double? foodprice = Convert.ToDouble(txtFoodPrice.Text);
            txtFoodID.Text = id;
            txtFoodName.Text = name;
            txtFoodPrice.Text = Convert.ToString("P"+price);
            txtFoodCategory.Text = category;
            amountvalue = txtFoodPrice.Text;
            string replace = amountvalue.Replace("P", string.Empty);
            foodprice = Convert.ToDecimal(replace);
        }

        //private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        //{
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{BadgeCount.badgecount}", Color.Red, Color.White);
        }

        //public async void CheckMessage()
        //{
        //    await Task.Run(() =>
        //    {
        //        MessagingCenter.Subscribe<object, string>(this, "BadgeCount", async (sender, args) =>
        //        {
        //            //await DisplayAlert("Message", "BadgeCount" + args, "Ok");
        //            await Task.Delay(500);
        //            //DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{args}", Color.Red, Color.White);
        //            newcount = Convert.ToInt32(args);
        //            BadgeCount.badgecount = newcount;
        //        });
        //        //MessagingCenter.Unsubscribe<object, string>(this, "BadgeCount");
        //    });
        //}

        public int badgecount()
        {
            int count = newcount;
            return count;
        }
        private void OnCustomEntry_Changed(object sender, TextChangedEventArgs e)
        {
            if (txtFoodAmount.Text != "" && txtFoodAmount.Text != "0")
            {
                foodamount = Convert.ToDecimal(txtFoodAmount.Text);
                foodcalculate = foodamount * foodprice;
                txtFoodPrice.Text = Convert.ToString("P" + foodcalculate);
                BtnOrderFood.IsEnabled = true;
            }
            else
            {
                txtFoodPrice.Text = Convert.ToString("P" + foodprice);
                BtnOrderFood.IsEnabled = false;
            }
        }

        private async void BtnOrderFood_Clicked(object sender, EventArgs e)
        {
            string pricevalue = txtFoodPrice.Text;
            string removePrice = pricevalue.Replace("P", string.Empty);
            var cartlist = Foodcart.Instance;
            var cartitem = cartlist.Any(a => a.FoodOrderItem==txtFoodName.Text);

            if (cartitem)
            {
                foreach(var item in cartlist.Where(a => a.FoodOrderItem==txtFoodName.Text))
                {
                    item.FoodOrderQuantity += Convert.ToInt32(txtFoodAmount.Text);
                    item.FoodOrderPriceAmount += Convert.ToDecimal(removePrice);
                    await DisplayAlert("Confirmation Message", txtFoodName.Text + " quantity added " +"Current quantity: "+item.FoodOrderQuantity, "Ok");
                }
                //await DisplayAlert("Confirmation Message", "Yes data", "Ok");
            }
            else
            {
                _FoodOrder = new FoodOrder();
                _FoodOrder.FoodOrderPic = foodpic;
                _FoodOrder.FoodOrderItem = txtFoodName.Text;
                _FoodOrder.FoodOrderQuantity = Convert.ToInt32(txtFoodAmount.Text);
                _FoodOrder.FoodOrderPrice = Convert.ToDecimal(foodorderprice);
                _FoodOrder.FoodOrderPriceAmount = Convert.ToDecimal(removePrice);
                cartlist.Add(_FoodOrder);
                await DisplayAlert("Confirmation Message", txtFoodName.Text+" added to order list", "Ok");
                if (ToolbarItems.Count > 0)
                {
                    //Count = badgecount();
                    //if (Count > 0)
                    //{
                    //    Count++;
                    //    DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{Count}", Color.Red, Color.White);
                    //    MessagingCenter.Send<object, string>(this, "BadgeCount", $"{Count}");
                    //}
                    //else
                    //{
                    if (BadgeCount.badgecount >= 0)
                    {
                        BadgeCount.badgecount += 1;
                        DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{BadgeCount.badgecount}", Color.Red, Color.White);
                        //MessagingCenter.Send<object, string>(this, "BadgeCount", $"{BadgeCount.badgecount}");
                    }
                    else
                    {
                        BadgeCount.badgecount += 1;
                        DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{BadgeCount.badgecount}", Color.Red, Color.White);
                    }
                    //}
                    //var badgecount = _badgeCount.count;
                }
            }
            //await DisplayAlert("Confirmation Message", "No data", "Ok");
        }
            //foreach(var item in cartlist)
            //{
            //    if(item != null)
            //    {

            //        else if (item.FoodOrderItem != txtFoodName.Text)
            //        {

        //    }
        //    else
        //    {
        //        Count++;
        //        _FoodOrder = new FoodOrder();
        //        _FoodOrder.FoodOrderPic = foodpic;
        //        _FoodOrder.FoodOrderItem = txtFoodName.Text;
        //        _FoodOrder.FoodOrderQuantity = Convert.ToInt32(txtFoodAmount.Text);
        //        _FoodOrder.FoodOrderPrice = Convert.ToDecimal(foodorderprice);
        //        _FoodOrder.FoodOrderPriceAmount = Convert.ToDecimal(removePrice);
        //        cartlist.Add(_FoodOrder);
        //        if (ToolbarItems.Count > 0)
        //        {
        //            _badgeCount = new BadgeCount();
        //            _badgeCount.count = Count;
        //            DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{_badgeCount.count}", Color.Red, Color.White);
        //        }
        //    }

        //}
        //Count++; _badgeCount = new BadgeCount();
        //_badgeCount.count = Count;
        //DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{_badgeCount.count}", Color.Red, Color.White);

        //protected override bool OnBackButtonPressed()
        //{

        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        if (await DisplayAlert("Exit?", "Are you sure you want to exit from this page?", "Yes", "No"))
        //        {
        //            base.OnBackButtonPressed();
        //            await Navigation.PopAsync(false);
        //            await Navigation.PopModalAsync(true);
        //        }
        //    });

        //    return true;
        //}
    }
}