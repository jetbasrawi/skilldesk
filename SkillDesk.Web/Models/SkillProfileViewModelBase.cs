using SkillDesk.Domain.Entities;

namespace SkillDesk.Web.Models
{
    public class SkillProfileViewModelBase
    {
        public UserSkill CurrentSkill { get { return SkillProfile.GetUserSkill(SkillPath); } }
        public SkillProfile SkillProfile { get; set; }
        public string ReturnUrl { get; set; }
        public string SkillPath { get; set; }
    }
}