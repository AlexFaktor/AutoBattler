using Game.GameCore.BattleSystem;
using Game.GameCore.Units;

namespace Game.Manager.BattleSystem;

public class BattleManager
{
    public BattleConfiguration BattleConfiguration { get; }
    public BattleResult BattleResult { get; }

    public BattleManager (BattleConfiguration battleConfiguration)
    {
        BattleConfiguration = battleConfiguration;
        BattleResult = Result();

        AllUnits = battleConfiguration.Teams
                    .Select(team => team.Squad)
                    .SelectMany(squad => squad.Units)
                    .ToList();
        AllTeam = battleConfiguration.Teams;
    }

    public ulong Timeline { get; }
    public List<Unit> AllUnits { get; }
    public List<Team> AllTeam { get; }

    private BattleResult Result()
    {
        // Конфігурація перед ігровим циклом


        // Ігровий цикл
        while (true)
        {

        }
    }
}
