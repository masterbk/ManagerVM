using ManagerVM.Contacts.Dtos;
using ManagerVM.Data;
using ManagerVM.Services.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.User.Queries
{
    public class GetUserByUserNamePasswordQuery: IRequest<UserDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class GetUserByUserNamePasswordQueryHandler : IRequestHandler<GetUserByUserNamePasswordQuery, UserDto>
    {
        private readonly VMDbContext _dbContext;
        public GetUserByUserNamePasswordQueryHandler(VMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDto> Handle(GetUserByUserNamePasswordQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);
            if(user?.CheckPasswordValid(request.Password) == true)
            {
                return user.MapTo<UserDto>();
            }

            throw new Exception("Usename or password is incorrect");
        }
    }
}
