using Hangfire;
using ManagerVM.Data;
using ManagerVM.Services.Features.VM.Notifications;
using ManagerVM.Services.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.VM.InstallServices
{
    public class InstallLMSService : INotificationHandler<InstalledDockerNotification>
    {
        private readonly IBackgroundJobClient _jobClient;
        private readonly IOpenStackClient _openStackClient;
        private readonly IMediator _mediator;
        private readonly VMDbContext _dbContext;
        private readonly IAppLogger<InstallLMSService> _appLogger;
        public InstallLMSService(IBackgroundJobClient jobClient, IOpenStackClient openStackClient, IMediator mediator,
            VMDbContext dbContext, IAppLogger<InstallLMSService> appLogger)
        {
            _jobClient = jobClient;
            _openStackClient = openStackClient;
            _mediator = mediator;
            _dbContext = dbContext;
            _appLogger = appLogger;
        }

        public Task Handle(InstalledDockerNotification notification, CancellationToken cancellationToken)
        {
            _jobClient.Enqueue(() => InstallLMS(notification));

            return Task.CompletedTask;
        }

        public async Task<bool> InstallLMS(InstalledDockerNotification notification)
        {
            var vmEntity = await _dbContext.VMEntities.FirstOrDefaultAsync(f => f.InstanceId.Equals(notification.VMInstanceId) && !f.IsDeleted);

            if (vmEntity == null)
            {
                _appLogger.LogError($"[{nameof(InstallDockerService)}]VM is not exist! (VmId = {notification.VMInstanceId})");
                return false;
            }

            var res = await _openStackClient.InstallServiceAsync(notification.OpenStackEndPointUrl, notification.VMInstanceId, 
                ServiceConstants.INSTALL_LMS.Replace("@_FullServerNameReplace", "http:\\/\\/" + vmEntity.Address).Replace("@_ServerNameReplace", vmEntity.Address));

            if (res)
            {
                //Install LMS success
                await _mediator.Publish(new InstalledAllServiceSuccessNotification { VMInstanceId = notification.VMInstanceId });
                return true;
            }
            else
            {
                throw new Exception($"Install Docker error! (VmId = {notification.VMInstanceId})");
            }
        }
    }
}
