using App.Core.Resources.Enums.ScheduledTask;

namespace App.Core.DatabaseRecords.ScheduledTask;

public class IndividualTask : AbstractTask
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public IndividualTasks Type { get; set; }
    public int CallsNeeded { get; set; }
}
