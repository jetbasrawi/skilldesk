using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Entities;
using SkillDesk.Web.Models;

namespace SkillDesk.Web.Controllers
{
    public class MoveSkillsController : Controller
    {
        private MoveSkillViewModel _moveSkillViewModel;

        private readonly ISkillProfileRepository _skillProfileRepository;

        public MoveSkillsController(ISkillProfileRepository skillProfileRepository)
        {
            _skillProfileRepository = skillProfileRepository;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var moveSkillViewModel = Request.Form["moveSkillViewModel"];
            if (moveSkillViewModel != null)
            { // Form was posted containing serialized data
                _moveSkillViewModel = (MoveSkillViewModel)new MvcSerializer().Deserialize(moveSkillViewModel, SerializationMode.Signed);
                TryUpdateModel(_moveSkillViewModel);
            }
            else {
                _moveSkillViewModel = (MoveSkillViewModel) TempData["moveSkillViewModel"] ?? new MoveSkillViewModel();
            }
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Result is RedirectToRouteResult)
                TempData["moveSkillViewModel"] = _moveSkillViewModel;
        }

        [HttpPost]
        public ActionResult DoAction(SkillProfile skillProfile, string selectedAction, Guid[] selectedItems, string rootId) {
            
            switch (selectedAction.ToUpper()) {
                
                case "MOVE":
                    #region Get List of skills to display from the root

                    UserSkill rootSkillToDisplay = null;

                    Guid displayRootId;
                    if (!string.IsNullOrEmpty(rootId) && Guid.TryParse(rootId, out displayRootId)) {
                        rootSkillToDisplay = skillProfile.FirstOrDefault(s => s.Id == displayRootId);
                        rootSkillToDisplay.Children.AddRange(skillProfile.FindAll(s => s.ParentId == displayRootId));
                    }
                    else {
                        rootSkillToDisplay = new UserSkill();
                        var childSkills = skillProfile.FindAll(s => s.ParentId == null || s.ParentId == Guid.Empty);
                        rootSkillToDisplay.Children.AddRange(childSkills);
                    }

                    #endregion
                    #region Construct ViewModel with list of skills to display and selected skills to move

                    _moveSkillViewModel = new MoveSkillViewModel {Root = rootSkillToDisplay, SelectedItems = selectedItems};

                    #endregion
                    return RedirectToAction("Move");
                case "DELETE":

                    // return delete view to confirm deletion

                    //foreach (Guid guid in selectedItems)
                    //{
                    //    skillProfile.RemoveAll(s => s.Id.Equals(guid));
                    //}

                    break;
            }
            
            return View("YourProfile", new SkillProfileViewModelBase { SkillProfile = skillProfile });
        }

        [HttpPost]
        public RedirectToRouteResult Move(SkillProfile skillProfile)
        {
            foreach (var childGuid in _moveSkillViewModel.SelectedItems) {
                foreach (var parentGuid in _moveSkillViewModel.TargetItems) {
                    skillProfile.FirstOrDefault(s => s.Id.Equals(childGuid)).ParentId = parentGuid;
                }
            }

            _skillProfileRepository.Save(skillProfile);
            return RedirectToAction("Index", "SkillProfile");
        }

        public ViewResult Move() {
            return View(_moveSkillViewModel);
        }

        public ViewResult Detail(SkillProfile skillProfile, Guid userSkillId) {
            var userSkill = skillProfile.GetChildren(skillProfile.FirstOrDefault(s=>s.Id.Equals(userSkillId)));
            return View(new UserSkillDetailViewModel { UserSkill = userSkill });
        }
    }
}
