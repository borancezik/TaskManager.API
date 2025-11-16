using TaskManager.Application.Utilities.Authorization.Model;

namespace TaskManager.Application.Interfaces.Helpers;


public interface ITokenHelper
{
    string GenerateToken(TokenModel tokenModel);
    TokenModel ValidateToken(string token);
}
