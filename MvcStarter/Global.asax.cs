namespace MvcStarter.Web
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Castle.Windsor;

    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly WindsorContainer _container = new WindsorContainer();

        protected void Application_Start()
        {
            _container.Install(new WindsorInstaller());

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(_container));
        }
    }
}