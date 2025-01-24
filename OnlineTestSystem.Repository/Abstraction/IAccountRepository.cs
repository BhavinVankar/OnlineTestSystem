using OnlineTestSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Repository.Abstraction
{
    public interface IAccountRepository
    {
     UserModel GetUserDetails(SignInModel signInModel);
    }
}
