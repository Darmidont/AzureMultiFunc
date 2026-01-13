using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureMultiFunc;

public class MyTimeTrigger
{
    private readonly ILogger _logger;

    public MyTimeTrigger(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<MyTimeTrigger>();
    }

    [Function("MyTimeTrigger")]
    public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);
        
        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }
    }
}