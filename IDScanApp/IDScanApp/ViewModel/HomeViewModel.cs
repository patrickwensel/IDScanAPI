using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using IDScanApp.ApiService;
using IDScanApp.ApiService.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IDScanApp.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        IUploadService _uploadService;

        private string _imgDocument;
        private byte[] _byteImage;
        public string ImgDocument
        {
            get { return _imgDocument; }
            set
            {
                _imgDocument = value;
                OnPropertyChanged("ImgDocument");
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


        public ICommand TakePhotoCommand { get; set; }
        public ICommand ScanAndUploadCommand { get; set; }
        public HomeViewModel()
        {
            TakePhotoCommand = new Command(TakePhotoAsync);
            ScanAndUploadCommand = new Command(ScanAndUploadAsync);
            _uploadService = new UploadService();
        }

        private async void TakePhotoAsync()
        {

            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
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

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                ImgDocument = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
            {
                await stream.CopyToAsync(newStream);
                using (var memory = new MemoryStream())
                {
                    await stream.CopyToAsync(memory);
                    _byteImage = memory.ToArray();
                }
            }

            ImgDocument = newFile;
            ShowScanButton = true;
        }
        private async void ScanAndUploadAsync()
        {


            try
            {
                
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                    SendDataToServer(result.Text);
            }
            catch (Exception ex)
            {

            }
        }

        public async void SendDataToServer(string resultText)
        {
            UserDialogs.Instance.ShowLoading("Uploading...");

            var response = await _uploadService.UploadImage(new Models.UploadRequestModel()
            {
                file = _byteImage,
                id = resultText
            });
            UserDialogs.Instance.HideLoading();
             if (response == null)
            {
                UserDialogs.Instance.Alert("Error to upload image, please try again");
            }
             else
                UserDialogs.Instance.Alert("Data sent successfully");
        }
    }
}
