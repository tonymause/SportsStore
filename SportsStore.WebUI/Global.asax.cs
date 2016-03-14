namespace SportsStore.WebUI
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Domain.Entities;
    using Infrastructure.Binders;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}