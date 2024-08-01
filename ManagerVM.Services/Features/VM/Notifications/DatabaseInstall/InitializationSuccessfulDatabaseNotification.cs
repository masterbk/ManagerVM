using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.VM.Notifications.DatabaseInstall
{
    public class InitializationSuccessfulDatabaseNotification: INotification
    {
        public string VMInstanceId {  get; set; }
    }
}
