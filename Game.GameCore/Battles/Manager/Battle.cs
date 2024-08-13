using App.GameCore.Battles.System;
using App.GameCore.Tools.ShellImporters.ConfigReaders;
using App.GameCore.Units;
using App.GameCore.Units.Actions.Abilitys;
using App.GameCore.Units.Actions.Abilitys.Interfaces;
using System.Diagnostics;

namespace App.GameCore.Battles.Manager;

public class Battle
{
    // Tools
    private UnitFactory _unitFactory;
    public BattleLogger Logger { get; private set; }

    // Main
    public BattleConfiguration Configuration { get; }
    public BattleResult BattleResult { get; protected set; }
    public Random Random { get; set; }

    // Battle 
    public ulong Timeline { get; set; }
    public List<Team> AllTeam { get; protected set; } = [];
    public List<Unit> AllUnits { get; protected set; } = [];
    public List<IBattleAction> AllBattleActions { get; protected set; } = [];
    

    public Battle(BattleConfiguration battleConfiguration, CharacterConfigReader characteConfigReader)
    {
        _unitFactory = new(characteConfigReader);
        
        Configuration = battleConfiguration;
        
        BattleResult = new(battleConfiguration);
        Random = new Random(battleConfiguration.Seed);

        InitializeConfiguration();

        Logger = new(DateTime.Now, this);

        void InitializeConfiguration()
        {
            Timeline = 0;
            var allTeams = new List<Team>();
            foreach (var teamConfig in battleConfiguration.TeamConfigurations) {
                allTeams.Add(new(teamConfig, this, _unitFactory));
            }
            AllTeam = allTeams;
            AllUnits = allTeams.SelectMany(t => t.Units).ToList();
            AllBattleActions = AllUnits
            .SelectMany(u => u.Actions)
            .OfType<IBattleAction>()
            .ToList();
        }
    }

    public BattleResult CalculateBattle()
    {
        var stopwatch = new Stopwatch();

        try
        {
            StartBattle();

            InitializeTeams();
            InitializeBattle();

            // Ігровий цикл
            while (BattleResult.Stats.TeamWinner == Guid.Empty)
            {
                FindAndExecuteAction();
            }

            EndBattle();
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
        }
        return BattleResult;

        void InitializeTeams() // NEED MAKE BE COMPLEX
        {
            PlaceUnit();

            void PlaceUnit()
            {
                float startPostition = -100f;
                float stepBetweenTeam = 200f;

                foreach (var team in AllTeam)
                {
                    foreach (var unit in team.Units)
                    {
                        unit.TeleportTo(startPostition);
                    }
                    startPostition += stepBetweenTeam;
                }
            }
        }
        void InitializeBattle()
        {
            foreach (var unit in AllUnits)
            {
                unit.OnDead += Unit_OnDead;
            }

            void Unit_OnDead(object? sender, DeadEventArgs e)
            {
                if (sender is Unit)
                {
                    FindWinner();
                }
            }
        }

        void StartBattle()
        {
            stopwatch.Start();
        }
        void EndBattle()
        {
            stopwatch.Stop();
            BattleResult.Stats.ActualDuration = stopwatch.ElapsedMilliseconds;
            BattleResult.EndTime = DateTime.UtcNow;
        }
    }

    private void FindAndExecuteAction()
    {
        if (BattleResult.Stats.TeamWinner != Guid.Empty)
            return;
        if (AllBattleActions.Count <= 0)
            throw new Exception("No BattleAction");

        foreach (var battleAction in AllBattleActions)
        {
            if (battleAction is not IPeriodicAbility)
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

        Timeline = reloadableBattleAction.Time.NextUse;
        reloadableBattleAction.Action();
    }

    private void FindWinner()
    {
        var AliveTeams = new List<Team>();

        foreach (var team in AllTeam)
        {
            if (team.IsTeamAilve())
                AliveTeams.Add(team);
            if (AliveTeams.Count > 1)
                return;
        }

        if (AliveTeams.Count == 0)
            throw new Exception("No alive teams");

        else if (AliveTeams.Count == 1)
        {
            BattleResult.Stats.TeamWinner = AliveTeams.First().Token;
            var team = AllTeam.First(t => t.Token == BattleResult.Stats.TeamWinner);
            Logger.LogCustom("battle-win", $" \nID: {BattleResult.Stats.TeamWinner}\nNAME: {team.Player.Username}\n ");
            return;
        }
        Logger.LogCustom("battle-info", $" No team to win ");
    }
}
