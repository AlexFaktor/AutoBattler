namespace Game.GameCore.Battles.Manager;

public class BattleResult
{
    public BattleConfiguration Configuration { get; }
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime Time { get; }

    public BattleStats Stats { get; } = new ();
    public BattleLogs Logs { get; } = new();

    public BattleResult(BattleConfiguration configuration)
    {
        Configuration = configuration;
    }
}
