using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Web.Mvc;

namespace SkillDesk.Domain.Entities
{
    [Table( Name = "Skill")]
    public class Skill
    {
        [ScaffoldColumn(false)]
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a skill name")]
        [Column] public string Name { get;  set; }

        [DataType(DataType.MultilineText)]
        [Column] public string ShortDescription { get; set; }

        [Column] public string Category { get; set; }

        [Column]
        public byte[] ImageData { get; set; }

        [ScaffoldColumn(false)]
        [Column]
        public string ImageMimeType { get; set; }


        public Skill()
        {
        }
    }
}
