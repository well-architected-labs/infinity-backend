
namespace _4erp.application.Inbound.Authorization
{
    public class Authorization
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Addiciontal? Person { get; set; }
        public AuthorizationRoleEnum? Role { get; set; }
    }

    public class Addiciontal
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LegalName { get; set; }
        public string? FantasyName { get; set; }
        public int Type { get; set; } = 1;
        public string? TaxId { get; set; }
    }
}