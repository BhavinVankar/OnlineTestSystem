using OnlineTestSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Services.Abstraction
{
    public interface IUserHelper
    {
        void AddUser(UserModel userModel);
        void DeleteUser(Guid userId);
        List<UserModel> GetAllUserData();
        UserModel GetUserById(Guid userId);
        void UpdateUser(UpdateUserModel userModel);
    }
}
