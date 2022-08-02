using Application.BusinessLogic.Services.Interfaces;
using Application.Common.Models;
using Application.Model;
using Application.Model.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.BusinessLogic.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly ApplicationDatabaseContext _context;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config,
            IMapper mapper,
            ApplicationDatabaseContext context)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:TokenKey"]));
        }
        public JwtSecurityToken CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.Login)
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);
            var Jwt = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds,
                notBefore: DateTime.UtcNow
                );
            return Jwt;
        }
        public TokenModel GetToken(User user)
        {
            var token = CreateToken(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedJwt = tokenHandler.WriteToken(token);
            var refreshToken = AddRefreshToken(user);
            var mappedUser = _mapper.Map<User, UserModel>(user);
            return new TokenModel
            {
                UserId = user.Id,
                Token = encodedJwt,
                RefreshToken = refreshToken.Id,
                Username = user.Login,
                User = mappedUser,
                ValidFrom = token.ValidFrom,
                ValidTo = token.ValidTo
            };
        }

        private RefreshToken AddRefreshToken(User user)
        {
            var now = DateTime.UtcNow;
            var token = new RefreshToken
            {
                Id = new JwtSecurityTokenHandler().WriteToken(CreateToken(user)),
                UserId = user.Id,
                ValidTo = now.AddDays(1),
                ValidFrom = now
            };
            _context.RefreshTokens.Add(token);
            _context.SaveChanges();
            return token;
        }
        public TokenModel Refresh(RefreshTokenModel model)
        {
            var refreshToken = _context.RefreshTokens
                .FirstOrDefault(x => x.Id == model.RefreshToken);

            if (refreshToken == null) throw new Exception("refreshIsExpired");
            if (refreshToken.ValidTo < DateTime.UtcNow) throw new Exception("refreshIsExpired");

            var user = _context.Users.FirstOrDefault(x => x.Login == model.Login);
            if (user == null) throw new Exception("Пользователь не найден");
            _context.RefreshTokens.Remove(refreshToken);
            _context.SaveChanges();
            return GetToken(user);
        }
    }
}
