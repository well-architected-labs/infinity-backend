using _4erp.api.entities;

namespace _4erp.domain.Services.Tenant;

public interface ITenantService
{
    Task<User?> GetCurrentAsync();
}

