using FoodMenuApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodMenuApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestBadgePage : ContentPage
    {
        public TestBadgePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnAppearing();
        }
        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (ToolbarItems.Count > 0)
            {
                DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{e.NewValue}", Color.Red, Color.White);
            }
        }
    }
}