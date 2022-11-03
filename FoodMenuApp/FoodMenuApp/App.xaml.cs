using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Connectivity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using FoodMenuApp.Services;
using System.Linq;

namespace FoodMenuApp
{
    public partial class App : Application
    {
        //FoodMenuTabbedPage foodmenus = new FoodMenuTabbedPage();
        //NavigationPage navigationPage = new NavigationPage();
        //private string args;

        public App()
        {
            InitializeComponent();
            //var foodmenu = foodmenus;
            //var foodmenu = new FoodMenuTabbedPage();
            //foodmenu.CheckConnection();
            //MainPage = new NavigationPage(foodmenu);
            //MainPage = new FoodMenuTabbedPage();
            MainPage = new LoadingScreen();
            //navigationPage.Popped += NavigationPage_Popped;
            //MainPage = new NavigationPage(new FoodMenuPAge());
        }

        //private void NavigationPage_Popped(object sender, NavigationEventArgs e)
        //{
        //    if(foodmenus.ToolbarItems.Count>0)
        //    {
        //        DependencyService.Get<IToolbarItemBadgeService>().SetBadge(e.Page, foodmenus.ToolbarItems.First(), $"{args}", Color.Red, Color.White);
        //    }
        //}

        protected override async void OnStart()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                bool isReachable = await CrossConnectivity.Current.IsRemoteReachable("http://192.168.43.47:8082/api/FoodCategory", 500);

                if(isReachable)
                {
                    var checktablenumber = new CheckIfDeviceIsRegistered();
                    await checktablenumber.CheckIfTableNumberisRegistered();
                    var tablenumber = TableNumber.tablenumber;

                    if (tablenumber != 0)
                    {
                        var foodmenu = new FoodMenuTabbedPage();
                        await foodmenu.CheckConnection();
                        //await foodmenu.CheckMessage();
                        MainPage = new NavigationPage(foodmenu);
                    }
                    else
                    {
                        bool RegisterDeviceOrNot = await MainPage.DisplayAlert("Status Message!", "Current Device is currently not registered. Would you like to register it?", "Register", "Exit");

                        if (RegisterDeviceOrNot)
                        {
                            var registerdevice = new RegisterTableName();
                            MainPage = new NavigationPage(registerdevice);
                        }
                        else
                        {
                            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                        }
                    }
                }
                else
                {
                    //await MainPage.DisplayAlert("Error: No Internet Access", "App functions will be limited", "Ok");

                    //var foodmenu = new FoodMenuTabbedPage();
                    //await foodmenu.CheckConnection();
                    ////await foodmenu.CheckMessage();
                    //MainPage = new NavigationPage(foodmenu);
                    CannotConnect();
                }
            }
            else
            {
                CannotConnect();   
            }
        }

        private async void CannotConnect()
        {
            await MainPage.DisplayAlert("Error: No Internet Access", "App functions will be limited", "Ok");

            var foodmenu = new FoodMenuTabbedPage();
            await foodmenu.CheckConnection();
            //await foodmenu.CheckMessage();
            MainPage = new NavigationPage(foodmenu);
        }

        //protected override async void OnStart()
        //{
        //    var foodmenu = new FoodMenuTabbedPage();
        //    await foodmenu.CheckConnection();
        //    MainPage = new NavigationPage(foodmenu);
        //}

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
