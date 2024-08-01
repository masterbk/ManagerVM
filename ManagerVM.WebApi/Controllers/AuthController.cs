using ManagerVM.Contacts.Dtos;
using ManagerVM.Data.Helper;
using ManagerVM.Services.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerVM.WebApi.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(ISender sender, CurrentUserProvider currentUserProvider) : base(sender, currentUserProvider)
        {
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<TokenDto> Authenticate([FromBody]GetUserByUserNamePasswordQuery query)
        {
            var user = await _sender.Send(query);
            var token = await _sender.Send(new GetTokenQuery { User = user });

            return token;
        }
    }
}
