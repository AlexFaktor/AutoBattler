using Game.GameCore.Battles.Manager;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace Game.ContentManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("admin/[controller]")]
    public class TestBattleController : ControllerBase
    {

        public TestBattleController()
        {
            
        }

        [HttpPost("custom")]
        public async Task<IActionResult> Get([FromQuery] string jsonBattleConfiguration)
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
