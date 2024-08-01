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
    public class InstallServiceCommand: IRequest<bool>
    {
        public long VmId { get; set; }
        public long TenantId { get; set; }
        public string Script { get; set; }
    }

    public class InstallServiceCommandHandler : IRequestHandler<InstallServiceCommand, bool>
    {
        private readonly VMDbContext _dbContext;
        private readonly IOpenStackClient _openStackClient;
        public InstallServiceCommandHandler(VMDbContext dbContext, IOpenStackClient openStackClient)
        {
            _dbContext = dbContext;
            _openStackClient = openStackClient;
        }

        public async Task<bool> Handle(InstallServiceCommand request, CancellationToken cancellationToken)
        {
            var vmEntity = await _dbContext.VMEntities.AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == request.VmId && f.TenantId == request.TenantId && !f.IsDeleted, cancellationToken);

            if (vmEntity == null)
            {
                throw new Exception($"VM is not found (Id={request.VmId}, TenantId={request.TenantId})!");
            }

            var openStackEntity = await _dbContext.OpenStackEntities.AsNoTracking()
                .FirstOrDefaultAsync(f=>f.Id == vmEntity.OpenStackId && !f.IsDeleted, cancellationToken);

            if(openStackEntity == null)
            {
                throw new Exception($"OpenStack is not found (Id={vmEntity.OpenStackId})!");
            }

            return await _openStackClient.InstallServiceAsync(openStackEntity.EndPointUrl, vmEntity.InstanceId, request.Script.Replace("@_FullServerNameReplace", "http:\\/\\/" + vmEntity.Address)
                .Replace("@_ServerNameReplace", vmEntity.Address));
        }
    }
}
