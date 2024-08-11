using App.GameCore.Units;

namespace App.GameCore.Battles.Manager;

public class BattleStats
{
    public Guid TeamWinner { get; set; }

    public long TotalTime { get; set; }
    public int ActualDuration { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public List<UnitStatistics> UnitStatistics { get; set; } = [];
}
