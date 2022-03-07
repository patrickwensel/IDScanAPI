using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using IDScanApp.Models;

namespace IDScanApp.ApiService.Interfaces
{
	public interface IUploadService 
	{
		Task<ResponseModel> UploadImage(UploadRequestModel uploadRequestModel);
        
	}
}
