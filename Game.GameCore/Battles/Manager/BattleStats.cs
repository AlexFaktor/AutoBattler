using App.GameCore.Units;

namespace App.GameCore.Battles.Manager;

public class BattleStats
{
    public Guid TeamWinner { get; set; }

    public long TotalTime { get; set; }
    public long ActualDuration { get; set; }

    public List<UnitStatistics> UnitStatistics { get; set; } = [];
}
