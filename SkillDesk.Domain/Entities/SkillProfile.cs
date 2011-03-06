using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SkillDesk.Domain.Entities
{
    [Serializable]
    public class SkillProfile : List<UserSkill>
    {
        public UserSkill GetChildren(UserSkill userSkill) {
            if (userSkill == null)
                userSkill = new RootSkill();

            List<UserSkill> skill = FindAll(s => s.ParentId.Equals(userSkill.Id));
            userSkill.Children.Clear();
            userSkill.Children.AddRange(skill);
            return userSkill;
        }

        public UserSkill GetUserSkill(string skillPath)
        {
            if (!string.IsNullOrEmpty(skillPath)) {
                var match = Regex.Match(skillPath, "[a-zA-Z0-9-\\s\\.]*/?$");
                Capture capture = match.Captures[0];
                string skillName = capture.Value;
                return this.FirstOrDefault(s => s.UrlSafeName.Equals(skillName.TrimEnd("/".ToCharArray())));
            }
            else {
                return GetChildren(null);
            }
        }
    }
}