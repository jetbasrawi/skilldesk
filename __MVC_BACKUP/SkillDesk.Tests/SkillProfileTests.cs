using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using SkillDesk.Domain.Entities;
using SkillDesk.Domain.Services;
using SkillDesk.Web.Controllers;

namespace SkillDesk.Tests
{
    [TestFixture]
    public class SkillProfileTests
    {
        [Test]
        public void Adding_A_Skill_Creates_And_Adds_A_Skill_Profile_Item()
        {
            SkillProfile skillProfile = new SkillProfile();
            Skill skill1 = new Skill() {Id = 1, Name = "Skill1", Category = "Cat1" };
            
            //Act
            skillProfile.Add(skill1);

            //Assert:
            SkillProfileItem skillProfileItem = skillProfile.Skills.Single();
            Assert.AreEqual(skill1, skillProfileItem.Skill);
        }

        [Test]
        public void Can_Add_Skill_To_Profile()
        {
            //Arrange
            var mockRepo = UnitTestHelpers.MockSkillsRepository(
                new Skill() { Id = 1, Name = "skill1" },
                new Skill() { Id = 2, Name = "skill2" }
            );

            var skillsController = new SkillProfileController(mockRepo, null);
            var skillProfile = new SkillProfile();

            //Act
            skillsController.AddSkill(skillProfile, 2,null, null);
            
            //Assert
            skillProfile.Skills.Count.ShouldEqual(1);
            skillProfile.Skills[0].Skill.Id.ShouldEqual(2);

        }

        [Test]
        public void After_Adding_Skill_To_Profile_User_Goes_To_Profile_Screen()
        {
        // Arrange: Given a repository with some products...
            var mockProductsRepository = UnitTestHelpers.MockSkillsRepository(new Skill { Id = 1 });
            var profileController = new SkillProfileController(mockProductsRepository, null);
            // Act: When a user adds a skill to their profile...
            var result = profileController.AddSkill(new SkillProfile(), 1, "name", "someReturnUrl");
            // Assert: Then the user is redirected to the Profile ViewProfile screen
            result.ShouldBeRedirectionTo(new
            {
                action = "ViewProfile",
                returnUrl = "someReturnUrl"
            });
        }

        [Test]
        public void Can_View_Profile_Contents()
        {
            //Arrange
            ViewResult viewProfile = new SkillProfileController(null, null).ViewProfile(new SkillProfile(), "someUrl");

            //Act


            //Assert
        }

        public void Saving_SkillProfile_For_Anonymous_User_Will_Create_New_UserProfile()
        {

        }

        [Test]
        public void Can_Not_Save_If_User_Profile_Is_Invalid()
        {
            var skillProfileController = new SkillProfileController(null, null);
            skillProfileController.ModelState.AddModelError("Any", "Any");

            var userPofile = new UserProfile();
            userPofile.Name = "Name";
            userPofile.Country = "Country";
            userPofile.City = "City";

            ActionResult result = skillProfileController.SaveProfile(new SkillProfile(), userPofile);

            result.ShouldBeDefaultView();
        }
    }
}