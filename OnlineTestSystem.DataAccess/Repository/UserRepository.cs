using OnlineTestSystem.DataAccess.Abstraction;
using OnlineTestSystem.DataAccess.StoredProcedureDbAccess;
using OnlineTestSystem.Models;
using System;
using System.Collections.Generic;
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
    }
}
