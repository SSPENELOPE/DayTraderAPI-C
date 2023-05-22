using DayTraderProAPI.Core.Interfaces;
using DayTraderProAPI.Infastructure.Identity;
using DayTraderProAPI.Core.Entities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Web;
using System.Net.Http.Headers;

namespace DayTraderProAPI.Application.CustomService
{
    public class CoinBaseSignInService : ICoinBaseSignIn
    {
        private readonly IdentityContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly string _clientSecret;

        public CoinBaseSignInService(HttpClient httpClient, IdentityContext dbContext, string clientSecret)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
            _clientSecret = clientSecret;
        }

        public async Task<string> RequestTemporaryCode()
        {   
            string clientId = "21e22ab75a20a2e76b30db64a54cac52e250d83c9f205f8c249ac1452483a90e";
            string requestUrl = "https://api.coinbase.com/oauth/authorize";
            requestUrl += $"?response_type=code";
            requestUrl += $"&client_id={clientId}";

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));

            // Make the HTTP GET request to the Coinbase API
            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Extract the code from the response URL
            string callbackUrl = response.RequestMessage.RequestUri.ToString();
            string code = ExtractCodeFromCallbackUrl(callbackUrl);

            return code;
        }

        public async Task<AccessResponse> RequestAccessKey(string AppUserId)
        {
            string clientSecret = _clientSecret;
            string code = await RequestTemporaryCode();
            var requestData = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "client_id", "21e22ab75a20a2e76b30db64a54cac52e250d83c9f205f8c249ac1452483a90e" },
                { "client_secret", clientSecret },
                { "redirect_uri", "https://localhost:7015/accounts" }
            };

            var requestContent = new FormUrlEncodedContent(requestData);

            var response = await _httpClient.PostAsync("https://api.coinbase.com/oauth/token", requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content into an AccessResponse object
                var responseObject = JsonConvert.DeserializeObject<AccessResponse>(responseContent);

                // Access token retrieved from the response
                var accessToken = responseObject.access_token;

                // Retrieve the user account based on some identifier (e.g., user ID or email)
                var userAccount = _dbContext.AppUsers.FirstOrDefault(u => u.Id == AppUserId);

                if (userAccount != null)
                {
                    // Store the access token in the user account
                    userAccount.CBAccessKey = accessToken;

                    // Save changes to the database
                    await _dbContext.SaveChangesAsync();
                }

                return responseObject;
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception("Failed to retrieve access token: " + errorMessage);
            }

        }

        private static string ExtractCodeFromCallbackUrl(string callbackUrl)
        {
            Uri uri = new(callbackUrl);
            string code = HttpUtility.ParseQueryString(uri.Query).Get("code");
            return code;
        }
    }
}
