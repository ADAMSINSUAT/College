using FoodMenuApp.Foodcartmodel;
using FoodMenuApp.Models;
using FoodMenuApp.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodMenuApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodQueuePage : ContentPage
    {
        PollWebApi customerordertimer;
        PollWebApi ordernnumbertimer;
        PollWebApi queuenumbertimer;
        QueueTable _queueTable;
        string getorderurl = "http://192.168.43.47:8082/api/GetCustomerOrder/";
        string updateurl = "http://192.168.43.47:8082/api/GetCustomerOrder";
        string ordernumberurl = "http://192.168.43.47:8082/api/GetCustomerOrder";
        string queueurl = "http://192.168.43.47:8082/api/OrderNumber/";
        string SignalrUrl = "http://localhost:8082/chatHub";
        bool statusmessage;
        HubConnection hubConnection;
        FoodOrder _foodOrder;
        bool ifpreexistingorder = false;
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();
        string tablenumber = Convert.ToString(TableNumber.tablenumber);
        public FoodQueuePage()
        {
            InitializeComponent();
            CheckOrder();
            customerordertimer = new PollWebApi(TimeSpan.FromSeconds(60), CheckOrder);
            ordernnumbertimer = new PollWebApi(TimeSpan.FromSeconds(5), GetCurrentOrderNumber);
            queuenumbertimer = new PollWebApi(TimeSpan.FromSeconds(5), QueueNumber);
            //finalgetorderurl = getorderurl + deviceid;
            //CheckForChanges();
        }

        private void CheckForChanges()
        {
            hubConnection = new HubConnectionBuilder().WithUrl(SignalrUrl).Build();
            //var hubConnection = new HubConnection(SignalrUrl);

            hubConnection.StartAsync();

            hubConnection.On("getAllOrders", (string jsonresult) =>
            {
                var ordered = JsonConvert.DeserializeObject<List<QueueTable>>(jsonresult);
                ObservableCollection<QueueTable> orderedfood = new ObservableCollection<QueueTable>(ordered);
                FoodQueueListView.ItemsSource = orderedfood;
            });
        }

        protected override void OnAppearing()
        {
            GetCurrentOrderNumber();
            QueueNumber();
            FoodQueueListView.ItemsSource = null;
            FoodQueueListView.ItemsSource = QueueOrder.Instance;
            customerordertimer.Start();
            ordernnumbertimer.Start();
            queuenumbertimer.Start();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            //hubConnection.StopAsync();
            customerordertimer.Stop();
            ordernnumbertimer.Stop();
            queuenumbertimer.Stop();
            base.OnDisappearing();
        }

        private async void QueueNumber()
        {
            if (FoodQueueListView.ItemsSource != null)
            {
                Uri url = new Uri(queueurl);

                response = await httpClient.GetAsync(url + tablenumber);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject(content).ToString();
                    int queuenumber = Convert.ToInt32(result);

                    if (queuenumber > 0)
                    {
                        lblQueueNumberActual.Text = Convert.ToString(queuenumber);
                    }
                    else if (queuenumber == 0)
                    {
                        lblQueueNumberActual.Text = "";
                    }
                    else
                    {
                        lblQueueNumberActual.Text = "";
                    }
                }
            }
        }

        private async void GetCurrentOrderNumber()
        {
            Uri url = new Uri(ordernumberurl);

            response = await httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject(content).ToString();

                int tablenumber = Convert.ToInt32(result);

                if(tablenumber == TableNumber.tablenumber)
                {
                    lblOrderNumberActual.Text = "It's currently your turn!";
                }
                else if(tablenumber == 0)
                {
                    lblOrderNumberActual.Text = "There's currently no one in line...";
                }
                else
                {
                    lblOrderNumberActual.Text = Convert.ToString(tablenumber);
                }
            }
            else
            {

            }
        }

        private async void CheckOrder()
        {
            //string tablenumber = Convert.ToString(TableNumber.tablenumber);
            Uri url = new Uri(getorderurl);
            response = await httpClient.GetAsync(url+ tablenumber);

            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var ordered = JsonConvert.DeserializeObject<List<QueueTable>>(content);
                ObservableCollection<QueueTable> orderedfood = new ObservableCollection<QueueTable>(ordered);
                var foodcart = QueueOrder.Instance;
                var foodmenulist = FoodMenuList.menulist;
                //foreach(var item in foodcart)
                //{
                foodcart.Clear();
                foreach (var item in orderedfood)
                {
                    string newprice = Convert.ToString("P" + item.Price);
                    string newpriceamount = Convert.ToString("P" + item.PriceAmount);

                    _foodOrder = new FoodOrder();
                    _foodOrder.FoodOrderDeviceID = item.TableNumber;
                    _foodOrder.FoodOrderItem = item.Item;
                    _foodOrder.FoodOrderQuantity = item.Quantity;
                    _foodOrder.FoodOrderPrice = item.Price;
                    _foodOrder.FoodOrderPriceAmount = item.PriceAmount;
                    _foodOrder.FoodOrderPriceString = newprice;
                    _foodOrder.FoodOrderAmountString = newpriceamount;
                    _foodOrder.TransactionNumber = item.TransactionNumber;

                    foreach (var cartitem in foodmenulist.Where(a => a.FoodName == _foodOrder.FoodOrderItem))
                    {
                        _foodOrder.FoodOrderPic = cartitem.FoodPic;
                    }
                    //foreach(var cartitem in foodcart)
                    //{
                    //    cartitem.FoodOrderDeviceID = item.TempDeviceId;
                    //    cartitem.FoodOrderNumber = item.TempOrderNumber;
                    //    cartitem.FoodOrderItem = item.TempInvoiceItem;
                    //    cartitem.FoodOrderQuantity = item.TempInvoiceQuantity;
                    //    cartitem.FoodOrderPrice = item.TempOrderPrice;
                    //    cartitem.FoodOrderPriceAmount = item.TempOrderPriceAmount;
                    //    cartitem.FoodOrderTime = item.TempInvoiceTime;
                    //    cartitem.FoodOrderMonth = item.TempInvoiceMonth;
                    //    cartitem.FoodOrderDay = item.TempInvoiceDay;
                    //    cartitem.FoodOrderYear = item.TempInvoiceYear;
                    //    cartitem.FoodOrderPriceString = newprice;
                    //    cartitem.FoodOrderAmountString = newpriceamount;
                    //    foreach(var menuitem in foodmenulist.Where(a=> a.FoodName == item.TempInvoiceItem))
                    //    {
                    //        cartitem.FoodOrderPic = menuitem.FoodPic;
                    //    }
                    foodcart.Add(_foodOrder);
                    //}
                }
            }
            //foreach(var item in foodcart)
            //{
            //    //txtOrderNumber.Text = Convert.ToString(item.FoodOrderNumber);
            //}
            FoodQueueListView.ItemsSource = QueueOrder.Instance;
            //}
            //FoodQueueListView.ItemsSource = orderedfood;
            //await DisplayAlert("Message", "New Data Received", "Ok");

            if (Convert.ToInt32(QueueOrder.Instance.Count) != 0)
            {
                txtName.IsVisible = true;
                txtQuantity.IsVisible = true;
                txtPrice.IsVisible = true;
                txtAmount.IsVisible = true;
                txtDelete.IsVisible = true;
                lblNoItems.IsVisible = false;
                btnUpdate.IsEnabled = true;
                btnReset.IsEnabled = true;
                FoodQueueListView.IsVisible = true;
                //txtOrderNumber.IsVisible = true;
                //txtOrderNumberLabel.IsVisible = true;
                ifpreexistingorder = true;
                //GetTempOrderNumber();
                return;
            }
            else
            {
                txtName.IsVisible = false;
                txtQuantity.IsVisible = false;
                txtPrice.IsVisible = false;
                txtAmount.IsVisible = false;
                txtDelete.IsVisible = false;
                lblNoItems.IsVisible = true;
                btnUpdate.IsEnabled = false;
                btnReset.IsEnabled = false;
                FoodQueueListView.IsVisible = false;
                //txtOrderNumber.IsVisible = false;
                //txtOrderNumberLabel.IsVisible = false;
                //GetTempOrderNumber();
                return;
            }
        }
        private async void SwipeGestureRecognizer_Swiped_Down(object sender, SwipedEventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var user = btn.BindingContext as FoodOrder;
            bool checkdelete = await DisplayAlert("Confirmation Message: ", "Are you sure you want to remove this item from the queue?", "Ok", "Cancel");
            if(checkdelete)
            {
                Uri url = new Uri(getorderurl);
                response = await httpClient.DeleteAsync(url+tablenumber+","+user.FoodOrderItem);

                if(response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Status Message:", "Food has been deleted from order", "Ok");
                    QueueOrder.Instance.Remove(user);
                }
                else
                {
                    await DisplayAlert("Server Error:", "There was a problem when sending the request to the server", "Ok");
                }
            }
            //bool CheckConfirmation = await DisplayAlert("Confirmation Message", "Are you sure you want to remove this item from the cart?", "Ok", "Cancel");
            //if (CheckConfirmation)
            //{
            //    //await DisplayAlert("Message", "Copy", "Ok");
            //    Foodcart.Instance.Remove(user);
            //    await DisplayAlert("Status", "Food has been deleted", "Ok");
            //    BadgeCount.badgecount -= 1;
            //    //MessagingCenter.Send<object, string>(this, "BadgeCount", $"{BadgeCount.badgecount}");
            //    if (Convert.ToInt32(Foodcart.Instance.Count) == 0)
            //    {
            //        txtName.IsVisible = false;
            //        txtQuantity.IsVisible = false;
            //        txtPrice.IsVisible = false;
            //        txtAmount.IsVisible = false;
            //        txtDelete.IsVisible = false;
            //        lblNoItems.IsVisible = true;
            //        btnOrder.IsEnabled = false;
            //        FoodQueueListView.IsVisible = false;
            //    }
            //}
            //else
            //{
            //    //await DisplayAlert("Message", "Affirmative", "Ok");
            //}
        }

        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            var menu = QueueOrder.Instance;
            bool checkUpdate = await DisplayAlert("Confirmation Message: ", "Are you sure you want to update your order?", "Ok", "Cancel");
            if (checkUpdate)
            {
                foreach (var item in menu)
                {
                    _queueTable = new QueueTable();
                    _queueTable.TableNumber = Convert.ToInt32(tablenumber);
                    _queueTable.Item = item.FoodOrderItem;
                    _queueTable.Quantity = item.FoodOrderQuantity;
                    _queueTable.Price = item.FoodOrderPrice;
                    _queueTable.PriceAmount = item.FoodOrderPriceAmount;
                    String json = JsonConvert.SerializeObject(_queueTable);

                    StringContent stringcontent = new StringContent(json, Encoding.UTF8, "application/json");
                    Uri url = new Uri(updateurl);
                    response = await httpClient.PutAsync(url, stringcontent);

                    if (response.IsSuccessStatusCode)
                    {
                        statusmessage = true;
                        //await DisplayAlert("Status Message:", "Your order has been updated!", "Ok");
                    }
                    else
                    {
                        statusmessage = false;
                        //await DisplayAlert("Server Error:", "There was a problem when sending the request to the server", "Ok");
                    }
                }

                if(statusmessage==true)
                {
                    await DisplayAlert("Status Message:", "Your order has been updated!", "Ok");
                    CheckOrder();
                    //response = await httpClient.GetAsync()
                }
                else
                {
                    await DisplayAlert("Server Error:", "There was a problem when sending the request to the server", "Ok");
                }
            }
        }

        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView lv)
            {
                lv.SelectedItem = null;
                lv.IsEnabled = false;
                var details = (e.Item as FoodOrder);
                var queuelist = FoodMenuList.menulist;

                foreach (var getitem in queuelist.Where(a => a.FoodName == details.FoodOrderItem))
                {
                    await Navigation.PushModalAsync(new FoodQueueDetailsPage(getitem.FoodPic, details.FoodOrderItem, details.FoodOrderQuantity, getitem.FoodSRP, details.FoodOrderPriceAmount));
                }
                //var fooddetails = FoodMenuList.menulist.Where(a => a.FoodName == details.FoodOrderItem);
                //await Navigation.PushAsync(new FoodDetails(details.FoodPic, details.FoodId, details.FoodName, details.FoodPrice, details.FoodCategory));
                lv.IsEnabled = true;
            }
        }

        private async void btnReset_Clicked(object sender, EventArgs e)
        {
            Uri url = new Uri(getorderurl);

            response = await httpClient.GetAsync(url + tablenumber);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var ordered = JsonConvert.DeserializeObject<List<QueueTable>>(content);
                ObservableCollection<QueueTable> orderedfood = new ObservableCollection<QueueTable>(ordered);
                var foodcart = QueueOrder.Instance;

                var foodmenulist = FoodMenuList.menulist;
                foodcart.Clear();
                foreach (var item in orderedfood)
                {
                    string newprice = Convert.ToString("P" + item.Price);
                    string newpriceamount = Convert.ToString("P" + item.PriceAmount);

                    _foodOrder = new FoodOrder();
                    _foodOrder.FoodOrderDeviceID = item.TableNumber;
                    _foodOrder.FoodOrderItem = item.Item;
                    _foodOrder.FoodOrderQuantity = item.Quantity;
                    _foodOrder.FoodOrderPrice = item.Price;
                    _foodOrder.FoodOrderPriceAmount = item.PriceAmount;
                    _foodOrder.FoodOrderPriceString = newprice;
                    _foodOrder.FoodOrderAmountString = newpriceamount;
                    _foodOrder.TransactionNumber = item.TransactionNumber;

                    foreach (var cartitem in foodmenulist.Where(a => a.FoodName == _foodOrder.FoodOrderItem))
                    {
                        _foodOrder.FoodOrderPic = cartitem.FoodPic;
                    }

                    foodcart.Add(_foodOrder);
                }

                FoodQueueListView.ItemsSource = QueueOrder.Instance;
            }
        }
    }
}