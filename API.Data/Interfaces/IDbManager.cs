using System.Data;

namespace API.Data.Interfaces
{
    public interface IDbManager
    {
        IDbConnection GetConnection();
        IDbConnection GetOpenConnection();
    }
}
