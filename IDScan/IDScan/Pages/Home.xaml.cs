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

        void BtnStartQRScaning_Clicked(System.Object sender, System.EventArgs e)
        {
            _homeViewModel.IsPhotoAccepted = true;
            scanner.IsScanning = true;
        }

        void scanner_OnScanResult(ZXing.Result result)
        {
            _homeViewModel.QRCode = result.Text;
            _homeViewModel.IsQRCodeScanned = !string.IsNullOrEmpty(result.Text);
        }
    }
}
