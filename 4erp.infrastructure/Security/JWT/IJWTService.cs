
namespace _4erp.infrastructure.Security.JWT;
public interface IJWTService
{
    string WriteToken(string userId, JWTRole role);
}