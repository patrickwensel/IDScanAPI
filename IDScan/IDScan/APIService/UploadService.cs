using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IDScanApp.ApiService;
using IDScanApp.ApiService.Interfaces;
using IDScanApp.Models;

namespace IDScanApp.ApiService
{
    public class UploadService : ApiBaseService, IUploadService
    {      
        public async Task<ResponseModel> UploadImage(string encrypyed)
        {
             try
                {
                    var apiResponse = await GetRequest<ResponseModel>($"UploadIDbyQR?GenQRUIDenc={encrypyed}");
                    return apiResponse;
                }
                catch (TaskCanceledException ex)
                {
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }

        }
		
    }
}
