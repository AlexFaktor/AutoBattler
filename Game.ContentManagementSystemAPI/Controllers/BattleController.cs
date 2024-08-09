using App.GameCore.Battles.Manager;
using App.GameCore.Tools.ShellImporters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace App.ContentManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleController : ControllerBase
    {
        private IServiceProvider _serviceProvider;

        private DownloaderGameConfigService _downloader;

        public BattleController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _downloader = serviceProvider.GetRequiredService<DownloaderGameConfigService>();
        }

        [HttpPost("pvp")]
        public async Task<IActionResult> MakePvP([FromQuery] string jsonBattleConfiguration)
        {
            var config = JsonConvert.DeserializeObject<BattleConfiguration>(jsonBattleConfiguration);
            if (config == null)
            {
                Log.Error($"Cannot create config with this JSON:\n{jsonBattleConfiguration}\n");
                return NotFound();
            }

            var battle = new Battle(config!, _downloader.characterConfigs);

            return Ok(await battle.CalculateBattle());
        }
        
        [HttpPost("pve")]
        public async Task<IActionResult> MakePvE([FromQuery] string jsonBattleConfiguration)
        {
            var config = JsonConvert.DeserializeObject<BattleConfiguration>(jsonBattleConfiguration);
            if (config == null)
            {
                Log.Error($"Cannot create config with this JSON:\n{jsonBattleConfiguration}\n");
                return NotFound();
            }

            var battle = new Battle(config!, _downloader.characterConfigs);

            return Ok(await battle.CalculateBattle());
        }
    }
}
