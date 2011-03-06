using System;
using System.Collections.Generic;

namespace SkillDesk.Domain.Entities
{
    public class User
    {
        private List<Skill> _skills = new List<Skill>();

        public void AddSkill(Skill skill)
        {
            this._skills.Add(skill);   
        }
        
        public List<Skill> GetSkills()
        {
            return _skills; 
        }

        public User()
        {
        }
    }
}