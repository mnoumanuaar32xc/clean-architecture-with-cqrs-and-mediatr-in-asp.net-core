using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.ApplicationSettings
{
    public class SiteSettings
    {
        public JwtSettings JwtSettings { get; set; }
        public IdentitySettings IdentitySettings { get; set; }
        public bool ActivateSwagger { get; set; } = false;
        public List<string> CorsEnableUris { get; set; } = new List<string>();
    }
}
