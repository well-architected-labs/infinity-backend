using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using _4erp.api.entities.person;
using _4erp.api.entities.status;
using _4erp.api.entities.vacancy;

namespace _4erp.api.entities.candidature
{
    [Table("4erp_candidature")]
    public class Candidature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? deletedAt { get; set; }


        [JsonIgnore]
        [ForeignKey("Person")]
        public Guid PersonId { get; set; }
        public Person? Person { get; set; }


        [JsonIgnore]
        [ForeignKey("Vacancy")]
        public Guid VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }


        [JsonIgnore]
        [ForeignKey("Status")]
        public Guid StatusId { get; set; }
        public Status Status { get; set; }

        public Candidature() { }
        public Candidature(Guid id)
        {
            this.Id = id;
        }
    }
}
