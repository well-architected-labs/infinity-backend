namespace _4erp.infrastructure.Security.JWT;

public class JWTSettings
{
    public string Secret { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public int ExpirationInMinutes { get; set; }

    public JWTSettings() { }
}
