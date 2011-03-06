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

            routes.MapRoute(null, 
                                "", // Only matches the empty URL (i.e. ~/)
                                new { controller = "Skills", action = "List", category = (string)null, page = 1 }
            );

            routes.MapRoute(null,
                           "{category}", // Matches ~/Football or ~/AnythingWithNoSlash
                           new { controller = "Skills", action = "List", page = 1 }
           );

            routes.MapRoute(null,
                                "{category}/Page{page}", // Matches ~/Football/Page567
                                new { controller = "Skills", action = "List" }, // Defaults
                                new { page = @"\d+" } // Constraints: page must be numerical
            );

            routes.MapRoute(null, // No need to give it a name
                                "Page{page}", // Matches ~/Page2, ~/Page123, but not ~/PageXYZ
                                new { controller = "Skills", action = "List", category = (string)null }, // Where the URL goes to
                                new { page = @"\d+" } // Constraints: page must be numerical
            );

            //routes.MapRoute(null,
            //                    "{category}/{skillName}", // Matches ~/Football/Goalkeeping
            //                    new { controller = "Skills", action = "Detail" } // Defaults
            //);

            routes.MapRoute(null, "{controller}/{action}");

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