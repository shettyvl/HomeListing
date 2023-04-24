using System.Data;

namespace API.Data.Dapper
{
    public interface IDbManager
    {
        IDbConnection GetConnection();
        IDbConnection GetOpenConnection();
    }
}
