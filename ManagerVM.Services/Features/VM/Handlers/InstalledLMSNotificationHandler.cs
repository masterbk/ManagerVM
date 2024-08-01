using ManagerVM.Services.Features.VM.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.VM.Handlers
{
    public class InstalledLMSNotificationHandler : INotificationHandler<InstalledLMSNotification>
    {
        public Task Handle(InstalledLMSNotification notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
