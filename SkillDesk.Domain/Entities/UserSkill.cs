using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SkillDesk.Domain.Entities
{
    [Serializable]
    public class UserSkill
    {
        private readonly int? _skillId;
        private Guid? _parentId;
        public Guid? ParentId { 
            get { return _parentId; }
            set { 
                if (value == this.Id)
                    throw new InvalidOperationException("A user skill can not be a parent of itself");
                _parentId = value;
            }
        }

        public Guid? Id { get; set; }
        public int? SkillId { get; set; }
        public string SkillName { get; set; }
        public ExperienceType ExperienceType { get; set; }
        public string Summary { get; set; }
        private List<UserSkill> _children = new List<UserSkill>();
        
        [XmlIgnore]
        public List<UserSkill> Children { get { return _children; } set { _children = value; }}

        public UserSkill() {
            Id = Guid.NewGuid();
        }

        public string GetSkillPath(List<UserSkill> userSkills) {
            StringBuilder sb = new StringBuilder();
            
            if (this.ParentId.HasValue) {
                var parent = userSkills.FirstOrDefault(s => s.Id.Equals(this.ParentId));
                if(parent != null)
                    sb.Append(parent.GetSkillPath(userSkills));
            }

            sb.AppendFormat("{0}/", this.UrlSafeName);

            return sb.ToString();
        }

        public string UrlSafeName { get { return this.SkillName; } }

        public UserSkill Clone() {
            return new UserSkill() {
                                       Children = this.Children, 
                                       ParentId = this.ParentId, 
                                       ExperienceType = this.ExperienceType, 
                                       SkillId = this.SkillId,
                                       SkillName = this.SkillName
                                   };
        }
    }
}