using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Helper
{
    public interface IAppLogger<T>
    {
        void LogInformation(string message, params object[] args);

        void LogWarning(string message, params object[] args);

        void LogError(string message, params object[] args);

        void LogError(Exception ex, string message, params object[] args);

        void LogCritical(Exception ex, string message, params object[] args);
    }


}
