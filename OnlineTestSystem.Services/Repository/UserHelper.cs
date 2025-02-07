using OnlineTestSystem.DataAccess.Abstraction;
using OnlineTestSystem.Models;
using OnlineTestSystem.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Services.Repository
{
    public class UserHelper : IUserHelper
    {
        private readonly IUserRepository _userRepository;
        public UserHelper(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(UserModel userModel)
        {
            _userRepository.AddUser(userModel);
        }

        public void DeleteUser(Guid userId)
        {
            _userRepository.DeleteUser(userId);
        }

        public List<UserModel> GetAllUserData()
        {
            var allUsers = _userRepository.GetAllUserData();
            return allUsers;
        }
        public List<UserModel> GetAllUsers()
        {
            var allUsers = _userRepository.GetAllUsers();
            return allUsers;
        }

        public UserModel GetUserById(Guid userId)
        {
            var userInfo = _userRepository.GetUserById(userId);
            return userInfo;
        }

        public void UpdateUser(UpdateUserModel userModel)
        {
            _userRepository.UpdateUser(userModel);
        }
    }
}
