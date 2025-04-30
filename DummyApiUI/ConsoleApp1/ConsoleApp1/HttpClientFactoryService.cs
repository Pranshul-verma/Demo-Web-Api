using ConsoleApp1.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class HttpClientFactoryService : IHttpServiceImplementation
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly LoginClient loginClient;

        public HttpClientFactoryService(IHttpClientFactory _httpClientFactory,LoginClient loginClient)
        {
            httpClientFactory = _httpClientFactory;
            this.loginClient = loginClient;
        }
        public async Task Execute()
        {
            SetRegistration();
            
        }

        private async Task SetRegistration() => await loginClient.SetRegistration();
    }
}
