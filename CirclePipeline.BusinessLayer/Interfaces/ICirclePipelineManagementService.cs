using System.Collections.Generic;
using System.Threading.Tasks;
using CirclePipeline.Model;

namespace CirclePipeline.BusinessLayer.Interfaces
{
    public interface ICirclePipelineManagementService
    {
        Task<object> GetProjectDetails(string projectRepo, string projectName);
        Task<string> GetProjectPipeline(string projectRepo, string projectName);
        Task<string> GetSinglePipeline(string pipelineId);
        Task<string> GetPipelineWorkflows(string pipelineId);
        Task<string> GetWorkflow(string workflowId);
        Task<string> GetWorkflowJobs(string workflowId);
        Task<string> PostReRunJobs(string workflowId, JobRerun jobRerun);
        Task<string> PostApproveJob(string workflowId, string approvalId);
        Task<string> PostCancelJob(string projectRepo, string projectName, string jobId);
        Task<string> PostEnvironmentVariable(string projectRepo, string projectName, EnvVariable envVariable);
        Task<string> DeleteEnvironmentVariable(string projectRepo, string projectName, string envVariableName);
        Task<List<string>> GetPipelineId(string projectRepo, string projectName);
        Task<List<string>> GetWorkflowsId(string pipelineId);
        Task<List<string>> GetJobsId(string workflowId);
        Task<string> RunJobs(string projectRepo, string projectName);

        Task<string> AddEnvironmentVariables(string projectRepo, string projectName, EnvVariablesAsHeaders envVariable);

    }
}
