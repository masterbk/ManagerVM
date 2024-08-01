using ManagerVM.Contacts.Dtos;
using ManagerVM.Data.Helper;
using ManagerVM.Services.Features.OpenStack.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerVM.WebApi.Controllers.Admin
{
    public class OpenStackController : AdminBaseController
    {
        public OpenStackController(ISender sender, CurrentUserProvider currentUserProvider) : base(sender, currentUserProvider)
        {
        }

        [HttpPost("")]
        public async Task<OpenStackDto> CreateAsync([FromBody] CreateOpenStackCommand request)
        {
            return await _sender.Send(request);
        }

        [HttpPost("AddToTenant")]
        public async Task<bool> AddToTenantAsync([FromBody] AddOpenStackToTenantCommand request)
        {
            return await _sender.Send(request);
        }
    }
}
