using System;
using System.Web.Mvc;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Entities;
using SkillDesk.Web.Models;

namespace SkillDesk.Web.Controllers
{
    public class EditProfileController : Controller
    {
        private readonly ISkillsRepository _skillsRepository;
        private readonly ISkillProfileRepository _skillProfileRepository;
        private readonly IExperienceTypeRepository _experienceTypeRepository;

        public EditProfileController(ISkillsRepository skillsRepository, ISkillProfileRepository skillProfileRepository, IExperienceTypeRepository experienceTypeRepository)
        {
            _skillsRepository = skillsRepository;
            _experienceTypeRepository = experienceTypeRepository;
            _skillProfileRepository = skillProfileRepository;
        }

        [HttpPost]
        [ActionName("Move")]
        public RedirectToRouteResult MoveWhenVerbEqualsPost(SkillProfile skillProfile, MoveUserSkillViewModel moveSkillViewModel){
            UserSkill skillToMove = skillProfile.GetUserSkill(moveSkillViewModel.FromPath);
            UserSkill parentSkill = skillProfile.GetUserSkill(moveSkillViewModel.ToPath);
            skillToMove.ParentId = parentSkill.Id;
            _skillProfileRepository.Save(skillProfile);

            return RedirectToAction("Index", "SkillProfile", new { skillPath = moveSkillViewModel.ToPath });
        }

        public ViewResult Move(SkillProfile skillProfile, string skillPath) {
            return View("SelectUserSkill", new MoveUserSkillViewModel { SkillProfile = skillProfile, SkillPath = skillPath});
        }

        [HttpPost]
        public RedirectToRouteResult Copy(SkillProfile skillProfile, MoveUserSkillViewModel moveSkillViewModel) {
            UserSkill skillToCopy = skillProfile.GetUserSkill(moveSkillViewModel.FromPath);
            UserSkill parentSkill = skillProfile.GetUserSkill(moveSkillViewModel.ToPath);
            UserSkill copy = skillToCopy.Clone();
            copy.ParentId = parentSkill.Id;
            copy.Children.Clear();
            skillProfile.Add(copy);
            _skillProfileRepository.Save(skillProfile);

            return RedirectToAction("Index", "SkillProfile", new { skillPath = moveSkillViewModel.ToPath });
        }

        public ViewResult Copy(SkillProfile skillProfile, string skillPath){
            return View("SelectUserSkill", new CopyUserSkillViewModel { SkillProfile = skillProfile, SkillPath = skillPath });
        }

        [HttpPost]
        public RedirectToRouteResult Delete(SkillProfile skillProfile, string skillPath){
            UserSkill skillToDelete = skillProfile.Find(s => s.GetSkillPath(skillProfile).Equals(skillPath));
            UserSkill parentSkill = skillProfile.Find(s => s.Id.Equals(skillToDelete.ParentId));
            skillProfile.Remove(skillToDelete);
            return RedirectToAction("Index", "SkillProfile", new { skillPath = parentSkill.GetSkillPath(skillProfile)});
        }

        public ViewResult Delete(string skillPath){
            return View(new DeleteSkillViewModel(skillPath));
        }
    }
}
