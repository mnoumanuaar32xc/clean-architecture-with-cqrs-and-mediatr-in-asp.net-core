using DemoApplication.Common.Interfaces.Services.AppUserServices;

namespace DemoInfrastructure.Services
{
    public sealed class AppUserService : IAppUserService
    {
        public Task<bool> IsAppUserTokenActiveAsync(string appUserGuid, string token, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsClientTokenActiveAsync(string clientGuid, string token, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RevokeAppUserTokenAsync(string appUserGuid, string token, CancellationToken cancellationToken = default)
        {
            // get user from cache _distributedCacheService
            throw new NotImplementedException();
        }

        public Task RevokeClientTokenAsync(string clientGuid, string token, CancellationToken cancellationToken = default)
        {
            // get token from _distributedCacheService
            throw new NotImplementedException();
        }
    }
}
