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
    public class RemoveOpenStackToTenantCommand : IRequest<bool>
    {
        public long OpenStackId { get; set; }
        public long TenantId { get; set; }
    }

    public class RemoveOpenStackToTenantCommandHandler : IRequestHandler<RemoveOpenStackToTenantCommand, bool>
    {
        private readonly VMDbContext _dbContext;
        public RemoveOpenStackToTenantCommandHandler(VMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(RemoveOpenStackToTenantCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.OpenStackInTenantEntities
                .FirstOrDefaultAsync(f=>f.TenantId == request.TenantId && f.OpenStackId == request.OpenStackId, cancellationToken);

            _dbContext.Remove(entity);

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
