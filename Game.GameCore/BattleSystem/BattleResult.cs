namespace Game.Manager.BattleSystem;

public class BattleResult
{
    public BattleConfiguration Configuration { get; }
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime Time { get; }

    public BattleStats Stats { get; }
    public BattleLogs Logs { get; }

    public BattleResult()
    {
    }
}
