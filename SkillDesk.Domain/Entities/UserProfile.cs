using System.ComponentModel.DataAnnotations;

namespace SkillDesk.Domain.Entities
{
    public class UserProfile
    {
        [Required(ErrorMessage = "Please provide a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide a country.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please provide a city.")]
        public string City { get; set; }

        public string Age { get; set; }
        
        public string Salary { get; set; }



    }
}