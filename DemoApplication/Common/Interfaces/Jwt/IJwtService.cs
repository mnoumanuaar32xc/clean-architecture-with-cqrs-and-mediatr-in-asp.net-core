using DemoApplication.App.Users.DTOs;
using DemoDomain.Entites.DemoApp.AppUserEntites;
using DemoDomain.Enums.General.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Common.Interfaces.Jwt
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAppUserJwtAsync(BaseAppUserDTO appUser, List<AppUserTypes> appUserTypes, List<AppUserIdentifiers> appUserIdentifiers, List<AppUserPermissions>? appUserPermissions = null, CancellationToken cancellationToken = default);
        AccessToken GenerateClientJwtAsync(string clientGuid, string clientName, CancellationToken cancellationToken = default);
        Task<long?> ValidateJwtAccessTokenAsync(string token, CancellationToken cancellationToken = default);
        Task<long?> ReadIdFromJwtAccessTokenAsync(string token, CancellationToken cancellationToken = default);
        Task<string?> ReadGuidFromJwtAccessTokenAsync(string token, CancellationToken cancellationToken = default);
        Task<EnumAccessApplicationUserTypes> GetTypeOfAccessApplicationUserAsync(string token, CancellationToken cancellationToken = default);

    }
}
