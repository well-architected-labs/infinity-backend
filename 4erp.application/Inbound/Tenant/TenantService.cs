using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using _4erp.domain.Services.Tenant;
using _4erp.domain.Services.Users;
using _4erp.api.entities;

namespace _4erp.application.services
{
    public class TenantService : ITenantService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public TenantService(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<User?> GetCurrentAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new Exception("Tenant não encontrado!");

            if (!Guid.TryParse(userId, out Guid userGuid))
                throw new Exception("Tenant não encontrado!");

            return await _userService.FindFirstAsync(userId);
        }
    }
}
