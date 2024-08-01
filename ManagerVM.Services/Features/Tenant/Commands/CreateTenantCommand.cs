using ManagerVM.Contacts.Dtos;
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

namespace ManagerVM.Services.Features.Tenant.Commands
{
    public class CreateTenantCommand: IRequest<TenantDto>
    {
        public string Name {  get; set; }
        public string Code { get; set; }
    }

    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, TenantDto>
    {
        private readonly VMDbContext _dbContext;
        public CreateTenantCommandHandler(VMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TenantDto> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenantDb = await _dbContext.TenantEntities.FirstOrDefaultAsync(f => f.Code.Equals(request.Code), cancellationToken);
            if(tenantDb == null)
            {
                var tenantEntity = request.MapTo<TenantEntity>();
                await _dbContext.TenantEntities.AddAsync(tenantEntity, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return tenantEntity.MapTo<TenantDto>();
            }

            throw new Exception("Code is exist!");
        }
    }
}
