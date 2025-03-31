using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using _4erp.api.entities.vacancy;

namespace _4erp.api.entities.ocupation;

[Table("4erp_ocupation")]
public class Ocupation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    [JsonIgnore]
    public ICollection<Vacancy>? Vacancies { get; set; }

    public Ocupation() { }
}
