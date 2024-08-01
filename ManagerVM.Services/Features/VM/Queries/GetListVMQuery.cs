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

namespace ManagerVM.Services.Features.VM.Queries
{
    public class GetListVMQuery: IRequest<List<VMDto>>
    {
        public string Keyword { get; set; }
        public long TenantId { get; set; }
    }

    public class GetListVMQueryHandler : IRequestHandler<GetListVMQuery, List<VMDto>>
    {
        private readonly VMDbContext _dbContext;
        public GetListVMQueryHandler(VMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<VMDto>> Handle(GetListVMQuery request, CancellationToken cancellationToken)
        {
            var vms = await _dbContext.VMEntities.Where(v => EF.Functions.Like(v.Name, $"%{request.Keyword}%") && v.TenantId == request.TenantId && !v.IsDeleted).ToListAsync();

            return vms.MapTo<List<VMDto>>();
        }
    }
}
