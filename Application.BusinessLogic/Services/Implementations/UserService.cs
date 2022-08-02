using Application.BusinessLogic.Services.Interfaces;
using Application.Common.Models;
using Application.Model;
using Application.Model.Models;
using AutoMapper;

namespace Application.BusinessLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly IMapper _mapper;
        public UserService(ApplicationDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Delete(int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);
            if (user == null) throw new Exception("Пользователь не найден");
            _context.Users.Remove(user);
        }

        public void Edit(int userId, UpdateUserModel model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == userId);
            if (user == null) throw new Exception("Пользоватеть не найден");

        }

        public List<User> Get()
        {
            throw new NotImplementedException();
        }

        public UserModel Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
