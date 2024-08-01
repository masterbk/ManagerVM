using ManagerVM.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ManagerVM.Data.Helper
{
    public class CurrentUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserProvider(IHttpContextAccessor  httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long TenantId
        {
            get
            {
                var tenant = _httpContextAccessor?.HttpContext?.User?.FindFirst(AuthorizationConstants.CLAIM_TENANT_ID)?.Value;
                long.TryParse(tenant, out var tenantId);
                return tenantId;
            }
        }

        public long UserId { get
            {
                var nameIdentifier = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                long.TryParse(nameIdentifier, out var userId);
                return userId;
            }
        }

        public bool IsAdmin
        {
            get
            {
                var isAdmin = _httpContextAccessor?.HttpContext?.User?.IsInRole(AuthorizationConstants.ADMIN_ROLE);

                return isAdmin??false;
            }
        }
    }
}
