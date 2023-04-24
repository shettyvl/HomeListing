using System.Data;
using Microsoft.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using static API.Model.Models.Enums;
using API.Data.Interfaces;
using API.Model.Utils;
using Microsoft.Extensions.Options;

namespace API.Data.Dapper
{
    public class DbManager : IDbManager
    {
        private readonly IOptions<AppConfig> _configuration;

        public DbManager(IOptions<AppConfig> options)
        {
            _configuration = options;

        }

        private string GetConnectionString(EnumDB Name, DbAccessLevel AccessLevel)
        {

            if (AccessLevel == DbAccessLevel.WRITE)
            {
                return _configuration.Value.TESTWrite;
            }
            else
            {
                return _configuration.Value.TESTRead;
            }
           
        }

        public IDbConnection GetConnection()
        {
            var conn = new SqlConnection(GetConnectionString(EnumDB.TEST, DbAccessLevel.READ));
            return conn;
        }

        public IDbConnection GetOpenConnection()
        {
            var conn = GetConnection();
            conn.Open();
            return conn;
        }
    }
}
