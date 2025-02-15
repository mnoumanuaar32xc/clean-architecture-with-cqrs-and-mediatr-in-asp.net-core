using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.ApplicationSettings
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string EncryptionSecret { get; set; }
        public string LegacyIntegrationApiKey { get; set; }

    }
}
