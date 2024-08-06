using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Enums
{
    public enum HostStatus
    {
        InstallingDocker = 1,
        InstallDockerError = 2,
        InstalledDocker = 3,
        InstallingLMS = 4,
        InstallLMSError = 5,
        InstalledLMS = 6,
        CreatingDatabase = 7,
        CreateDatabaseError = 8,
        CreatedDatabase = 9,
        SynchronizingData = 10,
        SyncDataError = 11,
        SyncDataSuccess = 12,
        Available = 13
    }
}
