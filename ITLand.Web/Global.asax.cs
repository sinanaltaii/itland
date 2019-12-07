using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ITLand.Web
{
    public class Global : EPiServer.Global
    {
        protected void Application_Start()
        {
            var razorViewEngine = ViewEngines.Engines.OfType<RazorViewEngine>().First();
            RazorViewEngineConfig.RegisterViewLocations(razorViewEngine);
            RazorViewEngineConfig.RegisterPartialViewLocations(razorViewEngine);
            AreaRegistration.RegisterAllAreas();
            MvcHandler.DisableMvcResponseHeader = true;

            //var user = Membership.CreateUser("admin", "!QAZxsw2", "sinan.altaii@live.se");
            //user.IsApproved = true;
            //if (!Roles.RoleExists("Administrators"))
            //{
            //    Roles.CreateRole("Administrators");
            //}

            //if (!Roles.IsUserInRole("admin", "Administrators"))
            //{
            //    Roles.AddUserToRole("admin", "Administrators");
            //}
        }

        protected override void RegisterRoutes(RouteCollection routes)
        {
            base.RegisterRoutes(routes);
            RouteConfig.RegisterRoutes(routes);
        }
    }
}