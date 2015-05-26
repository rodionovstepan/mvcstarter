namespace MvcStarter.App.Controllers
{
    using System.Web.Mvc;
    using Dtos;
    using PostgreSql;
    using PostgreSql.Extensions;
    using Queries;

    public class HomeController : Controller
    {
        private readonly IConnectionFactory _factory;

        public HomeController(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult User(int id)
        {
            using (var connection = _factory.CreateConnection())
            {
                var user = connection.Single<User>(UserQuery.ById(id));

                return View(user);
            }
        }
    }
}