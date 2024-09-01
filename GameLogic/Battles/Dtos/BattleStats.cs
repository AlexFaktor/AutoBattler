using GameLogic.Units;

namespace GameLogic.Battles.Dtos;

public class BattleStats
{
    public Guid TeamWinner { get; set; }

    public ulong TotalTimeline { get; set; }
    public long ActualDuration { get; set; }

    public List<UnitStatistics> UnitStatistics { get; set; } = [];
}
