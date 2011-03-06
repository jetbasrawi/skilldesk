using System.Collections.Generic;
using NUnit.Framework;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Entities;
using SkillDesk.Web.Controllers;
using SkillDesk.Web.Models;

namespace SkillDesk.Tests
{
    [TestFixture]
    public class CatalogBrowsingTests
    {
        [Test]
        public void Can_View_A_Paged_List_Of_Skills()
        {
            //Arrange
            ISkillsRepository skillsRepository = UnitTestHelpers.DefaultMockSkillsRepository();

            var controller = new SkillsController(skillsRepository);
            controller.PageSize = 3;

            //Act
            var result = controller.List(null, 2);

            //Assert
            var model = (SkillsListViewModel) result.ViewData.Model;
            model.SkillsToDisplay.Count.ShouldEqual(3);
            model.SkillsToDisplay[0].Skill.Name.ShouldEqual("Skill 4");
            model.SkillsToDisplay[1].Skill.Name.ShouldEqual("Skill 5");
            model.SkillsToDisplay[2].Skill.Name.ShouldEqual("Skill 6");
        }

        [Test]
        public void Can_View_Skills_From_All_Categories()
        {
            // Arrange: If two products are in two different categories...
            ISkillsRepository repository = UnitTestHelpers.MockSkillsRepository(
            new Skill { Name = "Artemis", Category = "Greek" },
            new Skill { Name = "Neptune", Category = "Roman" }
            );
            var controller = new SkillsController(repository);
            // Act: ... then when we ask for the "All Products" category
            var result = controller.List(null, 1);
            // Arrange: ... we get both products
            var viewModel = (SkillsListViewModel)result.ViewData.Model;
            viewModel.SkillsToDisplay.Count.ShouldEqual(2);
            viewModel.SkillsToDisplay[0].Skill.Name.ShouldEqual("Artemis");
            viewModel.SkillsToDisplay[1].Skill.Name.ShouldEqual("Neptune");
        }

        [Test]
        public void Can_View_Skills_From_A_Single_Category()
        {
            // Arrange: If two products are in two different categories...
            ISkillsRepository repository = UnitTestHelpers.MockSkillsRepository(
                new Skill { Name = "Artemis", Category = "Greek" },
                new Skill { Name = "Neptune", Category = "Roman" }
                );
            var controller = new SkillsController(repository);
            // Act: ... then when we ask for one specific category
            var result = controller.List("Roman", 1);
            // Arrange: ... we get only the product from that category
            var viewModel = (SkillsListViewModel)result.ViewData.Model;
            viewModel.SkillsToDisplay.Count.ShouldEqual(1);
            viewModel.SkillsToDisplay[0].Skill.Name.ShouldEqual("Neptune");
            viewModel.CurrentCategory.ShouldEqual("Roman");
        }
    }
}