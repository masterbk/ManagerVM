using ManagerVM.Contacts.Dtos;
using ManagerVM.Data;
using ManagerVM.Data.Entities;
using ManagerVM.Services.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ManagerVM.Services.Features.User.Commands
{
    public class CreateUserCommand: IRequest<UserDto>
    {
        [Required]
        [RegularExpression("^[0-9a-zA-Z]{6,}$")]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string FullName { get; set; }
        public long TenantId { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly VMDbContext _dbContext;
        public CreateUserCommandHandler(VMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userDb = await _dbContext.Users.FirstOrDefaultAsync(f=>f.UserName == request.UserName, cancellationToken);
            if (userDb != null)
            {
                throw new Exception("UserName is exist!");
            }

            var userEntity = request.MapTo<UserEntity>();
            userEntity.Salt = Guid.NewGuid().ToString();
            userEntity.Password = $"{userEntity.Salt}{userEntity.Password}".ToSHA256Hash();

            await _dbContext.Users.AddAsync(userEntity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return userEntity.MapTo<UserDto>();
        }
    }
}
