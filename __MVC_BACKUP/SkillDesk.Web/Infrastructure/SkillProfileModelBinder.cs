using System;
using System.Web.Mvc;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Web.Infrastructure
{
    public class SkillProfileModelBinder : IModelBinder
    {
        private const string skillProfileSessionKey = "_skillProfile";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Some modelbinders can update properties on existing model instances. This
            // one doesn't need to - it's only used to supply action method parameters.
            if (bindingContext.Model != null)
                throw new InvalidOperationException("Cannot update instances");

            // Return the skillProfile from Session[] (creating it first if necessary)
            SkillProfile skillProfile = (SkillProfile)controllerContext.HttpContext.Session[skillProfileSessionKey];
            if (skillProfile == null)
            {
                skillProfile = new SkillProfile();
                controllerContext.HttpContext.Session[skillProfileSessionKey] = skillProfile;
            }
            return skillProfile;
        }
    }
}