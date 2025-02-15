using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Common.Helpers
{
    public class DemoAppSettings
    {
        public string Logging { get; set; }

    }
    public class AppConnectionStringSettings
    {
        public string DefaultConnection { get; set; }
        public string DefaultReadOnlyConnection { get; set; }

        public string OldSystemConnection { get; set; }

        public string OldSystemReadOnlyConnection { get; set; }
        public string OracleConnection { get; set; }
        public string OracleReadOnlyConnection { get; set; }
    }
}
