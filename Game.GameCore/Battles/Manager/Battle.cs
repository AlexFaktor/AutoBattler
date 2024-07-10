using Game.GameCore.Battles.System;
using Game.GameCore.Units;
using Game.GameCore.Units.Actions;
using Game.GameCore.Units.Actions.Interfaces;

namespace Game.GameCore.Battles.Manager;

public class Battle
{
    public BattleConfiguration Configuration { get; }
    public BattleResult Result { get; }

    public Battle(BattleConfiguration battleConfiguration)
    {
        Configuration = battleConfiguration;

        AllUnits = battleConfiguration.Teams
                    .Select(team => team.Squad)
                    .SelectMany(squad => squad.Units)
                    .ToList();
        AllTeam = battleConfiguration.Teams;

        Result = CalculateBattle(battleConfiguration);
    }

    public ulong Timeline { get; set; }
    public List<Unit> AllUnits { get; }
    public List<Team> AllTeam { get; }
    public List<BattleAction> AllBattleActions { get; }

    private BattleResult CalculateBattle(BattleConfiguration configuration)
    {
        try
        {
            var result = new BattleResult(configuration);

            // Конфігурація перед ігровим циклом


            // Ігровий цикл
            while (true)
            {
                FindAndExecuteAction();
                FindWinner(result);

                if (result.Stats.Winner != Guid.Empty)
                    return result;

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    }

    private void FindAndExecuteAction()
    {
        if (Result != null)
            return;
        if (AllBattleActions.Count <= 0)
            throw new Exception("No BattkeAction");

        foreach (var battleAction in AllBattleActions)
        {
            if (battleAction is not IReloadable)
            {
                battleAction.Action();
                AllBattleActions.Remove(battleAction);
                return;
            }
        }

        var reloadableBattleActions = new List<RechargingAbility>();

        foreach (var battleAction in AllBattleActions)
        {
            reloadableBattleActions.Add((RechargingAbility)battleAction);
        }

        var reloadableBattleAction = reloadableBattleActions.OrderBy(a => a.Time.NextUse).First();
        reloadableBattleAction.Action();
        reloadableBattleAction.Time.Reload();
        Timeline = reloadableBattleAction.Time.NextUse;
    }

    private void FindWinner(BattleResult result)
    {
        var AliveTeams = new List<Team>();

        foreach (var team in AllTeam)
        {
            if (team.Squad.IsSquadAilve())
                AliveTeams.Add(team);
        }

        if (AliveTeams.Count == 0)
            throw new Exception("No alive teams");
        else if (AliveTeams.Count == 1)
            result.Stats.Winner = AliveTeams.First().Token;
    }
}
