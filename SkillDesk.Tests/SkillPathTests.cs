using NUnit.Framework;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Tests
{
    [TestFixture]
    public class SkillPathTests
    {
        const string TestPath1 = "/Skill 1/Skill 1a/Skill 1aa";

        [Test]
        public void Should_Provide_A_List_Of_All_Items_In_Path_In_A_Child_Collection_Of_SkillPath_Objects() {
            //Arrange
            SkillPath skillPath = new SkillPath(TestPath1);

            //Assert
            Assert.AreEqual(3, skillPath.PathItems.Count);
            Assert.AreEqual("Skill 1", skillPath.PathItems[0].Name);
            Assert.AreEqual("Skill 1a", skillPath.PathItems[1].Name);
            Assert.AreEqual("Skill 1aa", skillPath.PathItems[2].Name);

            Assert.AreEqual("/Skill 1", skillPath.PathItems[0].FullPath);
            Assert.AreEqual("/Skill 1/Skill 1a", skillPath.PathItems[1].FullPath);
            Assert.AreEqual("/Skill 1/Skill 1a/Skill 1aa", skillPath.PathItems[2].FullPath);
        }

        [Test]
        public void Should_Return_Empty_For_Parent_Path_If_It_Is_The_Root() {
            
            SkillPath s = new SkillPath(TestHelpers.GetRandomPath(1));
            Assert.AreEqual(string.Empty, s.ParentPath);
        }
        
        [Test]
        public void Should_Return_The_Path_To_Its_Parent_For_Any_Url()
        {
            string randomPath = TestHelpers.GetRandomPath(3);

            string randomString = TestHelpers.RandomString();

            SkillPath s = new SkillPath(randomPath + "/" + randomString);

            Assert.AreEqual(randomPath, s.ParentPath);
        }


        [Test]
        public void Should_Return_The_Path_To_Its_Parent() {
            //Arrange
            SkillPath skillPath = new SkillPath(TestPath1);

            //Act

            //Assert
            Assert.AreEqual("/Skill 1/Skill 1a", skillPath.ParentPath);
        }

        [Test]
        public void Should_Return_Slash_Only_If_Path_Is_Empty() {
            //Arrange
            SkillPath s1 = new SkillPath("");
            SkillPath s2 = new SkillPath(" ");
            SkillPath s3 = new SkillPath(null);

            //Assert
            Assert.AreEqual("/", s1.FullPath);
            Assert.AreEqual("/", s2.FullPath);
            Assert.AreEqual("/", s3.FullPath);
        }

        [Test]
        public void Should_Return_Last_Item_In_Path_As_Name_For_Any_Path() {
            
            //Arrange
            string item1 = TestHelpers.RandomString();
            string item2 = TestHelpers.RandomString();
            string item3 = TestHelpers.RandomString();
            
            //Act
            SkillPath s = new SkillPath("/" + item1 + "/" + item2 + "/" + item3 + "/");

            //Assert
            Assert.AreEqual(item3, s.Name);
        }

        [Test]
        public void Should_Return_Last_Item_In_The_Path_As_The_Name()
        {
            //Arrange
            SkillPath s = new SkillPath(TestPath1);
            
            //Act
            string name = s.Name;

            //Assert
            Assert.AreEqual("Skill 1aa", s.Name);
        }

        [Test]
        public void Should_Encapsulate_The_Path_Passed_In() {
            
            //Arrange
            SkillPath skillPath = new SkillPath(TestPath1);

            //Assert
            Assert.AreEqual(TestPath1, skillPath.FullPath);
        }
    }
}