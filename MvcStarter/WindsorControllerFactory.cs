namespace MvcStarter.Web
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Castle.Windsor;

    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer _container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return _container.Resolve(controllerType) as IController;
        }

        public override void ReleaseController(IController controller)
        {
            var disposableController = controller as IDisposable;
            if (disposableController != null)
                disposableController.Dispose();

            _container.Release(controller);
        }
    }
}