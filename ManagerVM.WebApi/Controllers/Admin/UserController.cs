using ManagerVM.Contacts.Dtos;
using ManagerVM.Data.Entities;
using ManagerVM.Data.Helper;
using ManagerVM.Services.Features.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerVM.WebApi.Controllers.Admin
{
    public class UserController : AdminBaseController
    {
        public UserController(ISender sender, CurrentUserProvider currentUserProvider) : base(sender, currentUserProvider)
        {

        }

        [HttpPost("")]
        public async Task<UserDto> CreateAsync([FromBody] CreateUserCommand request)
        {
            if (_currentUserProvider.IsAdmin)
            {
                return await _sender.Send(request);
            }

            throw new Exception("Not permission!");
        }
    }
}
