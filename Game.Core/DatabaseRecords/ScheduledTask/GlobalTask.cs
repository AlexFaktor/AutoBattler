using Game.Core.Resources.Enums.ScheduledTask;

namespace Game.Core.DatabaseRecords.ScheduledTask;

public class GlobalTask : TimeTask
{
    public Guid Id { get; set; }
    public EGlobalTask Type { get; set; }
    public int CallsNeeded { get; set; }
    
}
