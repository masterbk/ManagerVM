using ManagerVM.Data.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerVM.WebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    public class AdminBaseController : BaseController
    {
        public AdminBaseController(ISender sender, CurrentUserProvider currentUserProvider) : base(sender, currentUserProvider)
        {
        }
    }
}
