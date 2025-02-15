using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Services.LoggerService.Models
{
    public class LogScope
    {
        public string LogScopeId { get; }

        public LogScope()
        {
            LogScopeId = Guid.NewGuid().ToString();
        }
    }
}
