using Npgsql;
using Oracle.ManagedDataAccess.Client;
using Started.Project.RabbitMq.Shared;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Started.Project.RabbitMq.Infra.DataContexts
{
    public class DataContext : IDisposable
    {
        public IDbConnection Connection;
        public ConnectionType Type;

        public DataContext()
        {
            CreateConnection();
        }

        private void CreateConnection()
        {
            if (Connection == null)
            {
                switch (System.Enum.Parse(typeof(ConnectionType), Settings.DbType.ToUpper()))
                {
                    case ConnectionType.SQLSERVER:
                        Type = ConnectionType.SQLSERVER;
                        Connection = new SqlConnection(Settings.ConnectionString);
                        break;
                    case ConnectionType.POSTGRESQL:
                        Type = ConnectionType.POSTGRESQL;
                        Connection = new NpgsqlConnection(Settings.ConnectionString);
                        break;
                    case ConnectionType.ORACLE:
                        Type = ConnectionType.ORACLE;
                        Connection = new OracleConnection(Settings.ConnectionString);
                        break;
                    default:
                        throw new NotImplementedException("DB_TYPE");
                }
                Connection.Open();
            }
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}