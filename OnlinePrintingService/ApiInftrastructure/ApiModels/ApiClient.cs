using OnlinePrintingService.ApiInfrastructure;
using OnlinePrintingService.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace OnlinePrintingService.Models
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> GetFormEncodedContent

        (string requestUri, params KeyValuePair<string, string>[] values);
        Task<HttpResponseMessage> PostFormEncodedContent
         (string requestUri, params KeyValuePair<string, string>[] values);
        
    }
    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;

        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
      
        private readonly ITokenContainer tokenContainer;


        public ApiClient(HttpClient httpClient, ITokenContainer tokenContainer)
        {
            this.httpClient = httpClient;
            this.tokenContainer = tokenContainer;
        }

        public async Task<HttpResponseMessage>
        GetFormEncodedContent(string requestUri, params KeyValuePair<string, string>[] values)
        {
            AddToken();
            using (var content = new FormUrlEncodedContent(values))
            {
                var query = await content.ReadAsStringAsync();
                var requestUriWithQuery = string.Concat(requestUri, "?", query);
                var response = await httpClient.GetAsync(requestUriWithQuery);
                return response;
            }
        }

        public async Task<HttpResponseMessage>
        PostFormEncodedContent(string requestUri, params KeyValuePair<string, string>[] values)
        {
            using (var content = new FormUrlEncodedContent(values))
            {
                var response = await httpClient.PostAsync(requestUri, content);
                
            }
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44358/token") { Content = new FormUrlEncodedContent(values) };
            var res = await client.SendAsync(req);
            return res;
        }



        

        public async Task<HttpResponseMessage>
        PostJsonEncodedContent<T>(string requestUri, T content) where T : RegisterViewModel
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add
            (new MediaTypeWithQualityHeaderValue("application/json"));
            AddToken();
            var response = await httpClient.PostAsJsonAsync(requestUri, content);
            return response;
        }

        private void AddToken()
        {
            if (tokenContainer.ApiToken != null)
            {
                httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenContainer.ApiToken.ToString());
            }
        }
    }
    public class TokenResponse : ApiResponse<string>
    {
    }

    public class ErrorStateResponse
    {
        public IDictionary<string, string[]> ModelState { get; set; }
    }
    public abstract class ClientBase
    {
        
         protected ClientBase(IApiClient apiClient)
    {
        ApiClient = apiClient;
    }
        // Other methods removed for brevity

        protected static async Task<tresponse>
        CreateJsonResponse<tresponse>(HttpResponseMessage response) where tresponse : ApiResponse, new()
        {
            var clientResponse = new tresponse
            {
                StatusIsSuccessful = response.IsSuccessStatusCode,
                ErrorState = response.IsSuccessStatusCode ?
                null : await DecodeContent<ErrorStateResponse>(response),
                ResponseCode = response.StatusCode
            };
            if (response.Content != null)
            {
                clientResponse.ResponseResult = await response.Content.ReadAsStringAsync();
            }

            return clientResponse;
        }


        public static async Task<TContentResponse>
            DecodeContent<TContentResponse>(HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();
            return Json.Decode<TContentResponse>(result);
        }

        private readonly IApiClient apiClient;

        public IApiClient ApiClient { get; }

        protected async Task<TResponse> GetJsonDecodedContent<TResponse, TContentResponse>
        (string uri, params KeyValuePair<string, string>[] requestParameters)
        where TResponse : ApiResponse<TContentResponse>, new()
        {
            var apiResponse = await apiClient.GetFormEncodedContent(uri, requestParameters);
            var response = await CreateJsonResponse<TResponse>(apiResponse);
            response.Data = Json.Decode<TContentResponse>(response.ResponseResult);
            return response;
        }

    }
    
}