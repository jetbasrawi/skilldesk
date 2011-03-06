using System;
using System.Linq;
using System.Web.Mvc;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Entities;
using SkillDesk.Web.Models;

namespace SkillDesk.Web.Controllers
{
    public class SkillProfileController : ControllerBase
    {
        private readonly ISkillsRepository _skillsRepository;
        private readonly ISkillProfileRepository _skillProfileRepository;
        private readonly IExperienceTypeRepository _experienceTypeRepository;

        private AddSkillViewModel _addSkillViewModel;

        public SkillProfileController(ISkillsRepository skillsRepository, ISkillProfileRepository skillProfileRepository, IExperienceTypeRepository experienceTypeRepository) {
            _skillsRepository = skillsRepository;
            _experienceTypeRepository = experienceTypeRepository;
            _skillProfileRepository = skillProfileRepository;
        }

        #region Add Skill

        [HttpPost]
        public ActionResult AddSkill(SkillProfile skillProfile, string returnUrl, string skillPath, string skillName)
        {
            UserSkill userSkill = skillProfile.FirstOrDefault(x => x.SkillName == skillName);
            if (userSkill == null) {
                userSkill = new UserSkill() { SkillName = skillName };

                string experienceTypeId = Request.Form["ExperienceType"];
                ExperienceType experienceType = _experienceTypeRepository.ExperienceLevels.FirstOrDefault(e => e.Id.ToString() == experienceTypeId);
                if (experienceType != null)
                    userSkill.ExperienceType = experienceType;
            }
            else
            {
                ModelState.AddModelError("Skill.Name", string.Format("The skill {0} already exists in your profile.", userSkill.SkillName));
            }

            if (ModelState.IsValid)
            {
                UserSkill parentSkill = skillProfile.GetUserSkill(skillPath);
                userSkill.ParentId = parentSkill.Id;
                skillProfile.Add(userSkill);
                _skillProfileRepository.Save(skillProfile);
            }
            else
            {
                return AjaxView(userSkill);
            }

            return RedirectToAction("Index", new { skillPath });
        }

        public ActionResult AddSkill(SkillProfile skillProfile, string skillPath) {
            
            PutExperienceTypesInViewData();

            AddSkillViewModel v = new AddSkillViewModel();
            v.SkillProfile = skillProfile;
            v.SkillPath = skillPath;
            
            return View(_addSkillViewModel);
        }

        private void PutExperienceTypesInViewData() {
            
            var selectListItems = _experienceTypeRepository
                .ExperienceLevels
                .Select(l => new { l.Id, l.Name })
                .AsEnumerable();

            ViewData["ExperienceTypes"] = new SelectList(selectListItems, "Id", "Name");
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

        #endregion

        [HttpPost]
        public RedirectToRouteResult RemoveSkill(SkillProfile skillProfile, int skillId, string returnUrl) {
            skillProfile.RemoveAll(x => x.SkillId == skillId);

            return RedirectToAction("Index", new {returnUrl});
        }
        
        public ViewResult Index(SkillProfile skillProfile, string skillPath) {
            return View(new SkillProfileViewModel { SkillProfile = skillProfile, SkillPath = skillPath });
        }

        public ViewResult Summary(SkillProfile skillProfile)
        {
            return View(skillProfile);
        }

        [HttpPost]
        public ViewResult SaveProfile(SkillProfileViewModelBase skillProfileViewModel)
        {
            if (!ModelState.IsValid)
                return View(skillProfileViewModel);

            _skillProfileRepository.Save(skillProfileViewModel.SkillProfile);

            TempData["message"] = "Your profile has been saved.";

            return View(skillProfileViewModel);
        }

        

       

       
    }

    
}