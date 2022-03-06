using System;
using System.Net.Http;
using System.Threading.Tasks;

using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace IDScanApp.ApiService
{
    public class ApiBaseService 
    {
        protected async Task<T> PostRequest<T>(string urlSegment, object requestBody)
        {
            using (var restClient = new RestClient(new Uri(Config.BaseUrl)))
            {
                var request = new RestRequest(urlSegment, Method.POST);
                request.AddHeader("content-type", "application/json");

                if (requestBody != null)
                    request.AddBody(requestBody);

                try
                {
                    var apiResponse = await restClient.Execute<T>(request);
                    return apiResponse.Data;
                }
                catch (TaskCanceledException ex)
                {
                    throw ex;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        protected async Task<T> PutRequest<T>(string urlSegment, object requestBody)
        {
            using (var restClient = new RestClient(new Uri(Config.BaseUrl)))
            {
                var request = new RestRequest(urlSegment, Method.PUT);
                request.AddHeader("content-type", "application/json");

                if (requestBody != null)
                    request.AddBody(requestBody);

                try
                {
                    var apiResponse = await restClient.Execute<T>(request);
                    return apiResponse.Data;
                }
                catch (TaskCanceledException ex)
                {
                    throw ex;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        protected async Task<T> GetRequest<T>(string urlSegment)
        {
            using (var restClient = new RestClient(new Uri(Config.BaseUrl)))
            {
                var request = new RestRequest(urlSegment, Method.GET);
                request.AddHeader("content-type", "application/json");

                try
                {
                    var apiResponse = await restClient.Execute<T>(request);
                    return apiResponse.Data;
                }
                catch (System.Net.Http.HttpRequestException ex)
                {
                    throw ex;
                }
                catch (TaskCanceledException ex)
                {
                    throw ex;
                }
            }
        }

        protected async Task<T> PostWithFormDataRequest<T>(string urlSegment, object requestBody)
        {
            using (var restClient = new RestClient(new Uri(Config.BaseUrl)))
            {
                var request = new RestRequest(urlSegment, Method.POST);
                request.AddHeader("content-type", "application/json");

                request.AddFile("file", ((Models.UploadRequestModel)requestBody).file, "profile_picture.jpg", "image/jpg");
                request.AddParameter("id", ((Models.UploadRequestModel)requestBody).id);
                if (requestBody != null)
                    request.AddBody(requestBody);

                try
                {
                    var apiResponse = await restClient.Execute<T>(request);
                    return apiResponse.Data;
                }
                catch (TaskCanceledException ex)
                {
                    throw ex;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}