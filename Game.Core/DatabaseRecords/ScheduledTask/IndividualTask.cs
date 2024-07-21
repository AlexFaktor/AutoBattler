using App.Core.Resources.Enums.ScheduledTask;

namespace App.Core.DatabaseRecords.ScheduledTask;

public class IndividualTask : TimeTask
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public EIndidualTask Type { get; set; }
    public int CallsNeeded { get; set; }
}
