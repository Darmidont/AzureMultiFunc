using BusinessLogic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;

namespace AzureMultiFunc;

public class MyTimeTrigger
{
    private readonly ILogger _logger;
    private readonly IDataService _dataService;

    public MyTimeTrigger(ILoggerFactory loggerFactory, IDataService dataService)
    {
        _dataService = dataService;
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
        var data = _dataService.GetProducts();

        _logger.LogInformation($"Retrieved {data.Count} products from the data service." );
        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }
    }
}