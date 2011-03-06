using SkillDesk.Domain.Entities;

namespace SkillDesk.Web.Models
{
    public class UserSkillViewModelItem
    {
        public UserSkillViewModelItem(UserSkill userSkill, SkillProfile skillProfile) {
            UserSkill = userSkill;
            SkillProfile = skillProfile;
        }

        public UserSkill UserSkill;
        public SkillProfile SkillProfile;
    }
}