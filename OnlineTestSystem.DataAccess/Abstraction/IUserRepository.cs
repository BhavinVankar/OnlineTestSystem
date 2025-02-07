using OnlineTestSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.DataAccess.Abstraction
{
    public interface IUserRepository
    {
        void AddUser(UserModel userModel);
        void DeleteUser(Guid userId);
        List<UserModel> GetAllUserData();
        List<UserModel> GetAllUsers();
        UserModel GetUserById(Guid userId);
        void UpdateUser(UpdateUserModel updateUserModel);
    }
}
