using Dapper;
using OnlineTestSystem.DataAccess.Abstraction;
using OnlineTestSystem.DataAccess.StoredProcedureDbAccess;
using OnlineTestSystem.Models;
using OnlineTestSystem.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.DataAccess.Repository
{
    public class UserRepository : SqlDbRepository<UserModel>, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public List<UserModel> GetAllUserData()
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            var userList = vconn.Query<UserModel>("sp_proc_GetAllUserData", vParams, commandType: CommandType.StoredProcedure);
            return userList.ToList();
        }
        public List<UserModel> GetAllUsers()
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            var userList = vconn.Query<UserModel>("sp_proc_GetAllUsers", vParams, commandType: CommandType.StoredProcedure);
            return userList.ToList();
        }
        public UserModel GetUserById(Guid userId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@UserId",userId);
            var userList = vconn.Query<UserModel>("sp_proc_GetUserById", vParams, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return userList;
        }
        public void DeleteUser(Guid userId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@UserId", userId);
            vconn.Execute("sp_proc_DeleteUser", vParams, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void AddUser(UserModel userModel)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@FirstName", userModel.FirstName);
            vParams.Add("@LastName", userModel.LastName);
            vParams.Add("@EmailAddress", userModel.EmailAddress);
            vParams.Add("@Password", AppEncrypt.CreateHash(userModel.Password));
            vParams.Add("@Role", AppConstants.Candidate);
            vParams.Add("@CreatedDate", DateTime.UtcNow);
            vconn.Execute("sp_proc_AddUser", vParams, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void UpdateUser(UpdateUserModel userModel)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@UserId", userModel.Id);
            vParams.Add("@FirstName", userModel.FirstName);
            vParams.Add("@LastName", userModel.LastName);
            vParams.Add("@EmailAddress", userModel.EmailAddress);
            vParams.Add("@UpdatedDate", DateTime.UtcNow);
            vconn.Execute("sp_proc_UpdateUser", vParams, commandType: System.Data.CommandType.StoredProcedure);

        }
    }
}
