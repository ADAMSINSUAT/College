﻿using FoodMenuApp.Services;
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
    public partial class LoadingScreen : ContentPage
    {
        public LoadingScreen()
        {
            InitializeComponent();
        }
    }
}