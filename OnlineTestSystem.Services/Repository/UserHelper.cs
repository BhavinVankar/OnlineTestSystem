using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserHelper(IUserRepository userRepository,
                          IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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
            return _userRepository.GetAllUserData();
        }
        public List<UserModel> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
        public UserModel GetUserById(Guid userId)
        {
            return _userRepository.GetUserById(userId);
        }
        public UpdateUserModel GetEditUserById(Guid userId)
        {
            return _mapper.Map<UserModel, UpdateUserModel>(_userRepository.GetUserById(userId));
        }
        public void UpdateUser(UpdateUserModel userModel)
        {
            _userRepository.UpdateUser(userModel);
        }
    }
}
