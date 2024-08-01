using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Helper
{
    public class HangFireExpirationTimeAttribute : JobFilterAttribute, IApplyStateFilter
    {
        private readonly IConfiguration _configuration;

        public HangFireExpirationTimeAttribute(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            context.JobExpirationTimeout = TimeSpan.FromHours(_configuration.GetValue<int?>("HangFireConfig:JobExpireTime").GetValueOrDefault(3));
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
        }
    }
}
