using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using _4erp.api.entities.person;
using _4erp.api.entities.vacancy;

namespace _4erp.api.entities.skill
{
    [Table("4erp_skill")]
    public class Skill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public List<Person>? Persons { get; set; }
        [JsonIgnore]
        public List<Vacancy>? Vacancies { get; set; }

        public Skill() { }
    }
}
