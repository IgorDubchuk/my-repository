using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using API.Contracts.ConsumedEvents;
using Domain.DomainEvents;
using Domain.DomainEvents.Consumed;
using static Domain.DomainEvents.Consumed.SeasonCalendarPublished;
using CommonLibrary;

namespace API.Controllers
{
    [ApiController]
    [Route("api/event")]
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventSourcingService _eventSourcingService;
        private readonly IEventSourcingSingletonService _eventSourcingSingletonService;
        public EventController(
            IEventSourcingService eventSourcingService,
            IEventSourcingSingletonService eventSourcingSingletonService,
            ILogger<EventController> logger
            )
        {
            _eventSourcingService = eventSourcingService;
            _eventSourcingSingletonService = eventSourcingSingletonService;
            _logger = logger;
        }

        [HttpPost]
        [Route("new-track")]
        public ActionResult<ConsumedEvent> PostNewTrackEvent([FromBody] NewTrackDto value)
        {
            return ApiRequestProcessor.Process<ConsumedEvent>(() =>
                {
                    var consumedEvent = new NewTrack(value.EventDateTime, value.Name);
                    return _eventSourcingService.SaveConsumedEvent(consumedEvent);
                }, _logger);
        }

        [HttpPost]
        [Route("season-calendar-published")]
        public ActionResult<ConsumedEvent> PostSeasonCalendarPublishedEvent([FromBody] SeasonCalendarPublishedDto value)
        {
            return ApiRequestProcessor.Process<ConsumedEvent>(() =>
                {
                    //У меня есть доменная сущность "потребляемое событие". Я их пишу в базу.
                    //В контроллере я избавляюсь от DTO, в сервис передаю событие в доменной модели
                    //Не знаю, насколько это правильный подход...
                    new SeasonCalendarPublishedDtoValidator().ValidateAndThrow(value);
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
                }, _logger);       
        }

        [HttpPost]
        [Route("process-events-command")]
        public IActionResult PostProcessEventsCommand([FromBody] ProcessEventCommandDto value)
        {

            return ApiRequestProcessor.ProcessApiRequest(
                _eventSourcingSingletonService.ProcessConsumedEvents
                ,
                _logger);            
        }
    }
}
