using Microsoft.AspNetCore.Mvc;
using WebAPITest.Application.Interfaces;
using WebAPITest.Contracts.ConsumedEvents;
using WebAPITest.Domain.Models.DomainEvents;
using WebAPITest.Domain.Models.DomainEvents.Consumed;
using WebAPITest.Infrastructure.Persistence.Repositories.Interfaces;
using static WebAPITest.Domain.Models.DomainEvents.Consumed.SeasonCalendarPublished;

namespace WebAPITest.API.Controllers
{
    [ApiController]
    [Route("api/event")]
    public class EventController : Controller
    {
        public EventController(
            IEventSourcingService eventSourcingService,
            IEventSourcingSingletonService eventSourcingSingletonService
            )
        {
            _eventSourcingService = eventSourcingService;
            _eventSourcingSingletonService = eventSourcingSingletonService;
        }

        private readonly IEventSourcingService _eventSourcingService;
        private readonly IEventSourcingSingletonService _eventSourcingSingletonService;



        [HttpPost]
        [Route("season-calendar-published")]
        public ConsumedEvent Post([FromBody] SeasonCalendarPublishedDto value)
        {
            List<SeasonCalendarRace> races = new();
            foreach (SeasonCalendarRaceDto dtoRace in value.Races)
            {
                var race = new SeasonCalendarRace(
                    dtoRace.TrackName,
                    dtoRace.NumberInSeason,
                    dtoRace.Name,
                    dtoRace.Date);
                races.Add(race);
            }

            var consumedEvent = new SeasonCalendarPublished(value.EventDateTime, value.Year, races);

            return _eventSourcingService.SaveConsumedEvent(consumedEvent);
        }



        [HttpPost]
        [Route("new-track")]
        public ConsumedEvent Post([FromBody] NewTrackDto value)
        {
            var consumedEvent = new NewTrack(value.EventDateTime, value.Name);
            return _eventSourcingService.SaveConsumedEvent(consumedEvent);
        }



        [HttpPost]
        [Route("process-event")]
        public void Post([FromBody] ProcessEventCommandDto value)
        {
            _eventSourcingSingletonService.ProcessConsumedEvents();
        }
    }
}
