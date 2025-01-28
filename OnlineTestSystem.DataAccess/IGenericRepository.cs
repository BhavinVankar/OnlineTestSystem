using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.DataAccess.StoredProcedureDbAccess
{
    public interface IGenericRepository<TEntity>
    {
        IDbConnection GetOpenConnection();
        TEntity GetSingle(int aSingleId);
        IEnumerable<TEntity> GetAll();

    }
}
