using System;
using IDScanApp.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDScanApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Home() { Title = "Home" });
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
