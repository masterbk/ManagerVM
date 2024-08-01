using Hangfire;
using ManagerVM.Contacts.Models.OpenStackResponses;
using ManagerVM.Data;
using ManagerVM.Data.Entities;
using ManagerVM.Services.Features.VM.Notifications;
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
    public class CreatedVMNotificationHandler : INotificationHandler<VMCreatedNotification>
    {
        private readonly IBackgroundJobClient _jobClient;
        private readonly VMDbContext _dbContext;
        private readonly IOpenStackClient _openStackClient;
        private readonly IMediator _mediator;
        private readonly IAppLogger<CreatedVMNotificationHandler> _appLogger;
        public CreatedVMNotificationHandler(IBackgroundJobClient jobClient,
            VMDbContext dbContext, IOpenStackClient openStackClient, IMediator mediator,
            IAppLogger<CreatedVMNotificationHandler> appLogger)
        {
            _jobClient = jobClient;
            _dbContext = dbContext;
            _openStackClient = openStackClient;
            _mediator = mediator;
            _appLogger = appLogger;
        }

        public Task Handle(VMCreatedNotification notification, CancellationToken cancellationToken)
        {
            var strId = _jobClient.Enqueue(() => Enqueue(notification));

            return Task.CompletedTask;
        }

        public async Task<bool> Enqueue(VMCreatedNotification notification) {
            var openStack = await _dbContext.OpenStackEntities.FirstOrDefaultAsync(f=>f.Id == notification.OpenStackId && !f.IsDeleted);
            if(openStack != null)
            {
                var listVM = await _openStackClient.GetListVMAsync<List<CreatedVMResponse>>(openStack.EndPointUrl, notification.Name);

                var vm = listVM?.FirstOrDefault(f => f.Id == notification.InstanceId);
                if (vm != null && vm.Status == Contacts.Enums.VMStatus.Active)
                {
                    var vmEntity = await _dbContext.VMEntities.FirstOrDefaultAsync(f=>f.InstanceId == notification.InstanceId && !f.IsDeleted);
                    if (vmEntity != null)
                    {
                        vmEntity.Status = vm.Status;
                        vmEntity.Address = vm.Address;

                        //Tạm thời hostname là IP
                        vmEntity.HostName = $"http://{vm.Address}";

                        await _dbContext.SaveChangesAsync();

                        await _mediator.Publish( new VMActivedNotification { OpenStackEndPointUrl = openStack.EndPointUrl, VMInstanceId = vmEntity.InstanceId });

                        return true;
                    }
                    else
                    {
                        _appLogger.LogError($"VM not found (InstanceId={notification.InstanceId})");

                        return false;
                    }
                }
                else
                {
                    //Continuous retry
                    throw new Exception($"VM in process (InstanceId={notification.InstanceId})");
                }
            }
            else
            {
                _appLogger.LogError($"OpenStack not found (id={notification.OpenStackId})");
                return false;
            }
        }
    }
}
