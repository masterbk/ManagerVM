using ManagerVM.Data;
using ManagerVM.Data.Entities;
using ManagerVM.Services.Features.VM.Commands;
using ManagerVM.Services.Features.VM.Notifications;
using ManagerVM.Services.Features.VM.Queries;
using ManagerVM.Services.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.VM.Handlers
{
    public class InstalledAllServiceSuccessNotificationHandler : INotificationHandler<InstalledAllServiceSuccessNotification>
    {
        private readonly IMediator _mediator;
        public InstalledAllServiceSuccessNotificationHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(InstalledAllServiceSuccessNotification notification, CancellationToken cancellationToken)
        {
            var vmEntity = await _mediator.Send(new GetVMQuery { InstanceId = notification.VMInstanceId });//_dbContext.VMEntities.FirstOrDefaultAsync(f=>f.InstanceId.Equals(notification.VMInstanceId), cancellationToken);

            if (vmEntity == null)
            {
                throw new Exception($"[{nameof(InstalledAllServiceSuccessNotificationHandler)}]VM not exist! (VMId = {notification.VMInstanceId})");
            }

            vmEntity.InstallAllServiceSuccess = true;
            
            await _mediator.Send(new UpdateVMCommand { VMEntity = vmEntity.MapTo<VMEntity>() });
            //Callback LMS to return info
        }
    }
}
