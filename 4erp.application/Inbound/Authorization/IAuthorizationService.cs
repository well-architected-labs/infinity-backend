
namespace _4erp.application.Inbound.Authorization
{
    public interface IAuthorizationService
    {
        Task<Authorized> Authorization(Authorization authorization);
        Task<Authorized> Register(Authorization authorization);
    }
}