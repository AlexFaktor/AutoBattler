using Core.Resources.Enums.ScheduledTask;

namespace Core.DatabaseRecords.ScheduledTask;

public class GlobalTask : AbstractTask
{
    public Guid Id { get; set; }
    public GlobalTasks Type { get; set; }
    public int CallsNeeded { get; set; }
    
}
