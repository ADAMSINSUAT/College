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
    public partial class FoodCartPage : ContentPage
    {
        Button button = new Button();
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();
        private Foodcart _FoodCart = new Foodcart();
        private QueueTable _temporaryInvoiceTable;
        private FoodOrder _foodOrder;
        private FoodMenuList _foodMenuList;
        string getorderurl= "http://192.168.43.47:8082/api/GetOrder/";
        string finalgetorderurl;
        List<string> orderitem= new List<string>();
        bool successmessage;
        HubConnection hubConnection;
        bool ifpreexistingorder = false;
        //ObservableCollection<TemporaryOrderNumber> temporder;
        //int? ordernumber;
        //private BadgeCount _badgeCount;
        int count;
        public FoodCartPage()
        {
            InitializeComponent();
            //CheckOrder();
            GetTempOrderNumber();
            //hubConnection = new HubConnectionBuilder().WithUrl(finalgetorderurl).Build();
            //hubConnection.On("refreshOrders", (string jsonresult) => {
            //    var ordered = JsonConvert.DeserializeObject<List<TemporaryInvoiceTable>>(jsonresult);
            //    ObservableCollection<TemporaryInvoiceTable> orderedfood = new ObservableCollection<TemporaryInvoiceTable>(ordered);
            //    FoodOrderCartListView.ItemsSource = orderedfood;
            //});
            //CheckOrder();
            //CheckMessage();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            foreach(var item in Foodcart.Instance)
            {
                decimal? foodorderpice = Convert.ToDecimal(item.FoodOrderPrice);
                string foodorderpricestring = Convert.ToString("P" + foodorderpice);
                item.FoodOrderPriceString = foodorderpricestring;

                decimal? foodorderpiceamount = Convert.ToDecimal(item.FoodOrderPriceAmount);
                string foodorderpriceamountstring = Convert.ToString("P" + foodorderpiceamount);
                item.FoodOrderAmountString = foodorderpriceamountstring;
            }
            FoodOrderCartListView.ItemsSource = null;
            FoodOrderCartListView.ItemsSource = Foodcart.Instance;
            if (Convert.ToInt32(Foodcart.Instance.Count)!=0)
            {
                txtName.IsVisible = true;
                txtQuantity.IsVisible = true;
                txtPrice.IsVisible = true;
                txtAmount.IsVisible = true;
                txtDelete.IsVisible = true;
                lblNoItems.IsVisible = false;
                lblOrderNumber.IsVisible = true;
                lblOrderNumberActual.IsVisible = true;
                btnOrder.IsEnabled = true;
                btnClear.IsEnabled = true;
                chkBxOrderType.IsEnabled = true;
                FoodOrderCartListView.IsVisible = true;
            }
            else
            {
                txtName.IsVisible = false;
                txtQuantity.IsVisible = false;
                txtPrice.IsVisible = false;
                txtAmount.IsVisible = false;
                txtDelete.IsVisible = false;
                lblNoItems.IsVisible = true;
                lblOrderNumber.IsVisible = false;
                lblOrderNumberActual.IsVisible = false;
                btnOrder.IsEnabled = false;
                btnClear.IsEnabled = false;
                chkBxOrderType.IsEnabled = false;
                FoodOrderCartListView.IsVisible = false;
            }
        }

        //public async void CheckMessage()
        //{
        //    await Task.Run(() =>
        //    {
        //        MessagingCenter.Subscribe<object, string>(this, "BadgeCount", async (sender, args) =>
        //        {
        //            //await DisplayAlert("Message", "BadgeCount" + args, "Ok");
        //            await Task.Delay(500);
        //            //DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{ args}", Color.Red, Color.White);
        //            count = Convert.ToInt32(args);
        //            BadgeCount.badgecount = count;
        //        });
        //    });

        //    //MessagingCenter.Unsubscribe<object, string>(this, "BadgeCount");
        //}

        private async void SwipeGestureRecognizer_Swiped_Down(object sender, SwipedEventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        
        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var user = btn.BindingContext as FoodOrder;
            bool CheckConfirmation = await DisplayAlert("Confirmation Message:", "Are you sure you want to remove this item from the eTray?", "Ok", "Cancel");
            if(CheckConfirmation)
            {
                //await DisplayAlert("Message", "Copy", "Ok");
                Foodcart.Instance.Remove(user);
                await DisplayAlert("Status", "Food has been removed", "Ok");
                BadgeCount.badgecount-=1;
                //MessagingCenter.Send<object, string>(this, "BadgeCount", $"{BadgeCount.badgecount}");
                if(Convert.ToInt32(Foodcart.Instance.Count)==0)
                {
                    txtName.IsVisible = false;
                    txtQuantity.IsVisible = false;
                    txtPrice.IsVisible = false;
                    txtAmount.IsVisible = false;
                    txtDelete.IsVisible = false;
                    lblNoItems.IsVisible = true;
                    btnOrder.IsEnabled = false;
                    chkBxOrderType.IsEnabled = true;
                    FoodOrderCartListView.IsVisible = false;
                }
            }
            else
            {
                //await DisplayAlert("Message", "Affirmative", "Ok");
            }
        }

        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView lv)
            {
                lv.SelectedItem = null;
                lv.IsEnabled = false;
                //var details = (e.Item as FoodOrder).FoodOrderItem;
                var details = (e.Item as FoodOrder);
                //var details = sender as ListView;
                var cartlist = FoodMenuList.menulist;
                //var getitem = cartlist.Where(a => a.FoodName == details);
                //foreach (var getitem in cartlist.Where(a => a.FoodName == details.FoodOrderItem))
                //{
                //    await Navigation.PopModalAsync();
                //    var page = new NavigationPage(new FoodOrderedDetails(getitem.FoodPic, getitem.FoodId, getitem.FoodName, details.FoodOrderPrice, getitem.FoodCategory));
                //    await Navigation.PushModalAsync(page);
                //    await Navigation.PushModalAsync(new FoodOrderedDetails(getitem.FoodPic, getitem.FoodId, getitem.FoodName, details.FoodOrderPrice, getitem.FoodCategory));
                //    NavigationPage.SetHasNavigationBar(this, true);
                //    NavigationPage.SetHasBackButton(this, true);
                //}
                foreach (var getitem in cartlist.Where(a => a.FoodName == details.FoodOrderItem))
                {
                    await Navigation.PushModalAsync(new FoodOrderedDetails(details.FoodOrderPic, details.FoodOrderItem, getitem.FoodCategory, getitem.FoodSRP, details.FoodOrderPriceAmount, details.FoodOrderQuantity));
                }
                //var fooddetails = FoodMenuList.menulist.Where(a => a.FoodName == details.FoodOrderItem);
                //await Navigation.PushAsync(new FoodDetails(details.FoodPic, details.FoodId, details.FoodName, details.FoodPrice, details.FoodCategory));
                lv.IsEnabled = true;
            }
        }

        private async void GetTempOrderNumber()
        {
            if(ifpreexistingorder==false)
            {
                string tempurl = "http://192.168.43.47:8082/api/Queue";
                bool isReachable = await CrossConnectivity.Current.IsRemoteReachable(tempurl, 500);
                if (isReachable)
                {
                    httpClient = new HttpClient();
                    var response = await httpClient.GetStringAsync(tempurl);
                    var food = JsonConvert.DeserializeObject(response).ToString();
                    var ifcount = int.TryParse(food, out count);
                    count = Convert.ToInt32(ifcount);
                    lblOrderNumberActual.Text = Convert.ToString(count);
                    //await DisplayAlert("Message", "Current queue number: " + food, "Ok");
                }
            }
            else
            {
                return;
            }
        }

        private async void CheckOrder()
        {
            finalgetorderurl = getorderurl + TableNumber.tablenumber;
            bool isReachable = await CrossConnectivity.Current.IsRemoteReachable(finalgetorderurl, 500);
            if (isReachable)
            {
                httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync(finalgetorderurl);
                var ordered = JsonConvert.DeserializeObject<List<QueueTable>>(response);
                ObservableCollection<QueueTable> orderedfood = new ObservableCollection<QueueTable>(ordered);
                var foodcart = Foodcart.Instance;
                var foodmenulist = FoodMenuList.menulist;
                //foreach(var item in foodcart)
                //{
                foodcart.Clear();
                foreach(var item in orderedfood)
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

                    foreach( var cartitem in foodmenulist.Where(a=> a.FoodName == _foodOrder.FoodOrderItem))
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
                FoodOrderCartListView.ItemsSource = Foodcart.Instance;
                //}
                //FoodOrderCartListView.ItemsSource = orderedfood;
                //await DisplayAlert("Message", "New Data Received", "Ok");

                if (Convert.ToInt32(orderedfood.Count) != 0)
                {
                    txtName.IsVisible = true;
                    txtQuantity.IsVisible = true;
                    txtPrice.IsVisible = true;
                    txtAmount.IsVisible = true;
                    txtDelete.IsVisible = true;
                    lblNoItems.IsVisible = false;
                    btnOrder.IsEnabled = true;
                    FoodOrderCartListView.IsVisible = true;
                    ifpreexistingorder = true;
                    GetTempOrderNumber();
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
                    btnOrder.IsEnabled = false;
                    FoodOrderCartListView.IsVisible = false;
                    GetTempOrderNumber();
                    return;
                }
            }
        }

        private async void btnOrder_Clicked(object sender, EventArgs e)
        {
            string tablenumber = Convert.ToString(TableNumber.tablenumber);
            string tempurl = "http://192.168.43.47:8082/api/Queue";
            string queueurl = "http://192.168.43.47:8082/api/Queue";
            string queueurlwithtablename = "http://192.168.43.47:8082/api/Queue/";
            string transactionnumberurl = "http://192.168.43.47:8082/api/TransactionNumber/";
            var cartorder = Foodcart.Instance;
            //var sendcart = SendFoodCart.Instance;
            ObservableCollection<QueueTable> temporaryInvoiceTables = new ObservableCollection<QueueTable>();

            Uri url = new Uri(queueurlwithtablename);

            response = await httpClient.GetAsync(url+ tablenumber);

            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var intresult = JsonConvert.DeserializeObject(content).ToString();

                if (intresult != "") //Checks if there is already an order from this table number
                {
                    List<string> itemlist = new List<string>();

                    foreach (var item in Foodcart.Instance)
                    {
                        itemlist.Add(item.FoodOrderItem);
                    }

                    foreach (var item in itemlist)
                    {

                        url = new Uri(queueurlwithtablename);
                        response = await httpClient.GetAsync(url + tablenumber+","+item);

                        if (response.IsSuccessStatusCode)
                        {
                            content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject(content).ToString();

                            if (result != "Item exist")
                            {
                                int ordernumber = Convert.ToInt32(intresult);
                                foreach (var itemname in cartorder)
                                {
                                    itemname.FoodOrderDeviceID = TableNumber.tablenumber;
                                }

                                foreach (var finalitem in cartorder.Where(a => a.FoodOrderItem != ""))
                                {
                                    _temporaryInvoiceTable = new QueueTable();
                                    _temporaryInvoiceTable.TableNumber = finalitem.FoodOrderDeviceID;
                                    _temporaryInvoiceTable.OrderNumber = ordernumber;
                                    _temporaryInvoiceTable.Item = finalitem.FoodOrderItem;
                                    _temporaryInvoiceTable.Quantity = finalitem.FoodOrderQuantity;
                                    _temporaryInvoiceTable.Price = finalitem.FoodOrderPrice;
                                    _temporaryInvoiceTable.PriceAmount = finalitem.FoodOrderPriceAmount;
                                    if (chkBxOrderType.IsChecked == true)
                                    {
                                        _temporaryInvoiceTable.OrderType = Convert.ToString("Take out");
                                    }
                                    else
                                    {
                                        _temporaryInvoiceTable.OrderType = Convert.ToString("Dine in");
                                    }

                                    response = await httpClient.GetAsync(transactionnumberurl + tablenumber);
                                    content = await response.Content.ReadAsStringAsync();
                                    string transaction = JsonConvert.DeserializeObject(content).ToString();
                                    _temporaryInvoiceTable.TransactionNumber = Convert.ToInt32(transaction);

                                    String json = JsonConvert.SerializeObject(_temporaryInvoiceTable);
                                    StringContent stringcontent = new StringContent(json, Encoding.UTF8, "application/json");
                                    response = await httpClient.PostAsync(tempurl, stringcontent);
                                    if (response.IsSuccessStatusCode)
                                    {
                                        successmessage = true;
                                        //await DisplayAlert("Confirmation Message", "Order has been sent", "Ok");
                                    }
                                    else
                                    {
                                        successmessage = false;
                                        //await DisplayAlert("Confirmation Message", "Order has not been sent", "Ok");
                                    }
                                }
                            }
                            else
                            {
                                orderitem.Add(item);
                                successmessage = false;

                                //url = new Uri(queueurl);

                                //response = await httpClient.GetAsync(url);

                                //if(response.IsSuccessStatusCode)
                                //{
                                //    content = await response.Content.ReadAsStringAsync();
                                //    result = JsonConvert.DeserializeObject(content).ToString();

                                //    int ordernumber = Convert.ToInt32(result);

                                //    foreach (var finalitem in cartorder.Where(a => a.FoodOrderItem != ""))
                                //    {
                                //        _temporaryInvoiceTable = new QueueTable();
                                //        _temporaryInvoiceTable.TableNumber = finalitem.FoodOrderDeviceID;
                                //        _temporaryInvoiceTable.OrderNumber = ordernumber;
                                //        _temporaryInvoiceTable.Item = finalitem.FoodOrderItem;
                                //        _temporaryInvoiceTable.Quantity = finalitem.FoodOrderQuantity;
                                //        _temporaryInvoiceTable.Price = finalitem.FoodOrderPrice;
                                //        _temporaryInvoiceTable.PriceAmount = finalitem.FoodOrderPriceAmount;
                                //        if (chkBxOrderType.IsChecked == true)
                                //        {
                                //            _temporaryInvoiceTable.OrderType = Convert.ToString("Take out");
                                //        }
                                //        else
                                //        {
                                //            _temporaryInvoiceTable.OrderType = Convert.ToString("Dine in");
                                //        }

                                //        response = await httpClient.GetAsync(transactionnumberurl);
                                //        content = await response.Content.ReadAsStringAsync();
                                //        string transaction = JsonConvert.DeserializeObject(content).ToString();
                                //        _temporaryInvoiceTable.TransactionNumber = Convert.ToInt32(transaction);

                                //        String json = JsonConvert.SerializeObject(_temporaryInvoiceTable);
                                //        StringContent stringcontent = new StringContent(json, Encoding.UTF8, "application/json");
                                //        response = await httpClient.PostAsync(tempurl, stringcontent);
                                //        if (response.IsSuccessStatusCode)
                                //        {
                                //            successmessage = true;
                                //            //await DisplayAlert("Confirmation Message", "Order has been sent", "Ok");
                                //        }
                                //        else
                                //        {
                                //            successmessage = false;
                                //            //await DisplayAlert("Confirmation Message", "Order has not been sent", "Ok");
                                //        }
                                //    }
                                //}

                            }
                        }
                    }
                }
                else
                {
                    List<string> itemlist = new List<string>();

                    foreach (var item in Foodcart.Instance)
                    {
                        itemlist.Add(item.FoodOrderItem);
                    }

                    response = await httpClient.GetAsync("http://192.168.43.47:8082/api/TransactionNumber");
                    content = await response.Content.ReadAsStringAsync();
                    string transaction = JsonConvert.DeserializeObject(content).ToString();

                    foreach (var item in itemlist)
                    {
                        response = await httpClient.GetAsync(queueurlwithtablename + tablenumber + "," + item);
                        if (response.IsSuccessStatusCode)
                        {
                            content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject(content).ToString();

                            if (result == "Item does not exist")
                            {
                                response = await httpClient.GetAsync(queueurl);

                                if (response.IsSuccessStatusCode)
                                {
                                    content = await response.Content.ReadAsStringAsync();
                                    result = JsonConvert.DeserializeObject(content).ToString();

                                    int ordernumber = Convert.ToInt32(result);

                                    foreach (var itemnumber in cartorder)
                                    {
                                        itemnumber.FoodOrderDeviceID = TableNumber.tablenumber;
                                    }

                                    foreach (var finalitem in cartorder.Where(a => a.FoodOrderItem == item))
                                    {
                                        _temporaryInvoiceTable = new QueueTable();
                                        _temporaryInvoiceTable.TableNumber = finalitem.FoodOrderDeviceID;
                                        _temporaryInvoiceTable.OrderNumber = ordernumber;
                                        _temporaryInvoiceTable.Item = finalitem.FoodOrderItem;
                                        _temporaryInvoiceTable.Quantity = finalitem.FoodOrderQuantity;
                                        _temporaryInvoiceTable.Price = finalitem.FoodOrderPrice;
                                        _temporaryInvoiceTable.PriceAmount = finalitem.FoodOrderPriceAmount;
                                        if (chkBxOrderType.IsChecked == true)
                                        {
                                            _temporaryInvoiceTable.OrderType = Convert.ToString("Take out");
                                        }
                                        else
                                        {
                                            _temporaryInvoiceTable.OrderType = Convert.ToString("Dine in");
                                        }
                                        _temporaryInvoiceTable.TransactionNumber = Convert.ToInt32(transaction);

                                        String json = JsonConvert.SerializeObject(_temporaryInvoiceTable);
                                        StringContent stringcontent = new StringContent(json, Encoding.UTF8, "application/json");
                                        response = await httpClient.PostAsync(tempurl, stringcontent);

                                        if (response.IsSuccessStatusCode)
                                        {
                                            orderitem.Add(_temporaryInvoiceTable.Item);
                                            successmessage = true;
                                        }
                                        else
                                        {
                                            orderitem.Add(_temporaryInvoiceTable.Item);
                                            successmessage = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                orderitem.Add(item);
                                BadgeCount.badgecount -= 1;
                                //await DisplayAlert("Error Message: ", "Item is already in queue. Please adjust the quantity in the Queue Section", "Ok");
                            }
                        }
                    }
                }
                if (successmessage == true)
                {
                    await DisplayAlert("Confirmation Message", "Order successfully sent", "Ok");
                    BadgeCount.badgecount = 0;
                    cartorder.Clear();
                    Clear();
                }
                else
                {
                    foreach(var itemname in orderitem)
                    {
                        await DisplayAlert("Confirmation Message", "Order: " + itemname + " item is already in queue. If you wish to adjust the quantity of the food, please do so at the Order Queue section", "Ok");
                    }
                    cartorder.Clear();
                    Clear();
                }
            }
        }

        private void Clear()
        {
            txtName.IsVisible = false;
            txtQuantity.IsVisible = false;
            txtPrice.IsVisible = false;
            txtAmount.IsVisible = false;
            txtDelete.IsVisible = false;
            lblNoItems.IsVisible = true;
            lblOrderNumber.IsVisible = false;
            lblOrderNumberActual.IsVisible = false;
            btnOrder.IsEnabled = false;
            btnClear.IsEnabled = false;
            chkBxOrderType.IsChecked = false;
            chkBxOrderType.IsEnabled = false;
            FoodOrderCartListView.IsVisible = false;
            BadgeCount.badgecount = 0;
            GetTempOrderNumber();
        }

        private async void btnClear_Clicked(object sender, EventArgs e)
        {
            bool ifclear = await DisplayAlert("Confirmation Message:", "Are you sure you want to clear the eTray?", "Ok", "Cancel");

            if (ifclear)
            {
                await DisplayAlert("Status Message:", "eTray has been cleared", "Ok");
                Clear();
                Foodcart.Instance.Clear();
            }
        }
    }
}