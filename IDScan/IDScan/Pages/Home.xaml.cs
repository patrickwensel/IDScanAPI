﻿using System;
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

        async void BtnStartQRScaning_Clicked(System.Object sender, System.EventArgs e)
        {
            PhotoTakenView.IsVisible = false;
            _homeViewModel.IsPhotoAccepted = true;
            PhotoTakenView.IsVisible = false;
            try
            {

                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                    scanner_OnScanResult(result);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception while scannig : {ex.Message}");
            }
            //scanner.IsScanning = true;
        }

        void scanner_OnScanResult(ZXing.Result result)
        {
            _homeViewModel.QRCode = result.Text;
            _homeViewModel.IsQRCodeScanned = !string.IsNullOrEmpty(result.Text);
        }
    }
}
