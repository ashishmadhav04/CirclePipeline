using System.Collections.Generic;
using System.Threading.Tasks;

namespace CirclePipeline.BusinessLayer.Interfaces
{
    public interface IGitPipelineManagementService
    {
        Task<List<string>> GetTestNames(string projectRepo, string projectName, string path);

    }
}
