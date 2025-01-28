using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineTestSystem.DataAccess.Abstraction;
using OnlineTestSystem.Models;
using OnlineTestSystem.Models.Common;
using OnlineTestSystem.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Services.Repository
{
    public class AccountHelper : IAccountHelper
    {
        private readonly IAccountRepository _accountRepository; 
        private readonly AppSettings _appSettings;

        public AccountHelper(IAccountRepository accountRepository, IOptions<AppSettings> appSettings)
        {
            _accountRepository = accountRepository;
            _appSettings = appSettings.Value;
        }
        public bool CheckEmailExists(string emailAddress)
        {
            var userInfo = _accountRepository.CheckEmailExists(emailAddress);
            if (userInfo != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public UserModel SignIn(SignInModel loginModel)
        {
            var userInfo = _accountRepository.SignIn(loginModel);
            if(userInfo != null)
            {
                return userInfo;
            }
            else
            {
                return null;
            }
        }
        public string GenerateToken(UserModel userInfo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.JWTTokenGenKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.PrimarySid, Convert.ToString(userInfo.UserId.ToString())),
                    new Claim(ClaimTypes.Name, userInfo.Name.ToString()),
                    new Claim(ClaimTypes.Role, userInfo.Role.ToString()),
                    new Claim(ClaimTypes.Email, userInfo.EmailAddress.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMonths(6), // Set token expiration to 6 months from now
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
