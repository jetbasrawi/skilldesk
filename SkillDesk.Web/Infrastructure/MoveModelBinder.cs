using System;
using System.Web.Mvc;
using SkillDesk.Web.Controllers;
using SkillDesk.Web.Models;

namespace SkillDesk.Web.Infrastructure
{
    public class MoveModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            MoveUserSkillViewModel moveSkillViewModel = new MoveUserSkillViewModel();
            moveSkillViewModel.SkillPath = controllerContext.RequestContext.HttpContext.Request["skillPath"];
            return moveSkillViewModel;
        }
    }
}