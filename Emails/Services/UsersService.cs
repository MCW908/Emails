using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Emails.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _client;
        private IConfiguration _config { get; }

        public UsersService(HttpClient client, IConfiguration config)
        {
            string baseUrl = config["BaseUrls:UsersService"];
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = client;
            _config = config;
        }

        public async Task<UsersDTO> GetUserAsync(int id) 
        {

            var auth0AuthenticationClient = new AuthenticationApiClient(
                new Uri($"https://dev-mp2kr6gn.eu.auth0.com"));
            var tokenRequest = new ClientCredentialsTokenRequest()
            {
                ClientId = _config["Auth:ClientId"],
                ClientSecret = _config["Auth:ClientSecret"],
                Audience = _config["Services:Values:AuthAudience"]
            };
            var tokenResponse =
                await auth0AuthenticationClient.GetTokenAsync(tokenRequest);

            var baseAddress = _config["Services:Values:BaseAddress"];
            _client.BaseAddress = new Uri(baseAddress);

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            var response = await _client.GetAsync("api/Users" + id);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) 
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            var user = await response.Content.ReadAsAsync<UsersDTO>();
            return user;
        }

        public async Task<IEnumerable<UsersDTO>> GetUsersAsync(string email) 
        {
            string uri = "api/Users";
            if(email != null)
            {
                uri = uri + "?Email=" + email;
            }
            var response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var users = await response.Content.ReadAsAsync<IEnumerable<UsersDTO>>();
            return users;
        }

    }
}
