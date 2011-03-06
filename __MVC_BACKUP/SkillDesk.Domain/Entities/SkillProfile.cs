using System;
using System.Collections.Generic;
using System.Linq;

namespace SkillDesk.Domain.Entities
{
    public class SkillProfile
    {
        private List<SkillProfileItem> _skills = new List<SkillProfileItem>();

        public List<SkillProfileItem> Skills
        {
            get { return _skills; }
        }

        public void Add(Skill skill)
        {
            _skills.Add(new SkillProfileItem(skill));
        }

        public void RemoveSkill(int skillId)
        {
            _skills.RemoveAll(s => s.Skill.Id == skillId);
        }
    }

    public class SkillProfileItem
    {
        private readonly Skill _skill;

        public SkillProfileItem(Skill skill)
        {
            _skill = skill;
        }

        public Skill Skill 
        { 
            get { return _skill; }
        }

        public ExperienceLevel ExperienceLevel { get; set; }
    }
}