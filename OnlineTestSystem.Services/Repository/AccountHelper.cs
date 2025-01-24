using OnlineTestSystem.Models;
using OnlineTestSystem.Repository.Abstraction;
using OnlineTestSystem.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Services.Repository
{
    public class AccountHelper : IAccountHelper
    {
        private readonly IAccountRepository _accountRepository;
        public AccountHelper(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public UserModel GetUserDetails(SignInModel signInModel)
        {
            var userInfo = _accountRepository.GetUserDetails(signInModel);
            if (userInfo != null)
            {
                return userInfo;
            }
            else
            {
                return null;
            }
        }
    }
}
