using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using _4erp.api.entities.candidature;
using _4erp.api.entities.skill;
using _4erp.api.entities.status;
using _4erp.api.entities.vacancy;

namespace _4erp.api.entities.person
{
    [Table("4erp_person")]
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LegalName { get; set; }
        public string? FantasyName { get; set; }
        public int Type { get; set; } = 0;
        public string? TaxId { get; set; }


        [JsonIgnore]
        [ForeignKey("Bio")]
        public Guid? BioId { get; set; }
        public Bio? Bio { get; set; }

        [JsonIgnore]
        [ForeignKey("Phone")]
        public Guid? PhoneId { get; set; }
        public Phone? Phone { get; set; }


        [JsonIgnore]
        public ICollection<User>? Users { get; set; }

        [JsonIgnore]
        public ICollection<Vacancy>? Vacancies { get; set; }

        [JsonIgnore]
        public ICollection<Candidature>? Candidatures { get; set; }


        public List<Skill>? Skills { get; set; } = [];

        public Person() { }
        public Person(string firstName, string lastName, string taxId)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
