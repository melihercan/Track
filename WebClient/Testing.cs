using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient
{
    public class Testing
    {

        private readonly ILogger<Testing> _logger;

        public Testing(ILogger<Testing> logger)
        {
            _logger = logger;


        }

        public void Run()
        {
            _logger.LogInformation("=============================Fenerbahce");
            _logger.LogTrace("XTrace: Forecast");
            _logger.LogDebug("XDebug: Forecast");
            _logger.LogWarning("XWarning: Forecast");
            _logger.LogError("XError: Forecast");
            _logger.LogCritical("XCritical: Forecast");
            _logger.LogInformation("XLogInformation: Forecast");
        }
    }
}
