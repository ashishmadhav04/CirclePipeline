using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CirclePipeline.BusinessLayer.Interfaces;
using CirclePipeline.Model;

namespace CirclePipeline
{

    //[Produces("application/json")]
    [ApiController]
    [Route("/api/v2/")]
    public class CirclePipelineControllers : ControllerBase
    {
        private readonly ICirclePipelineManagementService circlePipelineManagementService;

        public CirclePipelineControllers(ICirclePipelineManagementService circlePipelineManagementService)
        {
            this.circlePipelineManagementService = circlePipelineManagementService;
        }

        [HttpGet("project/gh/{projectRepo}/{projectName}")]
        //[Route("/api/v2/project/gh/{projectRepo}/{projectName}")]
        public async Task<object> GetProjectDetails([Required] string projectRepo, [Required] string projectName)
        {
            var projectDetails = await this.circlePipelineManagementService.GetProjectDetails(projectRepo, projectName);
            return this.Ok(projectDetails);
        }

        [HttpGet("project/gh/{projectRepo}/{projectName}/pipeline")]
        //[Route("/api/v2/project/gh/{projectRepo}/{projectName}/pipeline")]
        public async Task<object> GetProjectPipeline([Required] string projectRepo, [Required] string projectName)
        {
            var getProjectPipeline = await this.circlePipelineManagementService.GetProjectPipeline(projectRepo, projectName);
            return this.Ok(getProjectPipeline);
        }


        [HttpGet("pipeline/{pipelineId}")]
        public async Task<object> GetSinglePipeline(string pipelineId)
        {
            var getSinglePipeline = await this.circlePipelineManagementService.GetSinglePipeline(pipelineId);
            return this.Ok(getSinglePipeline);
        }

        [HttpGet("pipeline/{pipelineId}/workflow")]
        public async Task<object> GetPipelineWorkflows(string pipelineId)
        {
            var getPipelineWorkflows = await this.circlePipelineManagementService.GetPipelineWorkflows(pipelineId);
            return this.Ok(getPipelineWorkflows);
        }

        [HttpGet("workflow/{workflowId}")]
        public async Task<object> GetWorkflow(string workflowId)
        {
            var getWorkflow = await this.circlePipelineManagementService.GetWorkflow(workflowId);
            return this.Ok(getWorkflow);
        }

        [HttpGet("workflow/{workflowId}/job")]
        public async Task<object> GetWorkflowJobs(string workflowId)
        {
            var getWorkflowJobs = await this.circlePipelineManagementService.GetWorkflowJobs(workflowId);
            return this.Ok(getWorkflowJobs);
        }

        [HttpPost("workflow/{workflowId}/rerun")]
        public async Task<object> PostReRunJobs(string workflowId, [FromBody] JobRerun jobRerun)
        {
            var postReRunJobs = await this.circlePipelineManagementService.PostReRunJobs(workflowId, jobRerun);
            return this.Ok(postReRunJobs);
        }

        [HttpPost("workflow/{workflowId}/approve/{approvalId}")]
        public async Task<object> PostApproveJob(string workflowId, string approvalId)
        {
            var postApproveJob = await this.circlePipelineManagementService.PostApproveJob(workflowId, approvalId);
            return this.Ok(postApproveJob);
        }

        [HttpPost("project/gh/{projectRepo}/{projectName}/job/{jobId}/cancel")]
        public async Task<object> PostCancelJob([Required] string projectRepo, [Required] string projectName, [Required] string jobId)
        {
            var postCancelJob = await this.circlePipelineManagementService.PostCancelJob(projectRepo, projectName, jobId);
            return this.Ok(postCancelJob);
        }

        [HttpPost("project/gh/{projectRepo}/{projectName}/envvar")]
        public async Task<object> PostEnvironmentVariable([Required] string projectRepo, [Required] string projectName, [FromBody] EnvVariable envVariable)
        {
            var postEnvironmentVariable = await this.circlePipelineManagementService.PostEnvironmentVariable(projectRepo, projectName, envVariable);
            return this.Ok(postEnvironmentVariable);
        }

        [HttpDelete("project/gh/{projectRepo}/{projectName}/envvar/{envVariableName}")]
        public async Task<object> DeleteEnvironmentVariable([Required] string projectRepo, [Required] string projectName, string envVariableName)
        {
            var deleteEnvironmentVariable = await this.circlePipelineManagementService.DeleteEnvironmentVariable(projectRepo, projectName, envVariableName);
            return this.Ok(deleteEnvironmentVariable);
        }



        [HttpPost("project/gh/{projectRepo}/{projectName}/envvars")]
        public async Task<object> AddEnvironmentVariables([Required] string projectRepo, [Required] string projectName, [Required][FromBody] EnvVariablesAsHeaders envVariable)
        {
            var postEnvironmentVariable = await this.circlePipelineManagementService.AddEnvironmentVariables(projectRepo, projectName, envVariable);
            return this.Ok(postEnvironmentVariable);
        }

        /**************************************************************************************************************************************************************/
        /**************************************************************************************************************************************************************/


        [HttpGet("project/gh/{projectRepo}/{projectName}/pipelineIds")]
        public async Task<object> GetPipelineId([Required] string projectRepo, [Required] string projectName)
        {
            var getPipelineId = await this.circlePipelineManagementService.GetPipelineId(projectRepo, projectName);
            return this.Ok(getPipelineId);
        }


        [HttpGet("pipeline/{pipelineId}/workflowId")]
        public async Task<object> GetWorkflowsId(string pipelineId)
        {
            var getWorkflowsId = await this.circlePipelineManagementService.GetWorkflowsId(pipelineId);
            return this.Ok(getWorkflowsId);
        }


        [HttpGet("workflow/{workflowId}/jobId")]
        public async Task<object> GetJobsId(string workflowId)
        {
            var getJobsId = await this.circlePipelineManagementService.GetJobsId(workflowId);
            return this.Ok(getJobsId);
        }

        [HttpPost("Run Jobs")]
        public async Task<object> RunJobs([Required] string projectRepo, [Required] string projectName)
        {
            var runJobs = await this.circlePipelineManagementService.RunJobs(projectRepo, projectName);
            return this.Ok(runJobs);
        }

    }
}
