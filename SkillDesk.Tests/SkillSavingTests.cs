using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Entities;
using SkillDesk.Web.Controllers;

namespace SkillDesk.Tests
{
    [TestFixture]
    public class SkillSavingTests
    {
        [Test]
        public void Can_Save_Edited_Skill() {

            //Arrange
            var mockRepository = new Mock<ISkillsRepository>();
            var skill = new Skill {Id = 123};
            mockRepository.Setup(x => x.Skills).Returns(new[] {skill}.AsQueryable());

            //Act
            AdminController controller = new AdminController(mockRepository.Object)
                .WithIncomingValues(new FormCollection {
                                                           {"Name", "SomeName"},
                                                           {"Description", "Some description"},
                                                           {"Category", "Some category"}
                                                       });
            var result = controller.Edit(123, null);

            //Assert
            mockRepository.Verify(x => x.Save(skill));
            result.ShouldBeRedirectionTo(new { action = "Index" });
        }
    }
}