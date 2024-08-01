using ManagerVM.Contacts.Dtos;
using ManagerVM.Data.Entities;
using ManagerVM.Data.Helper;
using ManagerVM.Services.Features.Tenant.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerVM.WebApi.Controllers.Admin
{
    public class TenantController : AdminBaseController
    {
        public TenantController(ISender sender, CurrentUserProvider currentUserProvider) : base(sender, currentUserProvider)
        {
        }

        [HttpPost("")]
        public async Task<TenantDto> CreateAsync([FromBody]CreateTenantCommand request)
        {
            return await _sender.Send(request);
        }
    }
}
