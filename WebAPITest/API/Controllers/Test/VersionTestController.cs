using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Test
{
    //[ApiController]
    [ApiVersion("2.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class VersionTestController// : ControllerBase
    {
        private readonly ILogger<VersionTestController> _logger;

        public VersionTestController(ILogger<VersionTestController> logger)
        {
            _logger = logger;
        }

        [MapToApiVersion("2.0")]
        [HttpGet(Name = "GetRandomInt")]
        public int Get()
        {
            return Random.Shared.Next(int.MinValue, int.MaxValue);
        }
    }
}