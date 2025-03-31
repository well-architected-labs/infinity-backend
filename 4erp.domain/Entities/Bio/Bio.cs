using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using _4erp.api.entities.candidature;
using _4erp.api.entities.person;
using _4erp.api.entities.vacancy;

namespace _4erp.api.entities.status;

[Table("4erp_bio")]
public class Bio
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? About { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? Resume { get; set; }
    public string? Linkedin { get; set; }
    public string? WebSite { get; set; }

    [JsonIgnore]
    public ICollection<Person>? Persons { get; set; }
    public ICollection<Experience> Experiences { get; set; } = [];
    public ICollection<Education> Educations { get; set; } = [];

    public Bio() { }
}
