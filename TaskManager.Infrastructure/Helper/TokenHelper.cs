using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Application.Interfaces.Helpers;
using TaskManager.Application.Utilities.AppSettings;
using TaskManager.Application.Utilities.Authorization.Model;
using TaskManager.Application.Utilities.Constants;
using TaskManager.Application.Utilities.Exceptions;

namespace TaskManager.Infrastructure.Helper;

public class TokenHelper(IOptions<TaskManagerSettings> appSettings) : ITokenHelper
{
    private readonly JwtSettings _jwtSettings = appSettings.Value.JwtSettings;
    public string GenerateToken(Guid userId, string username, string email)
    {
        var issuer = _jwtSettings.Issuer;
        var audience = _jwtSettings.Audience;
        var secretKey = _jwtSettings.Secret;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
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
                UserId = long.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == "UserId").Value),
                Username = jwtSecurityToken.Claims.First(claim => claim.Type == "UserName").Value,
                ValidTo = Convert.ToDateTime(jwtSecurityToken.Claims.First(claim => claim.Type == "ValidTo").Value),
                RefreshTokenEndDate = Convert.ToDateTime(jwtSecurityToken.Claims.First(claim => claim.Type == "RefreshTokenEndDate").Value),
                RefreshToken = jwtSecurityToken.Claims.First(claim => claim.Type == "RefreshToken").Value,
            };

            if (personnel.ValidTo < DateTime.Now)
            {
                throw new UnauthorizedException(TokenConstant.EXPIRED_TOKEN);
            }

            return personnel;
        }
        catch
        {
            throw new UnauthorizedException(TokenConstant.INVALID_TOKEN);
        }
    }
}
