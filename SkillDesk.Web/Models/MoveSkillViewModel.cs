using System;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Web.Models
{
    [Serializable]
    public class MoveSkillViewModel
    {
        public UserSkill Root { get; set; }
        public Guid[] SelectedItems { get; set; }
        public Guid[] TargetItems { get; set; }
    }
}