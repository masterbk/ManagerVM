using Hangfire;
using ManagerVM.Data;
using ManagerVM.Services.Features.VM.Notifications;
using ManagerVM.Services.Features.VM.Notifications.DatabaseInstall;
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
        private readonly VMDbContext _dbContext;
        private readonly IMediator _mediator;
        public CheckInitDbHandler(ILMSClient lmsClient, IBackgroundJobClient backgroundJobClient, VMDbContext dbContext, IMediator mediator)
        {
            _lmsClient = lmsClient;
            _backgroundJobClient = backgroundJobClient;
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public Task Handle(InstalledAllServiceSuccessNotification notification, CancellationToken cancellationToken)
        {
            _backgroundJobClient.Enqueue(() => CheckLoginLMSAsync(notification));
            return Task.CompletedTask;
        }

        public async Task<bool> CheckLoginLMSAsync(InstalledAllServiceSuccessNotification notification)
        {
            var vm = await _dbContext.VMEntities.FirstOrDefaultAsync(f => f.InstanceId.Equals(notification.VMInstanceId) && !f.IsDeleted);
            if (vm == null) { return false; }
            var token = await _lmsClient.GetTokenAsync(vm.HostName);

            if (string.IsNullOrWhiteSpace(token)) throw new Exception("Initing database...!");

            await _mediator.Publish(new InitializationSuccessfulDatabaseNotification { VMInstanceId = notification.VMInstanceId });

            return true;
        }
    }
}
