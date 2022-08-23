using Application.Common.Models;

namespace Application.BusinessLogic.Services.Interfaces
{
    public interface IAuthService
    {
        TokenModel Login(LoginModel loginModel);
        TokenModel Refresh(RefreshTokenModel refreshTokenModel);
        bool Register(RegisterModel registerModel);
        bool UserExists(string login);
    }
}
