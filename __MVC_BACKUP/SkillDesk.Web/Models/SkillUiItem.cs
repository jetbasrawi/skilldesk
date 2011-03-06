using System.Web.Routing;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Web.Models
{
    public class SkillUiItem
    {
        public Skill Skill{ get; set; }
        public RouteValueDictionary Routes { get; set; }
    }
}