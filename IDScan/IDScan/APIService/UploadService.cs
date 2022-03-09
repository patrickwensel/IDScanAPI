using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IDScanApp.ApiService;
using IDScanApp.ApiService.Interfaces;
using IDScanApp.Models;
using RestSharp;

namespace IDScanApp.ApiService
{
    public class UploadService : ApiBaseService, IUploadService
    {
        public async Task<bool> UploadImage(UploadRequestModel uploadRequestModel)
        {
            try
            {
                //var apiResponse = await PostWithFormDataRequest<ResponseModel>("fileupload/upload", uploadRequestModel);
                //return apiResponse;
                string imageData = Convert.ToBase64String(uploadRequestModel.file);
                imageData = imageData.Replace("data:image/jpeg;base64,", string.Empty);
                string qrCodeData = Convert.ToBase64String(Encoding.UTF8.GetBytes(uploadRequestModel.id));
                string concatedData = $"{qrCodeData}|{imageData}";
                string fullData = Convert.ToBase64String(Encoding.UTF8.GetBytes(concatedData));

                var client = new RestClient("https://www.mybiodentity.com/finger/fprint.asmx?op=UploadIDbyQR");
                var request = new RestRequest();
                request.AddHeader("Content-Type", "text/xml");
                var body = @"<?xml version=""1.0"" encoding=""utf-8""?>" + "\n" +
                @"<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">" + "\n" +
                @"  <soap:Body>" + "\n" +
                @"    <UploadIDbyQR xmlns=""http://www.mybiodentity.com/"">" + "\n" +
                @"      <GenQRUIDenc>" + fullData + "\n" +
                @"</GenQRUIDenc>" + "\n" +
                @"    </UploadIDbyQR>" + "\n" +
                @"  </soap:Body>" + "\n" +
                @"</soap:Envelope>";
                request.AddParameter("text/xml", body, ParameterType.RequestBody);
                var response = await client.PostAsync(request);
                Console.WriteLine(response.Content);

                return response.StatusCode == System.Net.HttpStatusCode.OK;

            }
            catch (TaskCanceledException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
