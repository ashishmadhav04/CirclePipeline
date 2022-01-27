using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CirclePipeline.BusinessLayer.Interfaces;
using CirclePipeline.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;

namespace CirclePipeline.BusinessLayer
{
    public class CirclePipelineManagementService : ICirclePipelineManagementService
    {
        private readonly HttpClient client;
        private readonly IConfiguration config;

        public CirclePipelineManagementService(HttpClient client, IConfiguration config)
        {
            this.client = client;
            this.config = config;
        }
        public CirclePipelineManagementService()
        {
        }

        public async Task<object> GetProjectDetails(string projectRepo, string projectName)
        {
            var url = $"/api/v2/project/gh/{projectRepo}/{projectName}";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage response = await client.GetAsync(url);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsStringAsync();

            //This just returns back the response Body as string
            /*string response = await client.GetStringAsync(url);
            Console.WriteLine(response);
            return response;*/
        }

        public async Task<string> GetProjectPipeline(string projectRepo, string projectName)
        {
            var url = $"/api/v2/project/gh/{projectRepo}/{projectName}/pipeline";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage response = await client.GetAsync(url);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetSinglePipeline(string pipelineId)
        {
            var url = $"/api/v2/pipeline/{pipelineId}";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage response = await client.GetAsync(url);
            response.StatusCode.ToString();
            response.StatusCode.Equals(200);

            Console.WriteLine(response);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsStringAsync();

            //return response.StatusCode.ToString();
            //return response.StatusCode.Equals(200).ToString();
        }


        public async Task<string> GetPipelineWorkflows(string pipelineId)
        {
            var url = $"/api/v2/pipeline/{pipelineId}/workflow";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage response = await client.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetWorkflow(string workflowId)
        {
            var url = $"/api/v2/workflow/{workflowId}";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetWorkflowJobs(string workflowId)
        {
            var url = $"/api/v2/workflow/{workflowId}/job";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage response = await client.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostReRunJobs(string workflowId, JobRerun jobRerun)
        {
            var json = JsonConvert.SerializeObject(jobRerun);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine(content);

            /*From .NET 5.0 onwards, a new class called JSONConent was introduced whih derives from HttpContent
             * This class contains a static method called Create(), which takes any arbitrary object as a parameter, and as
             * the name implies returns an instance of JsonContent, which you can then pass as an argument to the PostAsync method.*/

            //JsonContent content1 = JsonContent.Create(jobRerun);

            var url = $"/api/v2/workflow/{workflowId}/rerun";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage response = await client.PostAsync(url, content);

            Console.WriteLine(response);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostApproveJob(string workflowId, string approvalId)
        {
            var url = $"/api/v2/workflow/{workflowId}/approve/{approvalId}";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage response = await client.PostAsync(url, null);

            Console.WriteLine(response);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostCancelJob(string projectRepo, string projectName, string jobId)
        {
            var url = $"/api/v2/project/gh/{projectRepo}/{projectName}/job/{jobId}/cancel";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage response = await client.PostAsync(url, null);

            Console.WriteLine(response);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostEnvironmentVariable(string projectRepo, string projectName, EnvVariable envVariable)
        {
            var json = JsonConvert.SerializeObject(envVariable);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine(content);

            var url = $"/api/v2/project/gh/{projectRepo}/{projectName}/envvar";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage response = await client.PostAsync(url, content);

            Console.WriteLine(response);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteEnvironmentVariable(string projectRepo, string projectName, string envVariableName)
        {
            var url = $"/api/v2/project/gh/{projectRepo}/{projectName}/envvar/{envVariableName}";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage response = await client.DeleteAsync(url);

            Console.WriteLine(response);

            return await response.Content.ReadAsStringAsync();
        }



        /**************************************************************************************************************************************************************/
        /**************************************************************************************************************************************************************/


        public async Task<List<string>> GetPipelineId(string projectRepo, string projectName)
        {
            var url = $"/api/v2/project/gh/{projectRepo}/{projectName}/pipeline";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage response = await client.GetAsync(url);
            string msg = await response.Content.ReadAsStringAsync(); //returns http response message as a string

            JObject obj = JObject.Parse(msg); //loads a JObject populated from a string that contains JSON
            var items = obj.SelectToken("items"); //return the specified object which is passed in SelectToken
            List<string> Ids = new List<string>();

            foreach (var item in items)
            {
                string id = (string)item.SelectToken("id");
                Ids.Add(id);
            }

            Console.WriteLine(Ids);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            return Ids;
        }

        public async Task<List<string>> GetWorkflowsId(string pipelineId)
        {
            var url = $"/api/v2/pipeline/{pipelineId}/workflow";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage response = await client.GetAsync(url);
            string message = await response.Content.ReadAsStringAsync();

            JObject obj = JObject.Parse(message);
            var items = obj.SelectToken("items");

            List<string> Ids = new List<string>();
            foreach (var item in items)
            {
                string id = (string)item.SelectToken("id");
                Ids.Add(id);
            }

            Console.WriteLine(Ids);

            return Ids;
        }

        public async Task<List<string>> GetJobsId(string workflowId)
        {
            var url = $"/api/v2/workflow/{workflowId}/job";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage response = await client.GetAsync(url);

            string message = await response.Content.ReadAsStringAsync();
            JObject obj = JObject.Parse(message);

            var items = obj.SelectToken("items");

            List<string> jobIds = new List<string>();

            foreach (var item in items)
            {
                string jobName = (string)item.SelectToken("name");
                string jobStatus = (string)item.SelectToken("status");
                if (jobName == "beta_performance_test" ||
                    jobName == "beta-performance-tests" ||
                    jobName == "beta-spike-tests" ||
                    jobName == "beta-load-tests" ||
                    jobName == "beta_load_test")
                {
                    if (jobStatus != "blocked")
                    {
                        string id = (string)item.SelectToken("id");
                        jobIds.Add(id);
                    }
                }
            }

            return jobIds;
        }

        public async Task<string> RunJobs(string projectRepo, string projectName)
        {
            List<string> pipeLineId = new List<string>();
            List<string> workFlowId = new List<string>();
            List<string> jobId = new List<string>();
            string loadTestJobId = null;
            string loadTestJobName = null;
            int loadTestJobNumber = 0;
            JObject finalValue = new JObject();

            var pipeLineUrl = $"/api/v2/project/gh/{projectRepo}/{projectName}/pipeline";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            HttpResponseMessage pipeLineResponse = await client.GetAsync(pipeLineUrl);
            string pipeLinemsg = await pipeLineResponse.Content.ReadAsStringAsync(); //returns http response message as a string

            JObject pipeLineobj = JObject.Parse(pipeLinemsg); //loads a JObject populated from a string that contains JSON
            var pipeLineItems = pipeLineobj.SelectToken("items"); //return the specified object which is passed in SelectToken

            foreach (var pipeLineitem in pipeLineItems)
            {
                string pipeLineIds = (string)pipeLineitem.SelectToken("id");
                pipeLineId.Add(pipeLineIds);

                var workFlowUrl = $"/api/v2/pipeline/{pipeLineIds}/workflow";

                HttpResponseMessage workFlowResponse = await client.GetAsync(workFlowUrl);
                string workFlowMessage = await workFlowResponse.Content.ReadAsStringAsync();

                JObject workFlowObj = JObject.Parse(workFlowMessage);
                var workFlowItems = workFlowObj.SelectToken("items");

                foreach (var workflowitem in workFlowItems)
                {
                    string workFlowIds = (string)workflowitem.SelectToken("id");
                    string workFlowStatus = (string)workflowitem.SelectToken("status");

                    if (workFlowStatus == "failing" ||
                        workFlowStatus == "success" ||
                        workFlowStatus == "failed" ||
                        workFlowStatus == "on_hold" ||
                        workFlowStatus == "canceled")
                    {
                        workFlowId.Add(workFlowIds);

                        var jobUrl = $"/api/v2/workflow/{workFlowIds}/job";

                        HttpResponseMessage jobResponse = await client.GetAsync(jobUrl);

                        string jobMessage = await jobResponse.Content.ReadAsStringAsync();
                        JObject jobObj = JObject.Parse(jobMessage);

                        var jobItems = jobObj.SelectToken("items");

                        foreach (var jobitem in jobItems)
                        {
                            string jobName = (string)jobitem.SelectToken("name");
                            string jobStatus = (string)jobitem.SelectToken("status");
                            if ((jobName == "beta_performance_test" ||
                                jobName == "beta-performance-tests" ||
                                jobName == "beta-spike-tests" ||
                                jobName == "beta-load-tests" ||
                                jobName == "beta_load_test") &&
                                (jobStatus == "success" ||
                                jobStatus == "failed" ||
                                jobStatus == "canceled"))
                            {
                                loadTestJobId = (string)jobitem.SelectToken("id");
                                loadTestJobName = (string)jobitem.SelectToken("name");
                                loadTestJobNumber = jobitem.SelectToken("job_number") != null ? (int)jobitem.SelectToken("job_number") : 0;
                                jobId.Add(loadTestJobId);

                                finalValue.Add("Pipe Line Id", pipeLineIds);
                                finalValue.Add("Work Flow Id", workFlowIds);
                                finalValue.Add("JobId", loadTestJobId);
                                finalValue.Add("JobName", loadTestJobName);
                                finalValue.Add("JobNumber", loadTestJobNumber);
                                break;

                            }
                            if (loadTestJobId != null)
                            {
                                break;
                            }
                        }
                        if (loadTestJobId != null)
                        {
                            break;
                        }
                    }
                }

                if (loadTestJobId != null)
                {
                    break;
                }
            }

            return (finalValue.ToString());
        }



        public async Task<string> AddEnvironmentVariables(string projectRepo, string projectName, EnvVariablesAsHeaders envVariable)
        {

            List<string> errors = new List<string>();
            /*EnvVariablesAsHeadersValidators validator = new EnvVariablesAsHeadersValidators();
            ValidationResult results = validator.Validate(envVariable);
            if (results.IsValid == false)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    errors.Add(failure.ErrorMessage);
                    Console.WriteLine(failure.ErrorMessage);
                }
            }
            foreach (var item in errors)
            {
                Console.WriteLine(item);
            }*/

            var url = $"/api/v2/project/gh/{projectRepo}/{projectName}/envvar";
            client.BaseAddress = new Uri(config["Uri"]);
            client.DefaultRequestHeaders.Add("Circle-Token", config["Circle-Token"]);
            client.DefaultRequestHeaders.Add("Authorization", config["Authorization"]);

            Dictionary<string, object> dcircleVariables = new Dictionary<string, object>();

            dcircleVariables.Add("LOADTEST_DURATION_PER_STAGE", envVariable.LOADTEST_DURATION_PER_STAGE);
            dcircleVariables.Add("LOADTEST_ENVIRONMENT", envVariable.LOADTEST_ENVIRONMENT);
            dcircleVariables.Add("LOADTEST_MAXVU", envVariable.LOADTEST_MAXVU);
            dcircleVariables.Add("LOADTEST_MAX_TARGET_DURATION", envVariable.LOADTEST_MAX_TARGET_DURATION);
            dcircleVariables.Add("LOADTEST_MAX_TPS", envVariable.LOADTEST_MAX_TPS);
            dcircleVariables.Add("LOADTEST_STAGES", envVariable.LOADTEST_STAGES);
            dcircleVariables.Add("LOAD_TESTS", envVariable.LOAD_TESTS);

            foreach (KeyValuePair<string, object> c in dcircleVariables)
            {
                EnvVariable eVar = new EnvVariable();
                eVar.name = c.Key;
                eVar.value = c.Value.ToString();
                Console.WriteLine(eVar);
                //await this.PostEnvironmentVariable(projectRepo, projectName, eVar);
                var json = JsonConvert.SerializeObject(eVar);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
            }
            return "skvjbs";

            //var content = new StringContent((string)json, Encoding.UTF8, "application/json");

            //Console.WriteLine(response);

        }

    }
}
