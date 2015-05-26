namespace MvcStarter.Tests.Live
{
    using NUnit.Framework;
    using PostgreSql;
    using PostgreSql.Extensions;
    using PostgreSql.Impl;

    [TestFixture]
    public class DapperLiveTests
    {
        [Test]
        public void ConnectionLiveTest()
        {
            var factory = new PostgresqlConnectionFactory();

            using (var connection = factory.CreateConnection())
                Assert.NotNull(connection);
        }

        [Test]
        public void SelectTest()
        {
            var factory = new PostgresqlConnectionFactory();
            var query = new QueryObject("SELECT * FROM users WHERE id = @Id", new {Id = 1});

            using (var connection = factory.CreateConnection())
            {
                var user = connection.Single<TestUser>(query);

                Assert.AreEqual(1, user.Id);
                Assert.AreEqual("rodionovstepan", user.Username);
            }
        }
    }

    class TestUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}