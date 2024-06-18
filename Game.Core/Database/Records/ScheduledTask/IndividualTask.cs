using Game.Core.Resources.Enums.ScheduledTask;

namespace Game.Core.Database.Records.ScheduledTask;

public class IndividualTask
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public EIndidualTask Type { get; set; }
    public uint FrequencyInSeconds { get; set; }
    public uint CallsNeeded { get; set; }
    public DateTime LastExecutionTime { get; set; }
}
