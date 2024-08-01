using ManagerVM.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.VM.Notifications
{
    public class VMActivedNotification : INotification
    {
        public string OpenStackEndPointUrl { get; set; }
        public string VMInstanceId { get; set; }
    }
}
