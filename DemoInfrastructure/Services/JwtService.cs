using CommonLibrary.ApplicationSettings;
using DemoApplication.App.Users.DTOs;
using DemoApplication.Common.Interfaces.Jwt;
using DemoDomain.Entites.DemoApp.AppUserEntites;
using DemoDomain.Enums.AppUser;
using DemoDomain.Enums.DemoApp.Authentication.Jwt;
using DemoDomain.Enums.General.Application;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedModels.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DemoInfrastructure.Services
{
    public class JwtService : IJwtService, IScopedDependency
    {
        private readonly SiteSettings _siteSetting;
        // private readonly IUserPermissionService _userPermissionService;


        public JwtService(IOptions<SiteSettings> settings)
        {
            _siteSetting = settings.Value;
        }
        public async Task<AccessToken> GenerateAppUserJwtAsync(BaseAppUserDTO appUser, List<AppUserTypes> appUserTypes, List<AppUserIdentifiers> appUserIdentifiers, List<AppUserPermissions>? appUserPermissions = null, CancellationToken cancellationToken = default)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.SecretKey); // longer that 16 character

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.EncryptKey); //must be 16 character

            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = await GetAppUserClaimsAsync(appUser, appUserTypes, appUserIdentifiers, appUserPermissions, cancellationToken);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _siteSetting.JwtSettings.Issuer,
                Audience = _siteSetting.JwtSettings.Audience,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow.AddMinutes(_siteSetting.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.UtcNow.AddMinutes(_siteSetting.JwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

            return new AccessToken(securityToken: securityToken, refreshToken: GenerateRefreshToken(), refreshTokenExpiresIn: _siteSetting.JwtSettings.RefreshTokenValidityInDays);
        }

        public AccessToken GenerateClientJwtAsync(string clientGuid, string clientName, CancellationToken cancellationToken = default)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.SecretKey); // longer that 16 character

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.EncryptKey); //must be 16 character

            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, clientGuid),
                new("ClientId", clientGuid),
                new("AccessType", $"{(int) EnumAccessApplicationUserTypes.ClientApplication}"),
                new("ClientNameEn", clientName)
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _siteSetting.JwtSettings.Issuer,
                Audience = _siteSetting.JwtSettings.Audience,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow.AddMinutes(_siteSetting.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.UtcNow.AddMinutes(_siteSetting.JwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

            return new AccessToken(securityToken: securityToken, refreshToken: GenerateRefreshToken(), refreshTokenExpiresIn: _siteSetting.JwtSettings.RefreshTokenValidityInDays);
        }

        public async Task<long?> ValidateJwtAccessTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.SecretKey); // longer that 16 character
            var encryptionKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.EncryptKey); //must be 16 character

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var validatedToken = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                });

                if (validatedToken != null)
                {
                    return Convert.ToInt64(validatedToken.Claims.First(claim => claim.Key == ClaimTypes.NameIdentifier).Value);
                }
            }
            catch
            {
                // ignored
            }

            return null;
        }

        public async Task<long?> ReadIdFromJwtAccessTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            long? _appUserId = null;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var readToken = tokenHandler.ReadJwtToken(token);
                if (readToken != null)
                {
                    var userIdClaim = readToken.Claims.FirstOrDefault(c => c.Type == EnumStandardJwtClaimTypes.NameId);
                    if (userIdClaim != null)
                    {
                        _appUserId = Convert.ToInt64(userIdClaim.Value);
                    }
                }
            }
            catch
            {
                // ignored
            }

            return await Task.FromResult(_appUserId);
        }

        public async Task<string?> ReadGuidFromJwtAccessTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            string? _appUserGuid = null;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var readToken = tokenHandler.ReadJwtToken(token);
                if (readToken != null)
                {
                    var userIdClaim = readToken.Claims.FirstOrDefault(c => c.Type == EnumStandardJwtClaimTypes.NameId);

                    if (userIdClaim != null)
                    {
                        _appUserGuid = userIdClaim.Value;
                    }
                }
            }
            catch
            {
                // ignored
            }

            return await Task.FromResult(_appUserGuid);
        }

        public async Task<EnumAccessApplicationUserTypes> GetTypeOfAccessApplicationUserAsync(string token, CancellationToken cancellationToken = default)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var readToken = tokenHandler.ReadJwtToken(token);

                if (readToken != null)
                {
                    var isValidAccessUserType = Enum.TryParse(readToken.Claims.FirstOrDefault(c => c.Type == "AccessType")?.Value, out EnumAccessApplicationUserTypes accessUserType);

                    if (isValidAccessUserType)
                        return await Task.FromResult(accessUserType);
                }

                return await Task.FromResult(EnumAccessApplicationUserTypes.Unknown);
            }
            catch
            {
                return await Task.FromResult(EnumAccessApplicationUserTypes.Unknown);
            }
        }

        private async Task<IEnumerable<Claim>> GetAppUserClaimsAsync(BaseAppUserDTO appUser, List<AppUserTypes> appUserTypes, List<AppUserIdentifiers> appUserIdentifiers, List<AppUserPermissions>? appUserPermissions = null, CancellationToken cancellationToken = default)
        {
            var _isMinistryUser = appUserTypes.FirstOrDefault(e => e.AppUserTypeId == EnumAppUserTypes.SystemUser.Value);

            var _claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, appUser.AppUserGuid.ToString()),
                new("AppUserGuid", appUser.AppUserGuid.ToString()),
                new("AccessType", $"{(int) EnumAccessApplicationUserTypes.InternalApplicationUser}"),
                new("AppUserNameEn", appUser.AppUserNameEn),
                new("AppUserNameAr", appUser.AppUserNameAr),
                new("IsMinistryUser", _isMinistryUser != null ? "1" : "0"),
            };

            

            return await Task.FromResult(_claims);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
