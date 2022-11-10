using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodMenuApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoConnectionPage : ContentPage
    {
        public NoConnectionPage()
        {
            InitializeComponent();
            Connectivity.ConnectivityChanged += ConnectivityChangedHandler;
        }
        private async void ConnectivityChangedHandler(object sender, ConnectivityChangedEventArgs e)
        {
            bool isRemoteReachable = await CrossConnectivity.Current.IsRemoteReachable("http://192.168.43.47:8082/api/", 100 * 1000);
            if (e.NetworkAccess == NetworkAccess.None)
            {
                return;
            }
            else if (e.NetworkAccess == NetworkAccess.Internet)
            {
                if (isRemoteReachable)
                {
                    await Navigation.PushModalAsync(new FoodMenuTabbedPage());
                }
                else
                {
                    await Navigation.PushModalAsync(new NotConnectedToRestaurantWifi());
                }
            }
        }

        private async void NoConnectionPageButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool isReachable = await CrossConnectivity.Current.IsRemoteReachable("http://192.168.43.47:8082/api/FoodMenu", 100 * 1000);
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    //if (CrossConnectivity.Current.IsConnected)
                    //{
                    if (isReachable)
                    {
                        await Navigation.PushModalAsync(new FoodMenuTabbedPage());
                    }
                    else
                    {
                        await Navigation.PushModalAsync(new NotConnectedToRestaurantWifi());
                    }
                //}
                else if (Connectivity.NetworkAccess == NetworkAccess.None)
                {
                    return;
                }
            }
            catch (Exception ey)
            {
                Debug.WriteLine("" + ey);
            }
        }
    }
}