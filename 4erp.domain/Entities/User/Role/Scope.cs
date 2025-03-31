using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _4erp.api.entities;

[Table("4erp_scope")]
public class Scope
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public string? Alias { get; set; }
    public string? Description { get; set; }

    [ForeignKey("Role")]
    [JsonIgnore]
    public Guid RoleId { get; set; } 

    [JsonIgnore]
    public Role? Role { get; set; } 

}
