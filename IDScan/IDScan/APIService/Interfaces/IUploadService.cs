using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using IDScanApp.Models;

namespace IDScanApp.ApiService.Interfaces
{
    public interface IUploadService
    {
        Task<bool> UploadImage(UploadRequestModel uploadRequestModel);

        Task<bool> UploadImage(string data);
    }
}
