using ManagerVM.Data;
using ManagerVM.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.VM.Commands
{
    public class UpdateVMCommand: IRequest<bool>
    {
        public VMEntity VMEntity { get; set; }
    }

    public class UpdateVMCommandHandler : IRequestHandler<UpdateVMCommand, bool>
    {
        private readonly VMDbContext _dbContext;
        public UpdateVMCommandHandler(VMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(UpdateVMCommand request, CancellationToken cancellationToken)
        {
            var vm = await _dbContext.VMEntities.FirstOrDefaultAsync(f=>f.Id == request.VMEntity.Id, cancellationToken);
            vm.HostStatus = request.VMEntity.HostStatus;
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
