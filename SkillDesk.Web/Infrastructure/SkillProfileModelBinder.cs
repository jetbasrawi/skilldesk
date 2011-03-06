using System;
using System.Web.Mvc;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Concrete;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Web.Infrastructure
{
    public class SkillProfileModelBinder : IModelBinder
    {
        private const string SkillProfileSessionKey = "_skillProfile";
        
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            return BindModel(bindingContext, controllerContext, new XmlFileSkillProfileRepository());
        }

        public object BindModel(ModelBindingContext bindingContext, ControllerContext controllerContext, ISkillProfileRepository skillProfileRepository) {

            // Some modelbinders can update properties on existing model instances. This
            // one doesn't need to - it's only used to supply action method parameters.
            if (bindingContext.Model != null)
                throw new InvalidOperationException("Cannot update instances");

            // Return the skillProfile from Session[] (creating it first if necessary)
            SkillProfile skillProfile = (SkillProfile)controllerContext.HttpContext.Session[SkillProfileSessionKey];
            if (skillProfile == null) {
                skillProfile = skillProfileRepository.Load();
                controllerContext.HttpContext.Session[SkillProfileSessionKey] = skillProfile;
            }
            return skillProfile;
        }
    }
}