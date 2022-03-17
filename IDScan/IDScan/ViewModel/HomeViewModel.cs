using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using IDScan.Interfaces;
using IDScanApp.ApiService;
using IDScanApp.ApiService.Interfaces;
using IDScanApp.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using RestSharp;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IDScanApp.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        IUploadService _uploadService;

        private string _imgDocument = "ImgPlaceholder.png";
        private byte[] _byteImage;
        private bool _isDocumentPhotoTaken;
        private bool _isQRCodeScanned;
        private bool _isPhotoAccepted;
        private int _imageRotation;
        private string _qrCode;

        public string ImgDocument
        {
            get { return _imgDocument; }
            set
            {
                _imgDocument = value;
                OnPropertyChanged("ImgDocument");
            }
        }

        public bool IsDocumentPhotoTaken
        {
            get { return _isDocumentPhotoTaken; }
            set
            {
                _isDocumentPhotoTaken = value;
                OnPropertyChanged("IsDocumentPhotoTaken");
            }
        }

        public bool IsPhotoAccepted
        {
            get { return _isPhotoAccepted; }
            set
            {
                _isPhotoAccepted = value;
                OnPropertyChanged("IsPhotoAccepted");
                if (_isPhotoAccepted)
                    IsDocumentPhotoTaken = false;
            }
        }

        public bool IsQRCodeScanned
        {
            get { return _isQRCodeScanned; }
            set
            {
                _isQRCodeScanned = value;
                OnPropertyChanged("IsQRCodeScanned");
            }
        }

        public int ImageRotation
        {
            get { return _imageRotation; }
            set
            {
                _imageRotation = value;
                OnPropertyChanged("ImageRotation");
            }
        }

        private bool _showScanButton;
        public bool ShowScanButton
        {
            get { return _showScanButton; }
            set
            {
                _showScanButton = value;
                OnPropertyChanged("ShowScanButton");
            }
        }

        public string QRCode
        {
            get { return _qrCode; }
            set
            {
                _qrCode = value;
                OnPropertyChanged("QRCode");
            }
        }

        public ICommand TakePhotoCommand { get; set; }
        public ICommand ScanAndUploadCommand { get; set; }
        public ICommand UploadIdCommand { get; set; }

        public HomeViewModel()
        {
            TakePhotoCommand = new Command(async () => await TakePhotoAsync());
            ScanAndUploadCommand = new Command(ScanAndUploadAsync);
            UploadIdCommand = new Command(UploadDocumentAsync);
            _uploadService = new UploadService();
        }

        private async Task TakePhotoAsync()
        {
            try
            {

                var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    RotateImage = true,
                    AllowCropping = true,
                    ModalPresentationStyle = MediaPickerModalPresentationStyle.FullScreen,
                });
                //var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions() { Title = "Take Photo" });
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {ImgDocument}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
                UserDialogs.Instance.Alert("Camera is not available");
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
                UserDialogs.Instance.Alert("Please give a permission to take photo");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(MediaFile photo)
        {
            try
            {
                // canceled
                if (photo == null)
                {
                    ImgDocument = null;
                    return;
                }
                var rotation = 0;
                // save the file into local storage
                System.Diagnostics.Debug.WriteLine($"Path : {photo.Path}");
                var newFile = Path.Combine(FileSystem.CacheDirectory, DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss") + ".jpeg");

                using (var stream = photo.GetStream())
                {
                    using (var newStream = File.OpenWrite(newFile))
                    {
                        await stream.CopyToAsync(newStream);

                        using (var memory = new MemoryStream())
                        {
                            await stream.CopyToAsync(memory);

                        }
                    }
                }

                IsDocumentPhotoTaken = true;
                ImgDocument = newFile;
                _byteImage = File.ReadAllBytes(newFile);
                ShowScanButton = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception : {ex.Message}");
            }
        }



        private async void ScanAndUploadAsync()
        {
            try
            {
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                var scanOption = new ZXing.Mobile.MobileBarcodeScanningOptions()
                {
                    AutoRotate = false,
                    TryHarder = true,
                };

                var result = await scanner.Scan(scanOption);

                if (result != null)
                    await SendDataToServer(result.Text);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception while scannig : {ex.Message}");
            }
        }

        private async void UploadDocumentAsync()
        {
            await SendDataToServer(QRCode);
        }

        public async Task SendDataToServer(string resultText)
        {
            UserDialogs.Instance.ShowLoading("Uploading...");
            await Task.Delay(300);
            var response = await _uploadService.UploadImage(new UploadRequestModel()
            {
                file = _byteImage,
                id = resultText
            });

            UserDialogs.Instance.HideLoading();
            if (!response)
                UserDialogs.Instance.Alert("Error to upload image, please try again");
            else
            {
                await UserDialogs.Instance.AlertAsync("Image uploaded successfully");
                var closer = DependencyService.Get<IKillApp>();
                closer?.closeApplication();
            }
        }
    }
}
