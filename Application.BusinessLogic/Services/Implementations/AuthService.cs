using Application.BusinessLogic.Services.Interfaces;
using Application.Common.Helpers.Criptography;
using Application.Common.Models;
using Application.Model;
using Application.Model.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AuthService(ApplicationDatabaseContext context,
            ITokenService tokenService,
            IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _context = context;
        }
        public TokenModel Login(LoginModel loginModel)
        {
            var user = AuthUser(loginModel);
            return _tokenService.GetToken(user);

        }

        private User AuthUser(LoginModel loginModel)
        {
            var user = _context.Users
                .SingleOrDefault(x => x.Login == loginModel.Login);

            if (user == null) throw new Exception("Пользователь не найден.");

            if (PasswordHasher.VerifyPassword(
                user.PasswordSalt,
                user.PasswordHash,
                loginModel.Password))
            {
                return user;
            }
            throw new Exception("Неверный пароль.");
        }

        public bool Register(RegisterModel registerModel)
        {
            var user = _mapper.Map<RegisterModel, User>(registerModel);
            PasswordHasher.HashPassword(registerModel.Password, out byte[] passwordHash,
                out byte[] passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            _context.Users.Add(user);
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool UserExists(string login)
        {
            return _context.Users
                .AsNoTracking()
                .Any(x => x.Login == login.ToLower()) ? true : false;
        }

        public TokenModel Refresh(RefreshTokenModel refreshTokenModel)
        {
            return _tokenService.Refresh(refreshTokenModel);
        }
    }
}
