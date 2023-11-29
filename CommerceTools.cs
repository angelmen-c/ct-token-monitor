using ct_token_monitor.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ct_token_monitor
{
    internal class CommerceTools
    {
        public IConfiguration Config { get; }
        public ILogger Logger { get; }
        public int sleepInterval { get; set; }
        public int runStopTime { get; set; }
        public CommerceTools(IConfiguration config, ILogger logger)
        {
            Config = config;
            Logger = logger;

            sleepInterval = Config.GetValue<int>("sleepInterval") * 1000;
            runStopTime = Config.GetValue<int>("runStopTime") * 60000;

        }

        public void runMonitor()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (sw.ElapsedMilliseconds < runStopTime)
            {
                callApi();
                Thread.Sleep(sleepInterval);
            }

        }

        public void callApi()
        {
            string clientId = Config.GetSection("ctCredentials").GetValue<string>("ctClientId");
            string clientSecret = Config.GetSection("ctCredentials").GetValue<string>("ctClientSecret");
            string url = Config.GetSection("ctCredentials").GetValue<string>("ctAuthUrl");

            var urlParams= new Dictionary<string, string> { { "grant_type", "client_credentials" }};
            var encodedContent = new FormUrlEncodedContent(urlParams);

            using (HttpClient client = new HttpClient())
            {
                var basicAuthentication = $"{clientId}:{clientSecret}";
                var encodedAuthentication = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(basicAuthentication));


                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedAuthentication);
                var response = client.PostAsync(url, encodedContent).Result;
                Logger.Information(response.Content.ReadAsStringAsync().Result);
            }

        }
    }
}
