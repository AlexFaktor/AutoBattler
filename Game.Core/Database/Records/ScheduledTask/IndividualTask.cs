using Game.Core.Resources.Enums.ScheduledTask;

namespace Game.Core.Database.Records.ScheduledTask;

public class IndividualTask
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public EIndidualTask Type { get; set; }
    public int FrequencyInSeconds { get; set; }
    public int CallsNeeded { get; set; }
    public DateTime LastExecutionTime { get; set; }
}
