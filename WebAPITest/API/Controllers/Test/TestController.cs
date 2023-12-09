using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using API.Contracts.ConsumedEvents;
using Domain.DomainEvents;
using Domain.DomainEvents.Consumed;
using static Domain.DomainEvents.Consumed.SeasonCalendarPublished;
using CommonLibrary;
using Application.Services;

namespace API.Controllers.Test
{
    [ApiController]
    [Route("api/test")]
    public class TestController : Controller
    {
        public TestController(
            TestService testService,
            ILogger<TestController> logger
            )
        {
            _testService = testService;
            _logger = logger;
        }

        private readonly ILogger<TestController> _logger;
        private readonly TestService _testService;


        [HttpPost]
        [Route("bench-command")]
        public IActionResult BenchCommand()
        {

            return ApiRequestProcessor.ProcessApiRequest(
                _testService.Bench,
                _logger);
        }

        [HttpPost]
        [Route("test-command")]
        public IActionResult TestCommand()
        {

            return ApiRequestProcessor.ProcessApiRequest(
                _testService.Test,
                _logger);
        }
    }
}
