using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.Client
{
    public class LoginClient : IClient
    {
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions option;

        public LoginClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri("https://localhost:7245/Auth/");
            // httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task SetRegistration()
        {
            var v = new Users() { Id = "pranshul", Password = "Pranshul14", Token = "" };
            var json = JsonSerializer.Serialize(v);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "RegisterUser")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            using (var responce = httpClient.SendAsync(requestMessage).Result)
            {
                responce.EnsureSuccessStatusCode();
                var vv = responce.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
