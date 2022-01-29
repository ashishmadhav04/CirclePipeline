using System;
namespace CirclePipeline.Model
{
    public class ProjectDetails
    {
        public string slug { get; set; }
        public string organization_name { get; set; }
        public string name { get; set; }
        public vcs_info vcs_Info { get; set; }
    }
    public class vcs_info
    {
        public string vcs_url { get; set; }
        public string default_branch { get; set; }
        public string provider { get; set; }
    }
}
