using GameLogic.Battles.Manager;

namespace GameLogic.Battles.Dtos;

public class BattleResult
{
    public BattleConfiguration Configuration { get; }
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime EndTime { get; set; }

    public BattleStats Stats { get; set; }

    public BattleResult(BattleConfiguration configuration, Battle battle)
    {
        Configuration = configuration;
        Stats = new(battle);
    }
}
