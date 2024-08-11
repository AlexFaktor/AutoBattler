using App.GameCore.Battles.Enums;
using App.GameCore.Units;
using Serilog;
using Serilog.Core;

namespace App.GameCore.Battles.Manager;

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
            foreach (var unit in battle.AllUnits)
            {
                unit.OnDead += Unit_OnDead;
            }
        }
    }

    public void LogInfo(string message)
    {
        _logger.Information("<info>{Message}</info>", message);
    }
    public void LogInfo(string tag, string message)
    {
        _logger.Information($"<info-{tag}>{{Message}}</info-{tag}>", message);
    }
    public void LogEvent(string message)
    {
        _logger.Information("<event>{Message}</event>", message);
    }
    public void LogEvent(string tag, string message)
    {
        _logger.Information($"<event-{tag}>{{Message}}</event-{tag}>", message);
    }
    public void LogAction(string message)
    {
        _logger.Information("<action>{Message}</action>", message);
    }
    public void LogAction(string tag, string message)
    {
        _logger.Information($"<action-{tag}>{{Message}}</action-{tag}>", message);
    }
    public void LogError(string message)
    {
        _logger.Error("<error>{Message}</error>", message);
    }
    public void LogError(string tag, string message)
    {
        _logger.Information($"<error-{tag}>{{Message}}</error-{tag}>", message);
    }
    public void LogCustom(string tag, string message)
    {
        _logger.Information($"<{tag}>{{Message}}</{tag}>", message);
    }
    public void LogCustom(string tag, string subTag, string message)
    {
        _logger.Information($"<{tag}-{subTag}>{{Message}}</{tag}-{subTag}>", message);
    }

    private void Unit_OnDead(object? sender, DeadEventArgs e)
    {
        if (sender is Unit unit)
        {
            LogEvent("dead", $" {unit.Name} killed by {e.Killer.Name} ");
        }
    }

}
