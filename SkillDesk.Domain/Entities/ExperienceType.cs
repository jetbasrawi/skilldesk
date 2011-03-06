using System;
using System.Data.Linq.Mapping;

namespace SkillDesk.Domain.Entities
{
    [Serializable]
    [Table]
    public class ExperienceType
    {
        [Column] 
        public int Id { get; set; }
        
        [Column]
        public string Name { get; set; }
        
        [Column]
        public decimal Value { get; set; }
        
        [Column]
        public string Description { get; set; }
    }
}
