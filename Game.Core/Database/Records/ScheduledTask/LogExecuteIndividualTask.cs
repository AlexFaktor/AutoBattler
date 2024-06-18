namespace Game.Core.Database.Records.ScheduledTask;

public class LogExecuteIndividualTask
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime LastExecutionTime { get; set; }
}
