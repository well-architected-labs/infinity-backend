using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _4erp.api.entities;

[Table("4erp_role")]
public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Alias { get; set; }
    public string Description { get; set; }
    

    [JsonIgnore]
    public ICollection<User>? Users { get; set; }

    public ICollection<Scope> Scopes { get; set; }

    public Role() { }

}
