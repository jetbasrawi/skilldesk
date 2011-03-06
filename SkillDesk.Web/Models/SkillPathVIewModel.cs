using System;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Web.Models
{
    public class SkillPathViewModel
    {
        public readonly SkillPath SkillPath;

        public SkillPathViewModel(string skillPath) {
            SkillPath = new SkillPath(skillPath);
        }
    }
}