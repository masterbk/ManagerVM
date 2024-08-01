using ManagerVM.Contacts.Dtos;
using ManagerVM.Contacts.Models.OpenStackResponses;
using ManagerVM.Data;
using ManagerVM.Data.Entities;
using ManagerVM.Data.Helper;
using ManagerVM.Services.Features.VM.Notifications;
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
    public class CreateVMCommand: IRequest<VMDto>
    {
        public string Name { get; set; }
        public long RoomId { get; set; }
    }

    public class CreateVMCommandHandler : IRequestHandler<CreateVMCommand, VMDto>
    {
        private readonly VMDbContext _dbContext;
        private readonly IOpenStackClient _openStackClient;
        private readonly CurrentUserProvider _currentUserProvider;
        private readonly IMediator _mediator;
        public CreateVMCommandHandler(VMDbContext dbContext, IOpenStackClient openStackClient, 
            CurrentUserProvider currentUserProvider, IMediator mediator)
        {
            _dbContext = dbContext;
            _openStackClient = openStackClient;
            _currentUserProvider = currentUserProvider;
            _mediator = mediator;
        }

        public async Task<VMDto> Handle(CreateVMCommand request, CancellationToken cancellationToken)
        {
            var openStackInTenant = await _dbContext.OpenStackInTenantEntities
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.TenantId == _currentUserProvider.TenantId, cancellationToken);

            if (openStackInTenant == null)
            {
                throw new Exception("Not permission!");
            }

            var openStack = await _dbContext.OpenStackEntities.AsNoTracking()
                .FirstOrDefaultAsync(f=>f.Id == openStackInTenant.OpenStackId, cancellationToken);

            if (openStack == null)
            {
                throw new Exception($"Not found OpenStack System (Id={openStackInTenant.OpenStackId})!");
            }

            var response = await _openStackClient.CreateVMAsync<CreatedVMResponse>(request.Name, openStack.EndPointUrl, cancellationToken);
            if (response != null)
            {
                var vmEntity = response.MapTo<VMEntity>();
                vmEntity.OpenStackId = openStack.Id;
                vmEntity.RoomId = request.RoomId;

                await _dbContext.VMEntities.AddAsync(vmEntity, cancellationToken);
                var isSave = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

                if (isSave)
                {
                    await _mediator.Publish(new VMCreatedNotification
                    {
                        InstanceId = vmEntity.InstanceId,
                        Name = vmEntity.Name,
                        OpenStackId = vmEntity.OpenStackId,
                    }, cancellationToken);
                }

                return vmEntity.MapTo<VMDto>();
            }

            throw new Exception("Create VM error!");
        }
    }
}
