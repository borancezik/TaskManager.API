using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Application.Interfaces.Helpers;
using TaskManager.Application.Utilities.AppSettings;
using TaskManager.Application.Utilities.Authorization.Model;
using TaskManager.Application.Utilities.Constants;
using TaskManager.Application.Utilities.Errors.ServiceErrors;
using TaskManager.Application.Utilities.Exceptions;

namespace TaskManager.Infrastructure.Helper;

public class TokenHelper(IOptions<TaskManagerSettings> appSettings) : ITokenHelper
{
    private readonly IOptions<TaskManagerSettings> _appSettings = appSettings;
    public string GenerateToken(TokenModel tokenModel)
    {
        var issuer = _appSettings.Value.JwtSettings.Issuer;
        var audience = _appSettings.Value.JwtSettings.Audience;
        var secretKey = _appSettings.Value.JwtSettings.Secret;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("UserId", tokenModel.UserId.ToString()),
            new Claim("UserName", tokenModel.Username),
            new Claim("Email", tokenModel.Email),
            new Claim("ValidTo", tokenModel.ValidTo.ToString("O")),
            new Claim("RefreshTokenEndDate", tokenModel.RefreshTokenEndDate.ToString("O")),
            new Claim("RefreshToken", tokenModel.RefreshToken)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_appSettings.Value.JwtSettings.ExpiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public TokenModel ValidateToken(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            var personnel = new TokenModel
            {
                UserId = Guid.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == "UserId").Value),
                Username = jwtSecurityToken.Claims.First(claim => claim.Type == "UserName").Value,
                ValidTo = Convert.ToDateTime(jwtSecurityToken.Claims.First(claim => claim.Type == "ValidTo").Value),
                RefreshTokenEndDate = Convert.ToDateTime(jwtSecurityToken.Claims.First(claim => claim.Type == "RefreshTokenEndDate").Value),
                RefreshToken = jwtSecurityToken.Claims.First(claim => claim.Type == "RefreshToken").Value,
            };

            if (personnel.ValidTo < DateTime.Now)
            {
                throw new UnauthorizedException(AuthenticationErrors.ExpiredToken.ToString());
            }

            return personnel;
        }
        catch
        {
            throw new UnauthorizedException(AuthenticationErrors.InvalidToken.ToString());
        }
    }
}
