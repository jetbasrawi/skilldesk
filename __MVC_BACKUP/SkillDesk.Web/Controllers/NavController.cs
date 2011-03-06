using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Concrete;
using SkillDesk.Web.Models;

namespace SkillDesk.Web.Controllers
{
    public class NavController : Controller
    {
        private ISkillsRepository _skillsRepository;

        public NavController(ISkillsRepository skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }

        public ViewResult Menu(string category)
        {
            // Just so we don't have to write this code twice
            Func<string, NavLink> makeLink = categoryName =>
                new NavLink
                {
                    Text = categoryName ?? "Home",
                    RouteValues = new RouteValueDictionary( new { controller = "Skills", action = "List", category = categoryName, page = 1 } ),
                    IsSelected = (categoryName == category)
                };
            
            // Put a Home link at the top
            List<NavLink> navLinks = new List<NavLink>();
            navLinks.Add(makeLink(null));
            
            // Add a link for each distinct category
           var categories = _skillsRepository.Skills.Where(x => x.Category == null).Select(x => x.Name);
            
            foreach (string categoryName in categories.Distinct().OrderBy(x => x))
                navLinks.Add(makeLink(categoryName));
            

            return View(navLinks);
        }
    }
}
