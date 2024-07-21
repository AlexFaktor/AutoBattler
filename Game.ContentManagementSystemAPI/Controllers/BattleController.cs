using Game.GameCore.Battles.Manager;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace Game.ContentManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleController : ControllerBase
    {
        public BattleController()
        {

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

            var battle = new Battle(config!);

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

            var battle = new Battle(config!);

            return Ok(await battle.CalculateBattle());
        }
    }
}
