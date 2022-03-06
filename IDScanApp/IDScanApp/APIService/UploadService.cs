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
        public async Task<ResponseModel> UploadImage(UploadRequestModel uploadRequestModel)
        {
             try
                {
                    var apiResponse = await PostWithFormDataRequest<ResponseModel>("fileupload/upload", uploadRequestModel);
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
