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


    }
}
