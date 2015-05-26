namespace MvcStarter.PostgreSql.Extensions
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Dapper;

    public static class DapperExtensions
    {
        public static IEnumerable<T> Query<T>(this IDbConnection connection, QueryObject queryObject,
            IDbTransaction transaction = null, bool buffered = true, int? timeout = null, CommandType? type = null)
        {
            return connection.Query<T>(queryObject.Sql, queryObject.QueryParams, transaction, buffered, timeout, type);
        }

        public static int Execute(this IDbConnection connection, QueryObject queryObject,
            IDbTransaction transaction = null, int? timeout = null, CommandType? type = null)
        {
            return connection.Execute(queryObject.Sql, queryObject.QueryParams, transaction, timeout, type);
        }

        public static T Single<T>(this IDbConnection connection, QueryObject queryObject,
            IDbTransaction transaction = null,
            bool buffered = true, int? timeout = null, CommandType? type = null)
        {
            return Query<T>(connection, queryObject, transaction, buffered, timeout, type).FirstOrDefault();
        }
    }
}