using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Common.Interfaces.Services.AppUserServices
{
    public interface IAppUserService
    {
        Task RevokeClientTokenAsync(string clientGuid, string token, CancellationToken cancellationToken = default);
        Task RevokeAppUserTokenAsync(string appUserGuid, string token, CancellationToken cancellationToken = default);
        Task<bool> IsClientTokenActiveAsync(string clientGuid, string token, CancellationToken cancellationToken = default);

        Task<bool> IsAppUserTokenActiveAsync(string appUserGuid, string token, CancellationToken cancellationToken = default);
    }
}
