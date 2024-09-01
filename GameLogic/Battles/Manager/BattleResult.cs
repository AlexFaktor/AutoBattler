namespace GameLogic.Battles.Manager;

public class BattleResult
{
    public BattleConfiguration Configuration { get; }
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime EndTime { get; set; }

    public BattleStats Stats { get; set; } = new ();

    public BattleResult(BattleConfiguration configuration)
    {
        Configuration = configuration;
    }
}
