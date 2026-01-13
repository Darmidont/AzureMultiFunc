using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
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

        string connStr = Environment.GetEnvironmentVariable("SqlConnectionString");
        if (!string.IsNullOrEmpty(connStr))
        {
            _logger.LogInformation($"Found connection: {connStr}");
            using SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 name FROM sys.tables", conn);
            var result = cmd.ExecuteScalar();
            _logger.LogInformation($"First table: {result}");
        }

        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }
    }
}