namespace MvcStarter.App.Queries
{
    using PostgreSql;

    public class UserQuery
    {
        public static QueryObject ById(int id)
        {
            return new QueryObject("SELECT * FROM users WHERE id = @Id", new {Id = id});
        }
    }
}