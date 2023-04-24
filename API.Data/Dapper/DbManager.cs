using System.Data;
using Microsoft.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using static API.Model.Models.Enums;
using API.Data.Interfaces;

namespace API.Data.Dapper
{
    public class DbManager : IDbManager
    {
        private readonly IConfiguration _configuration;

        public DbManager(IConfiguration config)
        {
            _configuration = config;

        }

        private string GetConnectionString(EnumDB Name, DbAccessLevel AccessLevel)
        {
            var sb = new StringBuilder();
            sb.Append(Name.ToString());

            if (AccessLevel == DbAccessLevel.WRITE)
            {
                sb.Append("Write");
            }
            else
            {
                sb.Append("Read");
            }
            return ConfigurationExtensions.GetConnectionString(_configuration, sb.ToString()).ToString();
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
