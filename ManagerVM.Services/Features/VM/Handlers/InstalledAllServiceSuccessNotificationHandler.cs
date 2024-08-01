using ManagerVM.Data;
using ManagerVM.Services.Features.VM.Notifications;
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
        private readonly VMDbContext _dbContext;
        public InstalledAllServiceSuccessNotificationHandler(VMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(InstalledAllServiceSuccessNotification notification, CancellationToken cancellationToken)
        {
            var vmEntity = await _dbContext.VMEntities.FirstOrDefaultAsync(f=>f.InstanceId.Equals(notification.VMInstanceId), cancellationToken);

            if (vmEntity == null)
            {
                throw new Exception($"[{nameof(InstalledAllServiceSuccessNotificationHandler)}]VM not exist! (VMId = {notification.VMInstanceId})");
            }

            vmEntity.InstallAllServiceSuccess = true;
            await _dbContext.SaveChangesAsync(cancellationToken);

            //Callback LMS to return info
        }
    }
}
