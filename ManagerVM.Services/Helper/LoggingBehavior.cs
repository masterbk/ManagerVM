using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Helper
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IAppLogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(IAppLogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;
            string responseName = typeof(TResponse).Name;
            _logger.LogInformation("[{@Prefix}] Handle request={@RequestName} and response={@responseName} with requestData={@RequestData}", "LoggingBehavior", requestName, responseName, request.ToJson());
            Stopwatch timer = new Stopwatch();
            timer.Start();
            TResponse val = await next();
            timer.Stop();
            TimeSpan elapsed = timer.Elapsed;
            if (elapsed.Seconds > 3)
            {
                _logger.LogWarning("[{@PerfPossible}] The request {@XRequestData} took {@TimeTaken} seconds.", "LoggingBehavior", typeof(TRequest).Name, elapsed.Seconds);
            }

            _logger?.LogInformation("[{@Prefix}] Handled request={@RequestName} and  response={@ResponseName}, responsedata={@ResponseData}", "LoggingBehavior", requestName, responseName, val.ToJson());
            return val;
        }
    }
}
