using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CirclePipeline.BusinessLayer.Interfaces;

namespace GitPipeline
{

    //[Produces("application/json")]
    [ApiController]
    [Route("/api/v3/")]
    public class GitPipelineController : ControllerBase
    {
        private readonly IGitPipelineManagementService gitPipelineManagementService;

        public GitPipelineController(IGitPipelineManagementService gitPipelineManagementService)
        {
            this.gitPipelineManagementService = gitPipelineManagementService;
        }

        [HttpGet("repos/{projectRepo}/{projectName}/contents/Tests/LoadTest.js")]
        public async Task<object> GetTestNames([Required] string projectRepo, [Required] string projectName, [Required] string gitToken)
        {
            var getPipelineId = await this.gitPipelineManagementService.GetTestNames(projectRepo, projectName, gitToken);
            return this.Ok(getPipelineId);
        }

    }
}
