using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FoodMenuApp.Droid;
using FoodMenuApp.Services;

[assembly: Xamarin.Forms.Dependency(typeof(UniqueAndroid))]
namespace FoodMenuApp.Droid
{
    public class UniqueAndroid:IDevice
    {
        public string GetIdentifier()
        {
            return Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
        }
    }
}