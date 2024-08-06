using Hangfire;
using ManagerVM.Data;
using ManagerVM.Data.Entities;
using ManagerVM.Services.Features.VM.Commands;
using ManagerVM.Services.Features.VM.Notifications;
using ManagerVM.Services.Features.VM.Notifications.DatabaseInstall;
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
    public class CheckInitDbHandler : INotificationHandler<InstalledAllServiceSuccessNotification>
    {
        private readonly ILMSClient _lmsClient;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMediator _mediator;
        public CheckInitDbHandler(ILMSClient lmsClient, IBackgroundJobClient backgroundJobClient,  IMediator mediator)
        {
            _lmsClient = lmsClient;
            _backgroundJobClient = backgroundJobClient;
            _mediator = mediator;
        }

        public Task Handle(InstalledAllServiceSuccessNotification notification, CancellationToken cancellationToken)
        {
            _backgroundJobClient.Enqueue(() => CheckLoginLMSAsync(notification));
            return Task.CompletedTask;
        }

        public async Task<bool> CheckLoginLMSAsync(InstalledAllServiceSuccessNotification notification)
        {
            var vm = await _mediator.Send(new GetVMQuery { InstanceId = notification.VMInstanceId });
            if (vm == null) { return false; }

            if(vm.HostStatus == Contacts.Enums.HostStatus.CreatedDatabase) { return true; }

            vm.HostStatus = Contacts.Enums.HostStatus.CreatingDatabase;
            await _mediator.Send(new UpdateVMCommand { VMEntity = vm });

            var token = await _lmsClient.GetTokenAsync(vm.HostName);

            if (string.IsNullOrWhiteSpace(token)) throw new Exception("Initing database...!");

            vm.HostStatus = Contacts.Enums.HostStatus.CreatedDatabase;
            await _mediator.Send(new UpdateVMCommand { VMEntity = vm.MapTo<VMEntity>() });

            await _mediator.Publish(new InitializationSuccessfulDatabaseNotification { VMInstanceId = notification.VMInstanceId });

            return true;
        }
    }
}
