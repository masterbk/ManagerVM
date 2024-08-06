using ManagerVM.Data;
using ManagerVM.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.VM.Queries
{
    public class GetVMQuery: IRequest<VMEntity>
    {
        public string InstanceId { get; set; }
    }

    public class GetVMQueryHandler : IRequestHandler<GetVMQuery, VMEntity>
    {
        private readonly VMDbContext _dbContext;
        public GetVMQueryHandler(VMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VMEntity> Handle(GetVMQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.VMEntities.AsNoTracking().FirstOrDefaultAsync(f=>f.InstanceId.Equals(request.InstanceId) && !f.IsDeleted, cancellationToken);
        }
    }
}
