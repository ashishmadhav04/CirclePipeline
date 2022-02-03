using System;
using System.Threading.Tasks;
using CirclePipeline.Model;
using Xunit;

namespace CirclePipeline.APITests
{
    public class UnitTest1: Various_API_Calls
    {
        [Theory]
        [InlineData("ashishmadhav04", "CirclePipeline")]
        public async Task Assertions(string projectRepo, string projectName)
        {
            var response = await GetProjectDetails(projectRepo, projectName);
            Assert.Matches(response.organization_name, "ashishmadhav04");
        }

        [Theory]
        [InlineData("b85b9a3a-ed9c-4147-aeb0-5b22a5387cd5")]
        public async Task Assertions1(string pipeineId)
        {
            var response = await GetPipelineWorkflows(pipeineId);
            Assert.Null(response.next_page_token);
            Assert.Equal("f0c25aaa-4f8e-4128-8fdc-b9b4aff4da4f", response.items[0].id);
        }

        [Theory]
        [InlineData("ashishmadhav04", "CirclePipeline")]
        public async Task Assertions2(string projectRepo, string projectName)
        {
            EnvVariable body = new EnvVariable()
            {
                name = "Variable",
                value = "Variable_Value"
            };

            var response = await AddEnvironmentVariable(projectRepo, projectName, body);
            Console.WriteLine(response);

            Assert.Equal(body.name, response.name);
        }
    }
}