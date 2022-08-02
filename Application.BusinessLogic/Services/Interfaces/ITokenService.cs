using Application.Common.Models;
using Application.Model.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Application.BusinessLogic.Services.Interfaces
{
    public interface ITokenService
    {
        TokenModel GetToken(User user);
        TokenModel Refresh(RefreshTokenModel model);
    }
}
