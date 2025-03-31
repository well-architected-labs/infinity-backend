using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using _4erp.api.entities.candidature;
using _4erp.api.entities.ocupation;
using _4erp.api.entities.person;
using _4erp.api.entities.skill;
using _4erp.api.entities.status;

namespace _4erp.api.entities.vacancy;


[Table("4erp_vacancy")]
public class Vacancy
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [JsonIgnore]
    public ICollection<Candidature>? Candidatures { get; set; }


    [JsonIgnore]
    [ForeignKey("Ocupation")]
    public Guid OcupationId { get; set; }
    public Ocupation Ocupation { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DateInit { get; set; }  = DateTime.UtcNow;
    public DateTime? DateEnd { get; set; }  = DateTime.UtcNow;
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    [JsonIgnore]
    public DateTime? deletedAt { get; set; }


    [JsonIgnore]
    [ForeignKey("Status")]
    public Guid? StatusId { get; set; }
    public Status Status { get; set; }


    [JsonIgnore]
    [ForeignKey("Person")]
    public Guid? PersonId { get; set; }
    public Person? Person { get; set; }

    public List<Skill> Skills { get; set; }

    public Vacancy() { }


}
