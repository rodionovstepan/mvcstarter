namespace MvcStarter.Web
{
    using System.Web.Mvc;
    using App.Controllers;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using PostgreSql;
    using PostgreSql.Impl;

    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes
                    .FromAssemblyContaining<HomeController>()
                    .BasedOn<Controller>()
                    .LifestyleTransient()
                );

            container.Register(
                Component.For<IConnectionFactory>().ImplementedBy<PostgresqlConnectionFactory>().LifestyleSingleton()
                );
        }
    }
}