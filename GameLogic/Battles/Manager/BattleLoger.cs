using GameLogic.Battles.Enums;
using GameLogic.BattleSystem.Enums;
using GameLogic.Units;
using Serilog;
using System.Text;

namespace GameLogic.Battles.Manager;

public class BattleLogger
{
    private readonly ILogger _logger;
    private readonly string _logDirectory;
    private readonly Battle _battle;

    public BattleLogger(DateTime battleTime, Battle battle)
    {
        _battle = battle;
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        _logDirectory = Path.Combine(documentsPath, "Camp of Strangers", "Logs");
        Directory.CreateDirectory(_logDirectory);

        string fileName = $"{battleTime:yyyy-MM-dd_HH-mm-ss}_{BattleTypesParser.Parse(battle.Configuration.BattleType)}_battle-log.html";
        string filePath = Path.Combine(_logDirectory, fileName);

        _logger = new LoggerConfiguration()
           .WriteTo.File(filePath, outputTemplate: "{Message}{NewLine}{Exception}")
        .CreateLogger();

        InitializeLogger();

        void InitializeLogger()
        {
            battle.OnBattleStart += Battle_OnBattleStart;
            battle.OnBattleEnd += Battle_OnBattleEnd;
            battle.OnBattleDayPassed += Battle_OnBattleDayPassed;

            foreach (var unit in battle.AllUnits)
            {
                unit.OnDead += Unit_OnDead;
                unit.OnDamageReceived += Unit_OnDamageReceived;
                unit.OnMove += Unit_OnMove;
                unit.OnHealthRecovery += Unit_OnHealthRecovery;
            }
        }
    }

    public void LogInfo(string message)
    {
        _logger.Information($"{_battle.Timeline} | <info>{{Message}}</info>", message);
    }
    public void LogInfo(string tag, string message)
    {
        _logger.Information($"{_battle.Timeline} | <info-{tag}>{{Message}}</info-{tag}>", message);
    }
    public void LogEvent(string message)
    {
        _logger.Information($"{_battle.Timeline} | <event>{{Message}}</event>", message);
    }
    public void LogEvent(string tag, string message)
    {
        _logger.Information($"{_battle.Timeline} | <event-{tag}>{{Message}}</event-{tag}>", message);
    }
    public void LogAction(string message)
    {
        _logger.Information($"{_battle.Timeline} | <action>{{Message}}</action>", message);
    }
    public void LogAction(string tag, string message)
    {
        _logger.Information($"{_battle.Timeline} | <action-{tag}>{{Message}}</action-{tag}>", message);
    }
    public void LogError(string message)
    {
        _logger.Error($"{_battle.Timeline} | <error>{{Message}}</error>", message);
    }
    public void LogError(string tag, string message)
    {
        _logger.Information($"{_battle.Timeline} | <error-{tag}>{{Message}}</error-{tag}>", message);
    }
    public void LogCustom(string tag, string message)
    {
        _logger.Information($"{_battle.Timeline} | <{tag}>{{Message}}</{tag}>", message);
    }
    public void LogCustom(string tag, string subTag, string message)
    {
        _logger.Information($"{_battle.Timeline} | <{tag}-{subTag}>{{Message}}</{tag}-{subTag}>", message);
    }

    private void Battle_OnBattleStart(object? sender, EventArgs e)
    {
        // Show configuration

        if (sender is Battle battle)
        {
            LogCustom($"battle", "start", "\n" +
                $"STARTING CONFIGURATION\n" +
                $"TYPE : {BattleTypesParser.Parse(battle.Configuration.BattleType)}\n" +
                $"DAY TIME : {battle.Configuration.DayTime}\n" +
                $"TEMPETURA : {battle.Configuration.Tempetura}\n" +
                $"TERRAIN : {battle.Configuration.Terrain}\n" +
                $"WEATHER : {battle.Configuration.Weather}\n" +
                $"{TeamInfo()}\n");

            string TeamInfo()
            {
                var text = new StringBuilder();

                text.Append("TEAMS\n");

                foreach (var team in battle.AllTeam)
                {
                    text.Append($" TOKEN - {team.Token}\n");
                    foreach (var unit in team.Units)
                    {
                        text.Append($"  {unit.Name} > HEALPOINTS {unit.HealthPoints.Now}, ARMOR {unit.Armor.Now}, DAMAGE {unit.Damage.Now} \n");
                    }
                }

                return text.ToString();
            }
        }
    }

    private void Battle_OnBattleEnd(object? sender, EventArgs e)
    {
        // Show result

        if (sender is Battle battle)
        {
            LogCustom("battle", "end");
        }
    }

    private void Battle_OnBattleDayPassed(object? sender, EventArgs e)
    {
        if (sender is Battle battle)
        {
            LogCustom("battle", "daypassed");
        }
    }

    private void Unit_OnDamageReceived(object? sender, DamageReceivedEventArgs e)
    {
        if (sender is Unit unit)
        {
            LogAction("info", $" {unit.Name} will receive {e.DamageReceived:F1} damage from {e.Attacker.Name} ");
            Unit_LogResource(sender);
        }
    }
    private void Unit_OnHealthRecovery(object? sender, HealthEventArgs e)
    {
        LogAction("healt", $" {e.WhoGetHeal.Name} get {e.HealValue} healt from {e.Source} | Healt now: {e.WhoGetHeal.HealthPoints.Now}");
    }

    private void Unit_OnDead(object? sender, DeadEventArgs e)
    {
        if (sender is Unit unit)
        {
            LogEvent("dead", $" {unit.Name} killed by {e.Killer.Name} ");
        }
    }

    private void Unit_OnMove(object? sender, MoveEventArgs e)
    {
        if (sender is Unit unit)
        {
            LogAction("unit-move", $" {unit.Name} moved from {e.StartPositon} to {e.EndPositon} with speed {e.Speed}");
        }
    }

    private void Unit_LogResource(object? sender)
    {
        if (sender is Unit unit)
        {
            var shieldNow = unit.Shields.FirstOrDefault()?.Now ?? 0;
            var shieldMax = unit.Shields.FirstOrDefault()?.Max ?? 0;
            LogInfo("unit", $" {unit.Name} >> S {shieldNow:F1}/{shieldMax:F1}| H {unit.HealthPoints.Now:F1}/{unit.HealthPoints.Max:F1} ");
        }
    }
}
