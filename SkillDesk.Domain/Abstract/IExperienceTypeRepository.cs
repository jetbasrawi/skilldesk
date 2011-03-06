using System.Linq;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Domain.Abstract
{
    public interface IExperienceTypeRepository
    {
        IQueryable<ExperienceType> ExperienceLevels { get; }
    }
}