using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.VM.Notifications
{
    public class VMCreatedNotification : INotification
    {
        public string InstanceId { get; set; }
        public string Name { get; set; }
        public long OpenStackId { get; set; }
    }
}
