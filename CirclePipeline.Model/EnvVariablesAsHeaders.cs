using System;
using Microsoft.AspNetCore.Mvc;

namespace CirclePipeline.Model
{
    public class EnvVariablesAsHeaders
    {
        public EnvVariablesAsHeaders() { }

        public string LOADTEST_ENVIRONMENT { get; set; }

        public string LOAD_TESTS { get; set; }

        public int LOADTEST_STAGES { get; set; }

        public string LOADTEST_DURATION_PER_STAGE { get; set; }

        public int LOADTEST_MAX_TPS { get; set; }

        public string LOADTEST_MAX_TARGET_DURATION { get; set; }

        public int LOADTEST_MAXVU { get; set; }
    }

}
