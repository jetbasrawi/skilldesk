using System.Web.Mvc;
using System.Web.Routing;
using SkillDesk.Domain.Entities;
using SkillDesk.Web.Infrastructure;

namespace SkillDesk.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               null,
               "",
               new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
               null,
               "SkillProfile/Copy/{*skillPath}",
               new { controller = "EditProfile", action = "Copy" }
           );

            routes.MapRoute(
                null,
                "SkillProfile/Delete/{*skillPath}",
                new { controller = "EditProfile", action = "Delete" }
            );

            routes.MapRoute(
                null,
                "SkillProfile/Move/{*skillPath}",
                new { controller = "EditProfile", action = "Move" }
            );

            routes.MapRoute(
                null,
                "SkillProfile/AddSkill/{*skillPath}",
                new { controller = "SkillProfile", action = "AddSkill" }
            );

           routes.MapRoute(
                null,
                "SkillProfile/{*skillPath}",
                new { controller = "SkillProfile", action = "Index" }
            );

            routes.MapRoute(
                null, 
                "{controller}/{action}"
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            ModelBinders.Binders.Add(typeof(SkillProfile), new SkillProfileModelBinder());
        }
    }
}