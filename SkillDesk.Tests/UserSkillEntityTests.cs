using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Tests
{
    [TestFixture]
    public class UserSkillEntityTests
    {
        [Test]
        public void UserSkill_Has_A_Parent_Id() {
         
            //Arrange
            UserSkill userSkill = new UserSkill();

            //Act
            Guid parentId = Guid.NewGuid();
            userSkill.ParentId = parentId;

            //Assert
            Assert.AreEqual(parentId, userSkill.ParentId);
        }

        [Test]
        public void UserSkill_ParentId_May_Be_Null()
        {
            //Arrange
            UserSkill userSkill = new UserSkill();

            //Act
            userSkill.ParentId = null;

            //Assert
            Assert.IsNull(userSkill.ParentId);
        }

        [Test]
        public void UserSkill_ParentId_Is_Null_By_Default()
        {
            //Arrange
            UserSkill userSkill = new UserSkill();

            //Assert
            Assert.IsTrue(userSkill.ParentId == null);
        }

        [Test]
        public void UserSkill_ParentId_May_Be_Changed()
        {
            UserSkill userSkill = new UserSkill();

            Guid parentId = Guid.NewGuid();
            userSkill.ParentId = parentId;
            Assert.AreEqual(parentId, userSkill.ParentId);

            Guid parentId2 = Guid.NewGuid();
            userSkill.ParentId = parentId2;
            Assert.AreEqual(parentId2, userSkill.ParentId);
        }

        [Test]
        public void New_User_Skill_Gets_Auto_Generated_Id()
        {
            UserSkill userSkill = new UserSkill();
            Assert.AreNotEqual(Guid.Empty, userSkill.Id);
        }

        [Test]
        [Ignore("Private setter stops serialization working. Find a solution.")]
        public void User_SkillId_May_Not_Be_Changed()
        {
            UserSkill userSkill = new UserSkill();
            Type t = userSkill.GetType();
            PropertyInfo pi = t.GetProperty("Id");
            Assert.IsNull(pi.GetSetMethod());
        }

        [Test]
        public void UserSkill_Has_A_List_Of_Child_User_Skills() {
            
            //Arrange
            UserSkill userSkill = new UserSkill();
            userSkill.Children.Add(new UserSkill() { SkillId = 5});
            userSkill.Children.Add(new UserSkill() { SkillId = 6 });
            userSkill.Children.Add(new UserSkill() { SkillId = 7 });
            userSkill.Children.Add(new UserSkill() { SkillId = 8 });
            userSkill.Children.Add(new UserSkill() { SkillId = 9 });

            //Act

            //Assert
            for (int i = 5; i < 10; i++) {
                Assert.That(userSkill.Children.Any(x => x.SkillId == i));
            }
        }

        

        [Test]
        [Ignore]
        public void User_Skill_Cannot_Be_A_Child_Of_Itself() {
            //Arrange
            UserSkill userSkill = new UserSkill();

            //Act
            

            //Assert
            Assert.Throws<InvalidOperationException>(() => userSkill.ParentId = userSkill.Id);

        }
    }
}