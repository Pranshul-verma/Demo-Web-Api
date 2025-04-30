// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using ConsoleApp1.Client;
using ConsoleApp1.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;

#region Dummy Api


//var host = Host.CreateDefaultBuilder(args).ConfigureServices(ConfigureServices).Build();

//try
//{
//    await host.Services.GetRequiredService<IHttpServiceImplementation>().Execute();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.ToString());`
//}

//ResigterUser().Wait();
//var v = LoginUser().Result;
//SetEmployeeDetail().Wait();
for (int i = 0; i < 6; i++)
{
    myapiget("eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMTIzIiwiZXhwIjoxNzQ1NDEyNDI0LCJpc3MiOiJNeUF3ZXNvbWVBcHAiLCJhdWQiOiJNeUF3ZXNvbWVBdWRpZW5jZSJ9.6WuRuYswIdlwvFgvBY6SgMliyUTlv50LPjwZ_VCgKKhrV4BilgfJemrLtz-cBAW-tKZ_OQJ26OEBDygtzYpgMw").Wait();

}

static async Task<string> LoginUser()
{
    var v = new Users() { Id = "pranshul", Password = "Pranshul14", Token = "" };
    var json = JsonSerializer.Serialize(v);
    var requestMessage = new HttpRequestMessage(HttpMethod.Get, "Auth/Login")
    {
        Content = new StringContent(json, Encoding.UTF8, "application/json")
    };
    using (var responce = SendDataToApiWithBearer(requestMessage,""))
    {
        if (responce.IsSuccessStatusCode)
        {
            return responce.Content.ReadAsStringAsync().Result;
        }
    }

    return default(string);
}

static void ConfigureServices(HostBuilderContext context, IServiceCollection service)
{
    service.AddScoped<IHttpServiceImplementation, HttpClientFactoryService>();
    service.AddHttpClient<LoginClient>();
}

static async Task ResigterUser()
{
    var v = new Users() { Id = "pranshul", Password = "Pranshul14", Token = "" };
    var json = JsonSerializer.Serialize(v);
    var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Auth/RegisterUser")
    {
        Content = new StringContent(json, Encoding.UTF8, "application/json")
    };

    using (var responce = SendDataToApiWithBearer(requestMessage,""))
    {
        responce.EnsureSuccessStatusCode();
        var vv = responce.Content.ReadAsStringAsync().Result;
    }

}
static async Task SetLBStrategy(string token)
{
    var v = StrategyEnum.RoundRobin;
    var json = JsonSerializer.Serialize(v);
    var requestMessage = new HttpRequestMessage(HttpMethod.Post, "LoadBalancer/SetLBStrategy")
    {
        Content = new StringContent(json, Encoding.UTF8, "application/json")
    };
    var responce = SendDataToApiWithBearer(requestMessage, token);
    if (responce.IsSuccessStatusCode)
    {
        var vv = responce.Content.ReadAsStringAsync().Result;
    }
}

static HttpResponseMessage SendDataToApiWithBearer(HttpRequestMessage requestMessage, string token)
{
    requestMessage.Headers.Add("X-Forwarded-For", GetLocalIPAddress());
    using (HttpClient client = new HttpClient())
    {
        //setting base address of Loadbalancer Api....
        client.BaseAddress = new Uri("https://localhost:7090/");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        return client.SendAsync(requestMessage).Result;
    }
}

static async Task myapiget(string token)
{
    var requestMessage = new HttpRequestMessage(HttpMethod.Get, "EmployeeDetail/GetEmployeeDetail");
    //requestMessage.Headers.Host = "https://10.80.78.73:7090/";
    HttpResponseMessage res = SendDataToApiWithBearer(requestMessage,token);
    if (res.IsSuccessStatusCode)
    {
        List<EmployeeDetail> employee = await res.Content.ReadAsAsync<List<EmployeeDetail>>();
        foreach (EmployeeDetail emp in employee)
        {
            Console.WriteLine(emp.EmpId.ToString() + " " + emp.EmpName + " " + emp.EmpEmail + " " + emp.Salary);
        }
    }
    else
    {
        Console.WriteLine(res.StatusCode);
    }
    //Console.Read();

}

static string GetLocalIPAddress()
{
    var host = Dns.GetHostEntry(Dns.GetHostName());
    foreach (var ip in host.AddressList)
    {
        if (ip.AddressFamily == AddressFamily.InterNetwork)
        {
            return ip.ToString();
        }
    }
    throw new Exception("No network adapters with an IPv4 address in the system!");
}
static async Task SetEmployeeDetail()
{
    EmployeeDetail v = new EmployeeDetail { EmpId = 2, EmpName = "Pragya rathi", EmpEmail = "Pragya.com", Salary = 10000000 };
    var json = JsonSerializer.Serialize(v);
    var requestMessage = new HttpRequestMessage(HttpMethod.Post, "LoadBalancer/SetEmployeeDetail")
    {
        Content = new StringContent(json, Encoding.UTF8, "application/json")
    };
    var responce = SendDataToApiWithBearer(requestMessage,"");
    if (responce.IsSuccessStatusCode)
    {
        var vv = responce.Content.ReadAsStringAsync().Result;
    }
}

#endregion



Console.ReadLine();


