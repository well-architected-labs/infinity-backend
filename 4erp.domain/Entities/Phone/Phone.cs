using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using _4erp.api.entities.candidature;
using _4erp.api.entities.person;
using _4erp.api.entities.vacancy;

namespace _4erp.api.entities.status;

[Table("4erp_phone")]
public class Phone
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string? DDI { get; set; }
    public string? DDD { get; set; }
    public string? Number { get; set; }

    [JsonIgnore]
    public ICollection<Person>? Persons { get; set; }

    public Phone() { }
}
