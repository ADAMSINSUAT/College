using FoodMenuApp.Services;
using FoodMenuApp.UnusedPages;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodMenuApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodMenuTabbedPage : TabbedPage
    {
        //ContentPage page = new FoodMenuPAge();
        ActivityIndicator activityIndicator = new ActivityIndicator();
        HttpClient client = new HttpClient();
        //public BadgeCount _badgeCount;
        int count;
        public FoodMenuTabbedPage()
        {
            InitializeComponent();

            //CheckConnection();
            //CheckMessage();
            Connectivity.ConnectivityChanged += ConnectivityChangedHandler;
            //GetJsonData();
        }
        
        //private void CheckBadgeCount()
        //{
        //    if(_badgeCount.count!=0)
        //    {

        //    }
        //}
        protected override async void OnAppearing()
        {
            //base.OnAppearing();
            base.OnAppearing();
            this.ToolbarItems.First().IsEnabled = true;
            //if (_badgeCount.count > 0)
            //{
            await Task.Delay(200);
            DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{BadgeCount.badgecount}", Color.Red, Color.White);
        }

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            await Task.Delay(1);
            DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{BadgeCount.badgecount}", Color.Red, Color.White);
        }

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    this.ToolbarItems.First().IsEnabled = false;
        //    if(count>0)
        //    {
        //        MessagingCenter.Send<object, string>(this, "BadgeCount", $"{count}");
        //    }
        //    else
        //    {
        //        MessagingCenter.Send<object, string>(this, "BadgeCount", $"{count}");
        //    }
        //}


        //public async Task CheckMessage()
        //{
        //    await Task.Run(() =>
        //    {
        //        MessagingCenter.Subscribe<object, string>(this, "BadgeCount", async (sender, args) =>
        //        {
        //            //await DisplayAlert("Message", "BadgeCount" + args, "Ok");
        //            await Task.Delay(500);
        //            DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{ args}", Color.Red, Color.White);
        //            count = Convert.ToInt32(args);
        //        });
        //    });

        //    //MessagingCenter.Unsubscribe<object, string>(this, "BadgeCount");
        //}

        private async void ConnectivityChangedHandler(object sender, ConnectivityChangedEventArgs e)
        {
            var contentpage = new ContentPage();
            var noconnection = new Label() { Text = "You currently are not connected to the restaurant wifi. Please reconnect or order manually at the cashier", HorizontalTextAlignment = TextAlignment.Center };
            var notconnectedtoapi = new Label() { Text = "Cannot connect to the server network", HorizontalTextAlignment = TextAlignment.Center };
            var stack = new StackLayout() { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.CenterAndExpand };
            var reconnect = new Button() { Text = "Reconnect", HorizontalOptions = LayoutOptions.Center };
            reconnect.Clicked += new EventHandler(reconnect_clicked);
            reconnect.Text = "Retry";
            bool isRemoteReachable = await CrossConnectivity.Current.IsRemoteReachable("http://192.168.43.47:8082/api/", 10);
            if (e.NetworkAccess == NetworkAccess.None)
            {
                this.Children.Clear();
                stack.Children.Add(notconnectedtoapi);
                stack.Children.Add(reconnect);
                contentpage.Content = stack;
                this.Children.Add(contentpage);
                FoodCartIcon.IsEnabled = false;
                FoodQueueIcon.IsEnabled = false;
                TableNumber.IsEnabled = false;
            }
            else if(e.NetworkAccess == NetworkAccess.Internet)
            {
                if(!isRemoteReachable)
                {
                    if(FoodMenuApp.Services.TableNumber.tablenumber==0)
                    {
                        CheckIfDeviceIsRegistered check = new CheckIfDeviceIsRegistered();
                        await check.CheckIfTableNumberisRegistered();
                    }
                    this.Children.Clear();
                    await GetJsonFoodCategory();
                    FoodCartIcon.IsEnabled = true;
                    FoodQueueIcon.IsEnabled = true;
                    TableNumber.IsEnabled = true;
                    return;
                }
                else
                {
                    this.Children.Clear();
                    stack.Children.Add(noconnection);
                    stack.Children.Add(reconnect);
                    contentpage.Content = stack;
                    this.Children.Add(contentpage);
                    FoodCartIcon.IsEnabled = false;
                    FoodQueueIcon.IsEnabled = false;
                    TableNumber.IsEnabled = false;
                }
            }
        }

        public static List<Task> TaskList = new List<Task>(); 

        public async Task GetJsonFoodCategory()
        {
            var contentpage = new ContentPage();
            var noconnection = new Label() { Text = "You currently are not connected to the restaurant wifi. Please reconnect or order manually at the cashier", HorizontalTextAlignment = TextAlignment.Center };
            var stack = new StackLayout() { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.CenterAndExpand };
            var reconnect = new Button() { Text = "Reconnect", HorizontalOptions = LayoutOptions.Center };
            reconnect.Clicked += new EventHandler(reconnect_clicked);
            reconnect.Text = "Retry";
            try
            {
                string url = "http://192.168.43.47:8082/api/FoodCategory";
                var isReachable = await CrossConnectivity.Current.IsRemoteReachable("http://192.168.43.47:8082/api/FoodCategory", 500);
                
                if (isReachable)
                {
                    HttpClient client = new HttpClient();

                    HttpResponseMessage response = new HttpResponseMessage();

                    response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var FoodCategoryList = JsonConvert.DeserializeObject<List<FoodCategory>>(content);
                        ObservableCollection<FoodCategory> foodCategory = new ObservableCollection<FoodCategory>(FoodCategoryList);
                        var page = new FoodMenuPAge();
                        foreach (var item in foodCategory)
                        {
                            this.Children.Add(page = new FoodMenuPAge());
                            page.Title = item.Category;
                            page.IconImageSource = IconImageSource = ImageSource.FromStream(() => new MemoryStream(item.Image));

                            FoodCartIcon.IsEnabled = true;
                            FoodQueueIcon.IsEnabled = true;
                            TableNumber.IsEnabled = true;
                        }
                    }
                    else
                    {
                        this.Children.Clear();
                        stack.Children.Add(noconnection);
                        stack.Children.Add(reconnect);
                        contentpage.Content = stack;
                        this.Children.Add(contentpage);
                        FoodCartIcon.IsEnabled = false;
                        FoodQueueIcon.IsEnabled = false;
                        TableNumber.IsEnabled = false;
                    }
                }
                else
                {
                    this.Children.Clear();
                    stack.Children.Add(noconnection);
                    stack.Children.Add(reconnect);
                    contentpage.Content = stack;
                    this.Children.Add(contentpage);
                    FoodCartIcon.IsEnabled = false;
                    FoodQueueIcon.IsEnabled = false;
                    TableNumber.IsEnabled = false;
                }
            }
            catch (Exception ey)
            {
                Debug.WriteLine("" + ey);
            }
        }
        public async Task CheckConnection()
        {
            var contentpage = new ContentPage();
            var noconnection = new Label() {Text= "You currently are not connected to the restaurant wifi. Please reconnect or order manually at the cashier", HorizontalTextAlignment=TextAlignment.Center };
            var notconnectedtoapi = new Label() { Text = "Cannot connect to the server network", HorizontalTextAlignment = TextAlignment.Center };
            var stack = new StackLayout() {HorizontalOptions=LayoutOptions.Center, VerticalOptions=LayoutOptions.CenterAndExpand };
            var reconnect = new Button() {Text = "Reconnect", HorizontalOptions=LayoutOptions.Center};
            reconnect.Clicked += new EventHandler(reconnect_clicked);
            reconnect.Text = "Retry";
            //bool isReachable = await CrossConnectivity.Current.IsReachable("http://192.168.43.47:8082/api/FoodMenu", 1 * 1000);
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                this.Children.Clear();
                await GetJsonFoodCategory();
                return;
            }
            else if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                this.Children.Clear();
                stack.Children.Add(noconnection);
                stack.Children.Add(reconnect);
                contentpage.Content = stack;
                this.Children.Add(contentpage);
                FoodCartIcon.IsEnabled = false;
                FoodQueueIcon.IsEnabled = false;
                TableNumber.IsEnabled = false;
            }
        }

        private async void reconnect_clicked(object sender, EventArgs e)
        {
            await CheckConnection();
        }

        private async void FoodCartIcon_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new FoodCartPage());
        }

        private async void FoodQeue_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new FoodQueuePage());
        }

        private async void TableNumber_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageTableNumber());
        }
    }
}