using Application.Common.Models;
using Application.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        List<UserModel> Get();
        UserModel Get(int id);
        void Edit(int userId, UpdateUserModel model);
        void Delete(int id);

    }
}
