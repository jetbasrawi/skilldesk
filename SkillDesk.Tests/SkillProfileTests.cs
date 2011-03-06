using System;
using System.Linq;
using NUnit.Framework;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Tests
{
    [TestFixture]
    public class SkillProfileTests
    {
        [Test]
        public void Should_Return_Skill_For_Path() {
            
            //Arrange
            Guid g1 = Guid.NewGuid();
            Guid g2 = Guid.NewGuid();
            Guid g3 = Guid.NewGuid();
            Guid g4 = Guid.NewGuid();
            SkillProfile skillProfile = new SkillProfile();
            skillProfile.Add(new UserSkill { Id = g1, SkillName = g1.ToString()} );
            skillProfile.Add(new UserSkill { Id = g2, ParentId  = g1, SkillName = g2.ToString()} );
            skillProfile.Add(new UserSkill { Id = g3, ParentId  = g2, SkillName = g3.ToString()} );
            skillProfile.Add(new UserSkill { Id = g4, ParentId  = g3, SkillName = g4.ToString()} );

            UserSkill s4 = skillProfile.FirstOrDefault(s => s.SkillName.Equals(g4.ToString()));
            string s4Path = s4.GetSkillPath(skillProfile);

            //Act
            UserSkill userSkillFound = skillProfile.GetUserSkill(s4Path);
            
            //Assert
            Assert.AreSame(s4, userSkillFound);
        }
    }
}