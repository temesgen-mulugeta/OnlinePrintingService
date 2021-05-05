using OnlinePrintingService.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OnlinePrintingService.Models
{
    public interface ILoginClient
    {
        Task<TokenResponse> Login(string email, string password);
    }

    public class LoginClient : ClientBase, ILoginClient
    {
        private const string TokenUri = "https://localhost:44358/token";

        public LoginClient(IApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<TokenResponse> Login(string email, string password)
        {
            var response = await ApiClient.PostFormEncodedContent
            (TokenUri, new KeyValuePair<string, string>("grant_type", "password"),
             new KeyValuePair<string, string>("username", email),
              new KeyValuePair<string, string>("username", password));
            var tokenResponse = await CreateJsonResponse<TokenResponse>(response);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await DecodeContent<dynamic>(response);
                tokenResponse.ErrorState = new ErrorStateResponse
                {
                    ModelState = new Dictionary<string, string[]>
                {
                    {errorContent["error"],
                    new string[] {errorContent["error_description"]}}
                }
                };
                return tokenResponse;
            }

            var tokenData = await DecodeContent<dynamic>(response);
            tokenResponse.Data = tokenData["access_token"];
            return tokenResponse;
        }
    }
}