using Hangfire;
using ManagerVM.Data;
using ManagerVM.Services.Features.VM.Commands;
using ManagerVM.Services.Features.VM.Handlers;
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

namespace ManagerVM.Services.Features.VM.InstallServices
{
    public class InstallDockerService : INotificationHandler<VMActivedNotification>
    {
        private readonly IBackgroundJobClient _jobClient;
        private readonly IOpenStackClient _openStackClient;
        private readonly IMediator _mediator;
        private readonly IAppLogger<InstallDockerService> _appLogger;
        public InstallDockerService(IBackgroundJobClient jobClient, IOpenStackClient openStackClient,
            IMediator mediator, IAppLogger<InstallDockerService> appLogger)
        {
            _jobClient = jobClient;
            _openStackClient = openStackClient;
            _mediator = mediator;
            _appLogger = appLogger;
        }

        public Task Handle(VMActivedNotification notification, CancellationToken cancellationToken)
        {
            _jobClient.Enqueue(() => InstallDockerAsync(notification));
            return Task.CompletedTask;
        }

        public async Task<bool> InstallDockerAsync(VMActivedNotification notification)
        {
            var vmEntity = await _mediator.Send(new GetVMQuery { InstanceId = notification.VMInstanceId });

            if (vmEntity == null)
            {
                _appLogger.LogError($"[{nameof(InstallDockerService)}]VM is not exist! (VmId = {notification.VMInstanceId})");
                return false;
            }

            vmEntity.HostStatus = Contacts.Enums.HostStatus.InstallingDocker;
            await _mediator.Send(new UpdateVMCommand { VMEntity = vmEntity });

            var res = await _openStackClient.InstallServiceAsync(notification.OpenStackEndPointUrl, notification.VMInstanceId, ServiceConstants.INSTALL_DOCKER);

            if (res)
            {
                vmEntity.HostStatus = Contacts.Enums.HostStatus.InstalledDocker;
                await _mediator.Send(new UpdateVMCommand { VMEntity = vmEntity });

                await _mediator.Publish(new InstalledDockerNotification { OpenStackEndPointUrl = notification.OpenStackEndPointUrl, VMInstanceId = notification.VMInstanceId });
                return true;
            }
            else
            {
                vmEntity.HostStatus = Contacts.Enums.HostStatus.InstallDockerError;
                await _mediator.Send(new UpdateVMCommand { VMEntity = vmEntity });

                throw new Exception($"Install Docker error! (VmId = {notification.VMInstanceId})");
            }
        }
    }
}
