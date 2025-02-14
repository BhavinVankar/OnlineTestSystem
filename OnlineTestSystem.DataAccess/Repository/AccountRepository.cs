using Dapper;
using Microsoft.Extensions.Options;
using OnlineTestSystem.DataAccess.Abstraction;
using OnlineTestSystem.DataAccess.StoredProcedureDbAccess;
using OnlineTestSystem.Models;
using OnlineTestSystem.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.DataAccess.Repository
{
    public class AccountRepository : SqlDbRepository<UserModel>, IAccountRepository
    {
        private readonly RepositoryOptions repositoryOptions;
        public AccountRepository(IOptions< RepositoryOptions> repositoryOptions) : base(repositoryOptions.Value.DefaultConnection)
        {
            this.repositoryOptions = repositoryOptions.Value;
        }
        public UserModel CheckEmailExists(string emailAddress)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@EmailAddress", emailAddress);
            var result = vconn.QueryFirstOrDefault<UserModel>("sp_proc_CheckEmailExists", vParams, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public UserModel CheckEmailExistsByUserId(string role, string emailAddress, Guid userId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@UserId", userId);
            vParams.Add("@Role", role);
            vParams.Add("@EmailAddress", emailAddress);
            var result = vconn.QueryFirstOrDefault<UserModel>("sp_proc_CheckEmailExistsByUserId", vParams, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public UserModel GetUserDetails(SignInModel signInModel)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@email", signInModel.EmailAddress);
            var result = vconn.QueryFirstOrDefault<UserModel>("sp_proc_GetUserDetails", vParams, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public UserModel SignIn(SignInModel loginModel)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Email", loginModel.EmailAddress);
            vParams.Add("@Password", AppEncrypt.CreateHash(loginModel.Password));
            var result = vconn.QueryFirstOrDefault<UserModel>("sp_proc_GetLoginUser", vParams, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
