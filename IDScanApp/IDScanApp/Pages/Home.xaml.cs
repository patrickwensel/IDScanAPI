using System;
using System.Collections.Generic;
using IDScanApp.ViewModel;
using Xamarin.Forms;

namespace IDScanApp.Pages
{
    public partial class Home : ContentPage
    {
        HomeViewModel _homeViewModel;
        public Home()
        {
            _homeViewModel = new HomeViewModel();
            InitializeComponent();
            BindingContext = _homeViewModel;
        }
    }
}
