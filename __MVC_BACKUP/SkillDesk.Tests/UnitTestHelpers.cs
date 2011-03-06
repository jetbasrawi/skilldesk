using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Tests
{
    public static class UnitTestHelpers
    {
        public static T WithIncomingValues<T>(this T controller, FormCollection values) where T : Controller {
            controller.ControllerContext = new ControllerContext();
            controller.ValueProvider = new NameValueCollectionValueProvider(values, CultureInfo.CurrentCulture);
            return controller;
        }

        public static void ShouldEqual<T>(this T actualValue, T expectedValue) {
            Assert.AreEqual(expectedValue, actualValue);
        }

        public static ISkillsRepository MockSkillsRepository(params Skill[] skills) {
            var mockSkillsRepo = new Mock<ISkillsRepository>();
            mockSkillsRepo.Setup(x => x.Skills).Returns(skills.AsQueryable());
            return mockSkillsRepo.Object;
        }

        public static ISkillsRepository DefaultMockSkillsRepository() {
            return MockSkillsRepository(new Skill {Name = "Skill 1"},
                                        new Skill {Name = "Skill 2"},
                                        new Skill {Name = "Skill 3"},
                                        new Skill {Name = "Skill 4"},
                                        new Skill {Name = "Skill 5"},
                                        new Skill {Name = "Skill 6"});
        }

        public static void ShouldBeRedirectionTo(this ActionResult actionResult, object expectedRouteValues) {
            RouteValueDictionary actualValues = ((RedirectToRouteResult) actionResult).RouteValues;
            var expectedValues = new RouteValueDictionary(expectedRouteValues);
            foreach (string key in expectedValues.Keys)
                actualValues[key].ShouldEqual(expectedValues[key]);
        }

        public static void ShouldBeDefaultView(this ActionResult actionResult) {
            actionResult.ShouldBeView(string.Empty);
        }

        public static void ShouldBeView(this ActionResult actionResult, string viewName) {
            Assert.IsInstanceOf<ViewResult>(actionResult);
            ((ViewResult) actionResult).ViewName.ShouldEqual(viewName);
        }
    }
}
