using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Entities;
using SkillDesk.Domain.Services;
using SkillDesk.Web.Models;

namespace SkillDesk.Web.Controllers
{
    public class SkillProfileController : Controller
    {
        private readonly ISkillsRepository _skillsRepository;
        private readonly IProfileSaver _profileSaver;

        public SkillProfileController(ISkillsRepository skillsRepository, IProfileSaver profileSaver) {
            _skillsRepository = skillsRepository;
            _profileSaver = profileSaver;
        }

        [HttpPost]
        public ActionResult AddSkill(SkillProfile skillProfile, [DefaultValue(0)]int id, string name, string returnUrl) 
        {
            Skill skill = (id == 0 || string.IsNullOrEmpty(name)) ? new Skill() : _skillsRepository.Skills.First(x => x.Id == id || x.Name == name);
            TryUpdateModel(skill);
            
            if(ModelState.IsValid) {
                _skillsRepository.Save(skill);
                skillProfile.Add(skill);
            }
            else {
                return AjaxView(skill);
            }

            return RedirectToAction("ViewProfile", new {returnUrl});
        }

        public ActionResult AddSkill() {
            return AjaxView(new Skill());
        }

        protected ActionResult AjaxView(string viewName = null, object model = null) {
            if (RouteData != null && string.IsNullOrEmpty(viewName))
                viewName = RouteData.Values["action"].ToString();
            
            if (Request != null && Request.IsAjaxRequest())
                return PartialView(string.Format("_{0}", viewName), model);

            return View(viewName, null, model);
        }

        protected ActionResult AjaxView(object model)
        {
            return AjaxView(null, model);
        }


        [HttpPost]
        public RedirectToRouteResult RemoveSkill(SkillProfile skillProfile, int skillId, string returnUrl) {
            skillProfile.RemoveSkill(skillId);

            return RedirectToAction("ViewProfile", new {returnUrl});
        }

        public ViewResult ViewProfile(SkillProfile skillProfile, string returnUrl) {
            return View(new SkillProfileViewModel {Profile = skillProfile, ReturnUrl = returnUrl});
        }


        public ViewResult Summary(SkillProfile skillProfile) {
            return View(skillProfile);
        }

        [HttpPost]
        public ActionResult SaveProfile(SkillProfile skillProfile, UserProfile userProfile) {
            if (!ModelState.IsValid)
                return View(userProfile);

            _profileSaver.SaveProfile(userProfile);
            return View("Completed");
        }

        public ViewResult SaveProfile(UserProfile userProfile) {
            return View(new UserProfile());
        }
    }
}