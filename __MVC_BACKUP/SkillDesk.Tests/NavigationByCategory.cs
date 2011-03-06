using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SkillDesk.Domain.Entities;
using SkillDesk.Web.Controllers;
using SkillDesk.Web.Models;

namespace SkillDesk.Tests
{
    [TestFixture]
    public class NavigationByCategory
    {
        [Test]
        [Ignore]
        public void NavMenu_Includes_Alphabetical_List_Of_Categories()
        {
            // Arrange: Given 4 Skills in 3 categories in nonalphabetized order
            var mockSkillsRepository = UnitTestHelpers.MockSkillsRepository(
                new Skill { Category = "Vegetable", Name = "SkillA"},
                new Skill { Category = "Animal", Name = "SkillB" },
                new Skill { Category = "Vegetable", Name = "SkillC" },
                new Skill { Category = "Mineral", Name = "SkillD" }
                );
            
            // Act: ... when we render the navigation menu
            var result = new NavController(mockSkillsRepository).Menu(null);
            
            // Assert: ... then the links to categories ...
            var categoryLinks = ((IEnumerable<NavLink>) result.ViewData.Model);
            
            // ... are distinct categories in alphabetical order
            
            //CollectionAssert.AreEqual(new[] {"Animal", "Mineral", "Vegetable"}, categoryLinks.Select(x => x.RouteValues["category"]));
            
            // ... and contain enough information to link to that category
            foreach (var link in categoryLinks)
            {
                link.RouteValues["controller"].ShouldEqual("Skills");
                link.RouteValues["action"].ShouldEqual("List");
                link.RouteValues["page"].ShouldEqual(1);
                link.Text.ShouldEqual(link.RouteValues["category"]);
            }
        }

        [Test]
        public void NavMenu_Shows_Home_Link_At_Top()
        {
            // Arrange: Given any repository
            var mockSkillsRepository = UnitTestHelpers.MockSkillsRepository();
            
            // Act: ... when we render the navigation menu
            var result = new NavController(mockSkillsRepository).Menu(null);
            
            // Assert: ... then the top link is to Home
            var topLink = ((IEnumerable<NavLink>) result.ViewData.Model).First();
            topLink.RouteValues["controller"].ShouldEqual("Skills");
            topLink.RouteValues["action"].ShouldEqual("List");
            topLink.RouteValues["page"].ShouldEqual(1);
            topLink.RouteValues["category"].ShouldEqual(null);
            topLink.Text.ShouldEqual("Home");
        }

        [Test]
        public void NavMenu_Highlights_Current_Category()
        {
            // Arrange: Given two categories...
            var skillsRepository = UnitTestHelpers.MockSkillsRepository(
                new Skill { Category = null, Name = "CatA" },
                new Skill { Category = null, Name = "CatB" }
            );
            // Act: ... when we render the navigation menu
            var result = new NavController(skillsRepository).Menu("CatB");
            // Assert: ... then only the current category is highlighted
            var highlightedLinks = ((IEnumerable<NavLink>)result.ViewData.Model)
            .Where(x => x.IsSelected).ToList();

            highlightedLinks.Count.ShouldEqual(1);
            highlightedLinks[0].Text.ShouldEqual("CatB");
        }
    }
}
