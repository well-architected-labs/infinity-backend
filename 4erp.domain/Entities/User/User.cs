using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using _4erp.api.entities.person;

namespace _4erp.api.entities;

[Table("4erp_user")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column(TypeName = "nvarchar(255)")]
    public string Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres")]
    public string Password { get; set; }

    [JsonIgnore]
    [ForeignKey("Person")]
    public Guid? PersonId { get; set; }
    public Person? Person { get; set; }


    [JsonIgnore]
    [ForeignKey("Role")]
    public Guid? RoleId { get; set; }
    [Required]
    public Role Role { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? deletedAt { get; set; }


    public User() { }
    public User(string email, string password, Role role)
    {
        Email = email;
        Password = password;
        Role = role;
    }

    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
