using ManagerVM.Contacts.Dtos;
using ManagerVM.Data;
using ManagerVM.Data.Entities;
using ManagerVM.Services.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.OpenStack.Commands
{
    public class CreateOpenStackCommand: IRequest<OpenStackDto>
    {
        public string EndPointUrl { get; set; }
        public string SecretKey { get; set; }
    }

    public class CreateOpenStackCommandHandler : IRequestHandler<CreateOpenStackCommand, OpenStackDto>
    {
        private readonly VMDbContext _dbContext;
        public CreateOpenStackCommandHandler(VMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OpenStackDto> Handle(CreateOpenStackCommand request, CancellationToken cancellationToken)
        {
            var entity = request.MapTo<OpenStackEntity>();
            await _dbContext.OpenStackEntities.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.MapTo<OpenStackDto>();
        }
    }
}
