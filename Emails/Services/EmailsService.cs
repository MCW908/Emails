using Emails.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Emails.Services
{
    public class EmailsService : IEmailsService
    {

        private readonly HttpClient _client;

        public EmailsService(HttpClient client) 
        {
            // Add Base URL to Appsettings
            client.BaseAddress = new System.Uri("https://localhost:44310");
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = client;
        }

        public async Task<Email> GetEmailAsync(int id)
        {
            var response = await _client.GetAsync("api/Email" + id);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            var emailString = await response.Content.ReadAsStringAsync();
            var emailAsType = JsonConvert.DeserializeObject<Email>(emailString);
            return emailAsType;
        }

        public async Task<IEnumerable<Email>> GetEmailsAsync(string sender) 
        {
            var uri = "api/reviews?category=Sender";
            if(sender != null)
            {

            }
        }

    }
}
