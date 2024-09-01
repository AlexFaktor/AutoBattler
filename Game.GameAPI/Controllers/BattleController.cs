using GameLogic.Battles.Manager;
using GameLogic.Tools.ShellImporters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace ContentManagementSystemAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BattleController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    private readonly DownloaderGameConfigService _downloader;

    public BattleController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _downloader = serviceProvider.GetRequiredService<DownloaderGameConfigService>();
    }

    [HttpPost("pvp")]
    public IActionResult MakePvP([FromQuery] string jsonBattleConfiguration)
    {
        var config = JsonConvert.DeserializeObject<BattleConfiguration>(jsonBattleConfiguration);
        if (config == null)
        {
            Log.Error($"Cannot create config with this JSON:\n{jsonBattleConfiguration}\n");
            return NotFound();
        }

        var battle = new Battle(config!, _downloader.characterConfigs);

        return Ok(battle.CalculateBattle());
    }
    
    [HttpPost("pve")]
    public IActionResult MakePvE([FromQuery] string jsonBattleConfiguration)
    {
        var config = JsonConvert.DeserializeObject<BattleConfiguration>(jsonBattleConfiguration);
        if (config == null)
        {
            Log.Error($"Cannot create config with this JSON:\n{jsonBattleConfiguration}\n");
            return NotFound();
        }

        var battle = new Battle(config!, _downloader.characterConfigs);

        return Ok(battle.CalculateBattle());
    }

    [HttpPost("admin/custom")]
    public IActionResult Get([FromQuery] string jsonBattleConfiguration)
    {
        var config = JsonConvert.DeserializeObject<BattleConfiguration>(jsonBattleConfiguration);
        if (config == null)
        {
            Log.Error($"Cannot create config with this JSON:\n{jsonBattleConfiguration}\n");
            return NotFound();
        }
        Log.Information("Custom battle started.");
        var battleResult = new Battle(config!, _downloader.characterConfigs).CalculateBattle();
        Log.Information($"Custom battle successfully completed\nDuration: {battleResult.Stats.ActualDuration}\nID: {battleResult.Id}");

        return Ok(battleResult);
    }
}
