using System;
using System.Web.Mvc;
using NUnit.Framework;
using SkillDesk.Domain.Entities;
using SkillDesk.Web.Controllers;
using SkillDesk.Web.HtmlHelpers;
using SkillDesk.Web.Models;

namespace SkillDesk.Tests
{
    [TestFixture]
    public class DisplayingPageLinks
    {
        [Test]
        public void Can_Generate_Links_To_Other_Pages()
        {
            //Arrange
            HtmlHelper html = null;
            PagingInfo info = new PagingInfo { CurrentPage = 2, TotalItems = 28, ItemsPerPage = 10 };
            Func<int, string> pageUrl = i => "Page" + i;

            //Act
            MvcHtmlString result = html.PageLinks(info, pageUrl);
            
            //Assert
            result.ToString().ShouldEqual("<a href=\"Page1\">1</a>\r\n<a class=\"selected\" href=\"Page2\">2</a>\r\n<a href=\"Page3\">3</a>\r\n");
        }

        [Test]
        public void Product_Lists_Include_Correct_Page_Numbers()
        {
            // Arrange: If there are five products in the repository...
            var mockRepository = TestHelpers.DefaultMockSkillsRepository();
            var controller = new SkillsController(mockRepository) { PageSize = 3 };
            
            // Act: ... then when the user asks for the second page (PageSize=3)...
            var result = controller.List(null, 2);
            
            // Assert: ... they'll see page links matching the following
            var viewModel = (SkillsListViewModel)result.ViewData.Model;
            PagingInfo pagingInfo = viewModel.PagingInfo;
            pagingInfo.CurrentPage.ShouldEqual(2);
            pagingInfo.ItemsPerPage.ShouldEqual(3);
            pagingInfo.TotalItems.ShouldEqual(6);
            pagingInfo.TotalPages.ShouldEqual(2);
        }
    }
}