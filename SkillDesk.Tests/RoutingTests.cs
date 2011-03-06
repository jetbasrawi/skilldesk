using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using SkillDesk.Web;

namespace SkillDesk.Tests
{
    [TestFixture]
    public class RoutingTests
    {
        [Test]
        public void RootUrl_Goes_To_Home_Index() {
            TestRoute("~/", "Home", "Index");
        }

        [Test]
        public void SkillProfile_Copy_Goes_To_EditProfile_Copy(){
            TestRoute("~/SkillProfile/Copy/Some Skill Path", "EditProfile", "Copy");
        }

        [Test]
        public void SkillProfile_Delete_Goes_To_EditProfile_Delete(){
            TestRoute("~/SkillProfile/Delete/Some Skill Path", "EditProfile", "Delete");
        }

        [Test]
        public void SkillProfile_Move_Goes_To_EditProfile_Move() {
            TestRoute("~/SkillProfile/Move/Some Skill Path", "EditProfile", "Move");
        }

        [Test]
        public void SkillProfile_AddUserSkill_Goes_To_SkillProfile_AddUserSkill() {
            TestRoute("~/SkillProfile/AddSkill", "SkillProfile", "AddSkill");
        }

        [Test]
        public void Controller_Action_Goes_To_Controller_Action() {
            TestRoute("~/MyController/MyAction", "MyController", "MyAction");
        }

        [Test]
        public void Root_Is_At_SkillProfile_Index() { 
            string result = GenerateUrlViaMocks( new { controller = "Home", action = "Index" });
            Assert.AreEqual("/", result);
        }

        [Test]
        public void ControllerAction_Is_At_Controller_Action()
        {
            string result = GenerateUrlViaMocks(new { controller = "SomeController", action = "SomeAction" });
            Assert.AreEqual("/SomeController/SomeAction", result);
        }

        [Test]
        public void SkillProfile_Slash_Some_SkillPath_Goes_To_SkillProfile_Index() {
            TestRoute("~/SkillProfile/SomeSkillName/SomeOtherSkillName", "SkillProfile", "Index");
        }

        [Test]
        [Ignore("This is known")]
        public void Skill_Profile_IsAt_SkillProfile_Index() {
            string result = GenerateUrlViaMocks(new { controller = "SkillProfile", action = "Index", skillPath = "SomeSkill/Some Other Skill" });
            Assert.AreEqual("/SkillProfile/SomeSkill/Some Other Skill", HttpUtility.HtmlDecode(result));
        }

        private string GenerateUrlViaMocks(object values) {

            // get the routing config and test context
            RouteCollection routeConfig = new RouteCollection();
            RegisterAllAreas(routeConfig);
            MvcApplication.RegisterRoutes(routeConfig);
            var mockContext = MakeMockHttpContextBase(null);
            RequestContext context = new RequestContext(mockContext.Object, new RouteData());
            
            // generate a URL
            return UrlHelper.GenerateUrl(null, null, null, new RouteValueDictionary(values), routeConfig, context, true);
        }

        private static void RegisterAllAreas(RouteCollection routes)
        {
            //var allAreas = new AreaRegistration[] { new SkillProfileAreaRegistration() };
            //foreach (AreaRegistration area in allAreas)
            //{
            //    var context = new AreaRegistrationContext(area.AreaName, routes);
            //    context.Namespaces.Add(area.GetType().Namespace);
            //    area.RegisterArea(context);
            //}
        }

        private static string GenerateUrlViaTestDouble(object defaults) {
            
            RouteCollection routeConfig = new RouteCollection();
            MvcApplication.RegisterRoutes(routeConfig);
            var testContext = new TestHttpContext(null);
            RequestContext context = new RequestContext(testContext, new RouteData());
            return UrlHelper.GenerateUrl(null, null, null, new RouteValueDictionary(defaults), routeConfig, context, true);
        }

        private static void TestRoute(string url, string controller, string action) {
            
            //Arrange
            RouteCollection routeConfig = new RouteCollection();
            MvcApplication.RegisterRoutes(routeConfig);
            var mockContext = MakeMockHttpContextBase(url);
            
            // Act (run the routing engine against this HttpContextBase)
            RouteData routeData = routeConfig.GetRouteData(mockContext.Object);
            
            // Assert
            Assert.IsNotNull(routeData, "NULL RouteData was returned");
            Assert.IsNotNull(routeData.Route, "No route was matched");
            Assert.AreEqual(controller, routeData.Values["controller"], "Wrong controller");
            Assert.AreEqual(action, routeData.Values["action"], "Wrong action");
        }

        private static Mock<HttpContextBase> MakeMockHttpContextBase(string url) {
            
            //Mock the request
            Mock<HttpRequestBase> mockHttpRequest = new Mock<HttpRequestBase>();
            mockHttpRequest.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(url);
            
            //Mock the response
            Mock<HttpResponseBase> mockHttpResponse = new Mock<HttpResponseBase>();
            mockHttpResponse.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(x => x);
            
            //Mock the HttpContext
            Mock<HttpContextBase> mockHttpContextBase = new Mock<HttpContextBase>();
            mockHttpContextBase.Setup(x => x.Request).Returns(mockHttpRequest.Object);
            mockHttpContextBase.Setup(x => x.Response).Returns(mockHttpResponse.Object);
            return mockHttpContextBase;
        }
    }
}