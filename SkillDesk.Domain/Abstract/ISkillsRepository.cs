using System.Linq;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Domain.Abstract
{
    public interface ISkillsRepository
    {
        IQueryable<Skill> Skills { get; }
        void Save(Skill skill);
        void DeleteSkill(Skill skill);
    }
}
