using FoodMenuApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xam.Plugin.TabView;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Xamarin.Forms.Xaml.Internals;


namespace FoodMenuApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodMenuPAge : ContentPage
    {
        ActivityIndicator activityIndicator = new ActivityIndicator();
        TabViewControl tab = new TabViewControl();
        TabItem page = new TabItem();

        public FoodMenuPAge()
        {
            InitializeComponent();
            _ = GetJsonData();
            //CheckListView();
            //_ = RequestJsonData();
            //RequestJsonData();
        }

        //public async void Get()
        //{
        //    var tabtemplate = new TabViewPageTemplate();
        //    string url = "http://192.168.43.47:8082/api/FoodCategory";
        //    HttpClient httpClient = new HttpClient();
        //    var response = await httpClient.GetStringAsync(url);

        //    var FoodCategoryList = JsonConvert.DeserializeObject<List<FoodCategory>>(response);
        //    ObservableCollection<FoodCategory> foodCategory = new ObservableCollection<FoodCategory>(FoodCategoryList);
        //    //var page = new FoodMenuPAge();
        //    //var tabview = new TabViewPageTemplate();
        //    foreach (var item in foodCategory)
        //    {
        //        //await App.Current.MainPage.Navigation.PushAsync(tabview);
        //        tabtemplate.Title = item.Category;
        //        //IconImageSource = ImageSource.FromStream(() => new MemoryStream(item.Image));
        //        tabViewItemSource.AddTab(page);
        //        tab.AddTab(page);
        //    }
        //}
        public async Task GetJsonData()
        {
            try
            {
                //activityIndicator.IsRunning = true;
                string url = "http://192.168.43.47:8082/api/FoodMenu";
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync(url);

                var foodMenuList = JsonConvert.DeserializeObject<List<FoodMenu>>(response);
                ObservableCollection<FoodMenu> foodMenus = new ObservableCollection<FoodMenu>(foodMenuList);
                ObservableCollection<FoodMenu> foodItems = new ObservableCollection<FoodMenu>();

                var currentpage = base.Title;
                var foodmenu = foodMenus.Where(a => a.FoodCategory == currentpage).ToList();
                foreach (var data in foodmenu)
                {
                    FoodMenuList.menulist.Add(data);
                    decimal? foodprice = Convert.ToDecimal(data.FoodSRP);
                    string newprice = Convert.ToString("P" + foodprice);
                    data.PesoFoodPrice = newprice;
                    foodItems.Add(data);
                    this.FoodMenuListView.ItemsSource = foodItems;
                    if(FoodMenuListView.ItemsSource != null)
                    {
                        this.EmptyListView.IsVisible = false;
                        this.FoodMenuListView.IsVisible = true;
                        this.lblOrder.IsVisible = true;
                        this.lblPrice.IsVisible = true;
                    }
                    else
                    {
                        this.EmptyListView.IsVisible = true;
                        this.FoodMenuListView.IsVisible = false;
                        this.lblOrder.IsVisible = false;
                        this.lblPrice.IsVisible = false;
                    }
                }
                return;
                //foodItems.Clear();
            }
            catch (Exception ey)
            {
                Debug.WriteLine("" + ey);
            }
        }

        //public void CheckListView()
        //{
        //    if (this.FoodMenuListView.ItemsSource == null)
        //    {
        //        EmptyListView.IsVisible = true;
        //    }
        //}
        //public async Task RequestJsonData()
        //{
        //    try
        //    {
        //        //activityIndicator.IsRunning = true;
        //        string url = "http://192.168.43.47:8082/api/FoodMenu";
        //        HttpClient httpClient = new HttpClient();
        //        var response = await httpClient.GetStringAsync(url);

        //        var FoodMenuList = JsonConvert.DeserializeObject<List<FoodMenu>>(response);
        //        ObservableCollection<FoodMenu> foodMenus = new ObservableCollection<FoodMenu>(FoodMenuList);
        //        List<FoodMenu> listmenu = new List<FoodMenu>();
        //        foreach (var data in foodMenus)
        //        {
        //            listmenu.Add(data);
        //            this.FoodMenuListView.ItemsSource = listmenu;
        //        }
        //    }
        //    catch (Exception ey)
        //    {
        //        Debug.WriteLine("" + ey);
        //    }
        //}

        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView lv)
            {
                lv.SelectedItem = null;
                lv.IsEnabled = false;
                var details = e.Item as FoodMenu;
                await Navigation.PushAsync(new FoodDetails(details.FoodPic, details.FoodId, details.FoodName, details.FoodSRP, details.FoodCategory));
                lv.IsEnabled = true;
            }
        }
    }
}