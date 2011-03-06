using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ISkillsRepository _skillsRepository;

        public AdminController(ISkillsRepository skillsRepository) {
            this._skillsRepository = skillsRepository;
        }

        public ViewResult Index() {
            return View(_skillsRepository.Skills.ToList());
        }

        public ViewResult Edit(int id) {
            return View(_skillsRepository.Skills.First(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult Edit(int skillId, HttpPostedFileBase image) {

            Skill skill = skillId == 0 ? new Skill() : _skillsRepository.Skills.First(x => x.Id == skillId);
            TryUpdateModel(skill);
            
            if (ModelState.IsValid) {
                if (image != null) {
                    skill.ImageMimeType = image.ContentType;
                    skill.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(skill.ImageData, 0, image.ContentLength);
                }

                _skillsRepository.Save(skill);
                TempData["message"] = skill.Name + " has been saved.";
                return RedirectToAction("Index");
            }
            else // Validation error, so redisplay same view
                return View(skill);
        }

        public ViewResult Create() {
            return View("Edit", new Skill());
        }

        public RedirectToRouteResult Delete(int id) {
            Skill skill = _skillsRepository.Skills.First(x => x.Id == id);
            _skillsRepository.DeleteSkill(skill);
            TempData["message"] = skill.Name + " was deleted";
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int id)
        {
            var skill = _skillsRepository.Skills.First(x => x.Id == id);
            return File(skill.ImageData, skill.ImageMimeType);
        }
    }
}