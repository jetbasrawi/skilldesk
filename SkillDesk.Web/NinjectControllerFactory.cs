using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Concrete;
using SkillDesk.Domain.Services;

namespace SkillDesk.Web
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        // A Ninject "kernel" is the thing that can supply object instances
        private IKernel kernel = new StandardKernel(new SKillDeskServices());
        // ASP.NET MVC calls this to get the controller for each request
        protected override IController GetControllerInstance(RequestContext context,Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)kernel.Get(controllerType);
        }
        // Configures how abstract service types are mapped to concrete implementations
        private class SKillDeskServices : NinjectModule
        {
            public override void Load()
            {
                Bind<ISkillsRepository>()
                    .To<SqlSkillsRepository>()
                        .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["SkillDeskDb"].ConnectionString);

                Bind<ISkillProfileRepository>()
                    .To<XmlFileSkillProfileRepository>();

                Bind<IExperienceTypeRepository>()
                    .To<SqlExperienceTypeRepository>()
                        .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["SkillDeskDb"].ConnectionString);

            }
        }
        
    }
}