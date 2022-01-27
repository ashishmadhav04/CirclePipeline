using System.Collections.Generic;

namespace CirclePipeline.Model
{
    public class JobRerun
    {
        public bool from_failed { get; set; } = false;
        public bool sparse_tree { get; set; } = false;
        public List<string> jobs { get; set; }
    }
}
