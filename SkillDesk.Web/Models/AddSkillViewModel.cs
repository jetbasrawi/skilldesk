using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Web.Models
{
    [Serializable]
    public class AddSkillViewModel
    {
        public string SkillName;
        public string SkillPath;
        public SkillProfile SkillProfile;
        public int ExperinenceTypeId;
    }
}