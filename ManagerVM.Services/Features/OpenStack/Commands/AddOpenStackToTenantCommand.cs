using ManagerVM.Data;
using ManagerVM.Data.Entities;
using ManagerVM.Services.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.OpenStack.Commands
{
    public class AddOpenStackToTenantCommand: IRequest<bool>
    {
        public long OpenStackId { get; set; }
        public long TenantId { get; set; }
    }

    public class AddOpenStackToTenantCommandHandler : IRequestHandler<AddOpenStackToTenantCommand, bool>
    {
        private readonly VMDbContext _dbContext;
        public AddOpenStackToTenantCommandHandler(VMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(AddOpenStackToTenantCommand request, CancellationToken cancellationToken)
        {
            var entityDb = await _dbContext.OpenStackInTenantEntities
                .FirstOrDefaultAsync(f=>f.OpenStackId == request.OpenStackId && f.TenantId == request.TenantId, cancellationToken);

            if (entityDb != null)
            {
                return true;
            }

            var entity = request.MapTo<OpenStackInTenantEntity>();
            await _dbContext.AddAsync(entity, cancellationToken);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
