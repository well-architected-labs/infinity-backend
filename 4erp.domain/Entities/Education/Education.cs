using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _4erp.api.entities.status;

[Table("4erp_education")]
public class Education
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? DateInit { get; set; }
    public DateTime? DateEnd { get; set; }

    [ForeignKey("Bio")]
    [JsonIgnore]
    public Guid BioId { get; set; }
    [JsonIgnore]
    public Bio? Bio { get; set; } 

    [JsonIgnore]
    public ICollection<Bio>? Bios { get; set; }

    public Education() { }
}
