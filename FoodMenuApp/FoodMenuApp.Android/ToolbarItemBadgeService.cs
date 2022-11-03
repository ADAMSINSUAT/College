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
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(ToolbarItemBadgeService))]
namespace FoodMenuApp.Droid
{
    public class ToolbarItemBadgeService : IToolbarItemBadgeService
    {
        static Context _context;
        public static void Init(Context context)
        {
            _context = context;
        }
        public void SetBadge(Page page, ToolbarItem item, string value, Color backgroundColor, Color textColor)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var activity = _context as Activity;
                //var toolbar = activity.FindViewById(Resource.Id.toolbar) as Android.Support.V7.Widget.Toolbar;
                var toolbar = CrossCurrentActivity.Current.Activity.FindViewById(Resource.Id.toolbar) as Android.Support.V7.Widget.Toolbar;
                if (toolbar != null)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        var idx = page.ToolbarItems.IndexOf(item);
                        if (toolbar.Menu.Size() > idx)
                        {
                            var menuItem = toolbar.Menu.GetItem(idx);
                            //BadgeDrawable.SetBadgeText(_context, menuItem, value, backgroundColor.ToAndroid(), textColor.ToAndroid());
                            BadgeDrawable.SetBadgeText(CrossCurrentActivity.Current.Activity, menuItem, value, backgroundColor.ToAndroid(), textColor.ToAndroid());
                        }
                    }
                }
            });
        }
    }
}