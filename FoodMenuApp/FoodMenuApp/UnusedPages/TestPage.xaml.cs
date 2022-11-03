using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodMenuApp.UnusedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
        }

        private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}