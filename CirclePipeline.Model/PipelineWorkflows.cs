using System.Collections.Generic;

namespace CirclePipeline.Model
{
    public class PipelineWorkflows
    {
        public string next_page_token { get; set; }
        public List<Items> items { get; set; }
    }

    public class Items
    {
        public string pipeline_id { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string project_slug { get; set; }
        public string status { get; set; }
        public string started_by { get; set; }
        public int pipeline_number { get; set; }
        public string created_at { get; set; }
        public string stopped_at { get; set; }

    }
}
