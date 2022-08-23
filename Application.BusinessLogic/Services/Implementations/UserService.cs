using Application.BusinessLogic.Services.Interfaces;
using Application.Common.Models;
using Application.Model;
using Application.Model.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
            var updatedUser = _mapper.Map<UpdateUserModel, User>(model);
            _context.Users.Update(updatedUser);
            _context.SaveChanges();
        }
        
        public List<UserModel> Get()
        {
            return _mapper.Map<List<User>,List<UserModel>>(_context.Users.AsNoTracking().ToList());
        }

        public UserModel Get(int id)
        {
            var user = _context.Users.AsNoTracking().SingleOrDefault(x => x.Id == id);
            return _mapper.Map<User, UserModel>(user);

        }
    }
}
