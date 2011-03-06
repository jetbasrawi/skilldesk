using System.Linq;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Domain.Abstract
{
    public interface ISkillProfileRepository
    {
        void Save(SkillProfile skill);
        SkillProfile Load();
    }
}