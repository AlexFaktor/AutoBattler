namespace App.Core.DatabaseRecords.ScheduledTask;

public abstract class TimeTask
{
    public int FrequencyInSeconds { get; set; }
    public DateTime LastExecutionTime { get; set; }
}
