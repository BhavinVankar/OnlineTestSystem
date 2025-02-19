﻿using OnlineTestSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Services.Abstraction
{
    public interface IAccountHelper
    {
        bool CheckEmailExists(string emailAddress);
        bool CheckEmailExistsByUserId(string role, string emailAddress, Guid userId);
        string GenerateToken(UserModel userInfo);
        UserModel SignIn(SignInModel loginModel);
    }
}
