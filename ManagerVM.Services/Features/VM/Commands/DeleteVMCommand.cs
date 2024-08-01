using ManagerVM.Data;
using ManagerVM.Services.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.VM.Commands
{
    public class DeleteVMCommand: IRequest<bool>
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
    }

    public class DeleteVMCommandHandler : IRequestHandler<DeleteVMCommand, bool>
    {
        private readonly VMDbContext _dbContext;
        private readonly IOpenStackClient _openStackClient;
        public DeleteVMCommandHandler(VMDbContext dbContext, IOpenStackClient openStackClient)
        {
            _dbContext = dbContext;
            _openStackClient = openStackClient;
        }

        public async Task<bool> Handle(DeleteVMCommand request, CancellationToken cancellationToken)
        {
            var vmEntity = await _dbContext.VMEntities
                .FirstOrDefaultAsync(f => f.Id == request.Id && (request.TenantId == 0 || f.TenantId == request.TenantId), cancellationToken);

            var openStack = await _dbContext.OpenStackEntities.AsNoTracking()
                .FirstOrDefaultAsync(f=>f.Id == vmEntity.OpenStackId, cancellationToken);

            var isDeleted = await _openStackClient.DeleteVMAsync(openStack.EndPointUrl, vmEntity.InstanceId);

            if (isDeleted)
            {
                _dbContext.VMEntities.Remove(vmEntity);
                var res = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

                return res;
            }

            return false;
        }
    }
}
