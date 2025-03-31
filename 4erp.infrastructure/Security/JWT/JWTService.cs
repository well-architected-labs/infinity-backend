using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace _4erp.infrastructure.Security.JWT;

public class JWTService : IJWTService
{
    private readonly IConfiguration _config;

    private readonly JWTSettings _jwtSettings;

    public JWTService(IConfiguration config, IOptions<JWTSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
        _config = config;
    }

    public string WriteToken(string userId, JWTRole role)
    {
        var token = new JwtSecurityToken(
            issuer: "4erp.ai",
            audience: "4erp.ai",
            claims: new[]
                    {
                            new Claim(JwtRegisteredClaimNames.Sub.ToString(), userId),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    },
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings is not null && _jwtSettings.Secret is not null ? _jwtSettings.Secret : "")),
                     SecurityAlgorithms.HmacSha256)
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

