using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using _4erp.api.entities.candidature;
using _4erp.api.entities.vacancy;

namespace _4erp.api.entities.status;

[Table("4erp_status")]
public class Status
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Description { get; set; }


    [JsonIgnore]
    public ICollection<Candidature>? Candidatures { get; set; }

    [JsonIgnore]
    public ICollection<Vacancy>? Vacancies { get; set; }

    public Status() { }
}
