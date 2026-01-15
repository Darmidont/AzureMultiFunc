using System.Diagnostics;
using BusinessLogic.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureMultiFunc;

public class MyTimeTrigger
{
    private readonly ILogger _logger;
    private readonly IProductProcessor _productProcessor;

    public MyTimeTrigger(ILoggerFactory loggerFactory, IProductProcessor productProcessor)
    {
        _productProcessor = productProcessor;
        _logger = loggerFactory.CreateLogger<MyTimeTrigger>();
    }

    [Function("MyTimeTrigger")]
    public async Task Run(
        //[TimerTrigger("0 */1 * * * *")]
        [TimerTrigger("0 0 0 30 2 *")]
        TimerInfo myTimer
    )
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);
        Stopwatch sw = Stopwatch.StartNew();

        var data = await _productProcessor.ProcessProductsAsync();
        sw.Stop();
        if (myTimer.ScheduleStatus is not null)
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
    }
}