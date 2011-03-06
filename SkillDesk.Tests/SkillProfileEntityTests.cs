using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Tests
{
    [TestFixture]
    public class SkillProfileEntityTests
    {
        [Test]
        public void Can_Add_A_UserSkill_To_Profile()
        {

            ////Arrange
            //SkillProfile skillProfile = new SkillProfile();
            //UserSkill userSkill = new UserSkill()  SkillName = TestHelpers.RandomString() };

            ////Act
            //skillProfile.Add(userSkill);

            ////Assert
            //Assert.IsTrue(skillProfile.Contains(userSkill));
        }


        [Test]
        public void Should_Be_Able_To_Serialise_And_Deserialise_Skill_Profile()
        {

            ////Arrange
            //SkillProfile skillProfile = new SkillProfile();
            //UserSkill userSkill = new UserSkill(5, null, TestHelpers.RandomString());
            //skillProfile.Add(userSkill);

            ////Act
            //SkillProfile dsSkillProfile;
            //using (Stream stream = new MemoryStream())
            //{
            //    XmlSerializer xs = new XmlSerializer(typeof(SkillProfile));
            //    xs.Serialize(XmlWriter.Create(stream), skillProfile);
            //    stream.Flush();
            //    stream.Seek(0, SeekOrigin.Begin);
            //    dsSkillProfile = (SkillProfile)xs.Deserialize(XmlReader.Create(stream));
            //}
            ////Assert
            //Assert.IsTrue(skillProfile.Contains(userSkill));
        }
    }
}