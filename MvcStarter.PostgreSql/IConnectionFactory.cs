namespace MvcStarter.PostgreSql
{
    using System.Data;

    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}