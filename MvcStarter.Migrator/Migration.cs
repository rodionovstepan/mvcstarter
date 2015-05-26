namespace MvcStarter.Migrator
{
    using System.Collections.Generic;
    using System.Data;
    using PostgreSql;
    using PostgreSql.Extensions;

    public abstract class Migration
    {
        public abstract long Id { get; }
        public abstract void MigrationScenario();

        private IDbConnection Connection { get; set; }
        private IDbTransaction Transaction { get; set; }

        protected IEnumerable<T> Query<T>(string sql, object param = null)
        {
            return Connection.Query<T>(new QueryObject(sql, param), Transaction);
        }

        protected IEnumerable<T> Query<T>(QueryObject query)
        {
            return Connection.Query<T>(query, Transaction);
        }

        protected void Execute(string sql, object param = null)
        {
            Connection.Execute(new QueryObject(sql, param), Transaction);
        }

        protected void Execute(QueryObject query)
        {
            Connection.Execute(query, Transaction);
        }

        public void Run(IDbConnection connection, IDbTransaction transaction)
        {
            Connection = connection;
            Transaction = transaction;

            MigrationScenario();

            Execute("INSERT INTO migrations (id, migrated) VALUES (" + Id + ", now());");
        }
    }
}