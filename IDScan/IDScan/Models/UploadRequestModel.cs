using System;
namespace IDScanApp.Models
{
    public class UploadRequestModel
    {
        public UploadRequestModel()
        {
        }
        public byte[] file { get; set; }
        public string id { get; set; }
        public string GenQRUIDenc { get; set; }
    }
}
