using ManagerVM.Data.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerVM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class BaseController : ControllerBase
    {
        public readonly ISender _sender;
        public readonly CurrentUserProvider _currentUserProvider;
        public BaseController(ISender sender, CurrentUserProvider currentUserProvider)
        {
            _sender = sender;
            _currentUserProvider = currentUserProvider;
        }
    }
}
