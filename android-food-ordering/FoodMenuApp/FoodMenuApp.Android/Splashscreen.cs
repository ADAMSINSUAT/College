using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FoodMenuApp.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
        MainLauncher = true,
        NoHistory = true,
        Icon = "@drawable/Food_Menu_App_splashscreen")]
    public class Splashscreen: Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            InvokeMainActivity();
            //StartActivity(typeof(MainActivity));
            // Create your application here
        }

        private void InvokeMainActivity()
        {
            var mainActivityIntent = new Intent(this, typeof(MainActivity));
            mainActivityIntent.AddFlags(ActivityFlags.NoAnimation); //Add this line
            StartActivity(mainActivityIntent);
        }
        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    Task startupWork = new Task(() => {  SimulateStartup(); });
        //    startupWork.Start();
        //}

        // async void SimulateStartup()
        //{
        //    var mainpage = new FoodMenuTabbedPage();
        //    await mainpage.CheckConnection();
        //    StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        //}
    }
}