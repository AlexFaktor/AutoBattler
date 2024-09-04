using GameLogic.Battles.Enums;
using GameLogic.Units;
using Serilog;

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

        string fileName = $"{battleTime:yyyy-MM-dd_HH-mm-ss}_{EBattleTypeParser.Parse(battle.Configuration.BattleType)}_battle-log.html";
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
            LogCustom("battle", "start");
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
            LogInfo("unit", $" {unit.Name} >> S {unit.Shield.Now:F1}/{unit.Shield.Max:F1}| H {unit.HealthPoints.Now:F1}/{unit.HealthPoints.Max:F1} ");
        }
    }
}
