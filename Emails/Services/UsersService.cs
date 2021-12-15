using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Emails.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _client;

        public UsersService(HttpClient client, IConfiguration config)
        {
            string baseUrl = config["BaseUrls:UsersService"];
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = client;
        }

        public async Task<UsersDTO> GetUserAsync(int id) 
        {
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
