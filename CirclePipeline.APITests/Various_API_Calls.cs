using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CirclePipeline.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace CirclePipeline.APITests
{
    public class Various_API_Calls
    {
        private IConfiguration iconfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        public Various_API_Calls()
        {
        }

        public async Task<ProjectDetails> GetProjectDetails(string projectRepo, string projectName)
        {
            HttpResponseMessage msg;
            using (var httpClient = new HttpClient())
            {
                var url = $"/api/v2/project/gh/{projectRepo}/{projectName}";
                httpClient.BaseAddress = new Uri(iconfig["Uri"]);
                httpClient.DefaultRequestHeaders.Add("Circle-Token", iconfig["Circle-Token"]);
                httpClient.DefaultRequestHeaders.Add("Authorization", iconfig["Authorization"]);

                msg = await httpClient.GetAsync(url);
            }
            string content = await msg.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<ProjectDetails>(content);
            Console.WriteLine(data);

            return data;
        }

        public async Task<PipelineWorkflows> GetPipelineWorkflows(string pipelineId)
        {

            using (var httpClient = new HttpClient())
            {
                var url = $"api/v2/pipeline/{pipelineId}/workflow";
                httpClient.BaseAddress = new Uri(iconfig["Uri"]);
                httpClient.DefaultRequestHeaders.Add("Circle-Token", iconfig["Circle-Token"]);
                httpClient.DefaultRequestHeaders.Add("Authorization", iconfig["Authorization"]);

                HttpResponseMessage msg = await httpClient.GetAsync(url);
                string content = await msg.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<PipelineWorkflows>(content);
                Console.WriteLine(data);

                return data;
            }

        }

        public async Task<EnvVariable> AddEnvironmentVariable(string projectRepo, string projectName, EnvVariable body)
        {
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"/api/v2/project/gh/{projectRepo}/{projectName}/envvar";
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(iconfig["Uri"]);
                httpClient.DefaultRequestHeaders.Add("Circle-Token", iconfig["Circle-Token"]);
                httpClient.DefaultRequestHeaders.Add("Authorization", iconfig["Authorization"]);

                HttpResponseMessage msg = await httpClient.PostAsync(url, content);
                Assert.True(msg.IsSuccessStatusCode);
                string data = await msg.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EnvVariable>(data);
            }
        }
    }
}
