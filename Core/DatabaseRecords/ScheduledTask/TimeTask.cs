namespace Core.DatabaseRecords.ScheduledTask;

public abstract class AbstractTask
{
    public int FrequencyInSeconds { get; set; }
    public DateTime LastExecutionTime { get; set; }
}
