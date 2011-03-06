using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Domain.Services
{
    public interface IProfileSaver
    {
        void SaveProfile(UserProfile profile);
    }
}
