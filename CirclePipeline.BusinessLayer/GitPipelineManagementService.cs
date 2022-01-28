using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CirclePipeline.BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace CirclePipeline.BusinessLayer
{
    public class GitPipelineManagementService : IGitPipelineManagementService
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration config;

        public GitPipelineManagementService(HttpClient client, IConfiguration config)
        {
            this.client = client;
            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }
        public GitPipelineManagementService()
        {
            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }


        public async Task<List<string>> GetTestNames(string projectRepo, string projectName, string gitToken)
        {
            string plainText;
            List<string> testName = new List<string>();
            string[] aText;

            var url = $"/api/v3/repos/{projectRepo}/{projectName}/contents/Items.cs";
            client.BaseAddress = new Uri(config["Git-Uri"]);
            //client.DefaultRequestHeaders.Add("Authorization", config["Git-Authorization"]);
            client.DefaultRequestHeaders.Add("Authorization", $" Bearer {gitToken}");

            HttpResponseMessage response = await client.GetAsync(url);
            string msg = await response.Content.ReadAsStringAsync(); //returns http response message as a string
            JObject jobObj = JObject.Parse(msg);

            string content = (string)jobObj.SelectToken("content");

            byte[] data = System.Convert.FromBase64String(content);
            plainText = System.Text.ASCIIEncoding.ASCII.GetString(data);

            aText = plainText.Split("\n");
            foreach (string text in aText)
            {
                if (text.StartsWith("export function ") && text.Contains("(data)") && !text.Contains("handleSummary(data)"))
                {
                    string element = text.Split(" ")[2];
                    element = element.Split("(data)")[0];
                    testName.Add(element);
                }
            }

            return testName;
        }
    }
}

