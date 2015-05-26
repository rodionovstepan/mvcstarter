namespace MvcStarter.PostgreSql.Impl
{
    using System.Configuration;
    using System.Data;
    using Npgsql;

    public class PostgresqlConnectionFactory : IConnectionFactory
    {
        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
        }
    }
}