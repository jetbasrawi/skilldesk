using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Domain.Concrete
{
    public class FakeSkillsRepository : ISkillsRepository
    {
        private static readonly IQueryable<Skill> FakeSkills = new List<Skill>() {
            new Skill { Name = "C#", ShortDescription = "An object oriented programming language from Microsoft."},
            new Skill { Name = "ASP.NET", ShortDescription = "A framework for web development." },
            new Skill { Name = "Web Developement", ShortDescription = "The ability to develop software solutions for the web." }
        }.AsQueryable();

        public IQueryable<Skill> Skills
        {
            get { return FakeSkills; }
        }

        public void Save(Skill skill)
        {
            throw new NotImplementedException();
        }

        public void DeleteSkill(Skill skill)
        {
            throw new NotImplementedException();
        }
    }
}
