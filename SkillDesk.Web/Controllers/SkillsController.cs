using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Entities;
using SkillDesk.Web.Models;

namespace SkillDesk.Web.Controllers
{
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsRepository _skillsRepository;
        public int PageSize = 12;

        public SkillsController(ISkillsRepository skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }

        public ViewResult List(string category, int page)
        {
            List<Skill> skillsToDisplay = category == null ? _skillsRepository.Skills.ToList() : _skillsRepository.Skills.Where(s => s.Category == category).ToList();
            var pagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = skillsToDisplay.Count() };
            var viewModel = new SkillsListViewModel {
                                                        PagingInfo = pagingInfo,
                                                        SkillsToDisplay = skillsToDisplay.Skip((page - 1)*PageSize).Take(PageSize).Select(x => new SkillUiItem {Skill = x, Routes = new RouteValueDictionary(new {controller = "Skills", action = "Detail", category = !string.IsNullOrEmpty(category) ? category : null, skillName = x.Name})}).ToList(),
                                                        CurrentCategory = !string.IsNullOrEmpty(category) ? category : null
            };
            
            return View(viewModel);
        }

        public ViewResult Detail(string skillName) {

            var model = new SkillDetailViewModel();
            model.Skill = _skillsRepository.Skills.FirstOrDefault(x => x.Name == skillName);
            return View(model);
        }
    }
}
