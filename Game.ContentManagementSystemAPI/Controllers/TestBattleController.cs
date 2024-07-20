using Game.GameCore.Battles.Manager;
using Microsoft.AspNetCore.Mvc;

namespace Game.ContentManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestBattleController : ControllerBase
    {
        private readonly ILogger<TestBattleController> _logger;

        public TestBattleController(ILogger<TestBattleController> logger)
        {
            _logger = logger;
        }

        [HttpGet("custom")]
        public async Task<IActionResult> Get([FromQuery] string jsonBattleConfiguration)
        {
            var config = new BattleConfiguration();
            var result = ;

            return Ok(result);
        }
    }
}
