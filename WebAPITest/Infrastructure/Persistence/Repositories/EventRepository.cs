using Microsoft.EntityFrameworkCore;
using WebAPITest.Domain.Models.DomainEvents;
using WebAPITest.Domain;
using WebAPITest.Infrastructure.Persistence.DbModel;
using WebAPITest.Infrastructure.Persistence.Repositories.Interfaces;

namespace WebAPITest.Infrastructure.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private F1DbContext _dbContext;

        public EventRepository(F1DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveEventAndSetNewerEventsToRepeatProcess(ConsumedEvent consumedEvent)
        {
            var consumedEventForDb = ConsumedEventDbModel.CreateFromDomainEntity(consumedEvent);

            _dbContext.ConsumedEvent.Add(consumedEventForDb);

            //Есть ли возможность изящнее сделать так, чтобы не нужно было справочниые значения доставать из базы,
            //и не ставить для них явно unchanged?
            _dbContext.Entry(consumedEventForDb.State).State = EntityState.Unchanged;
            _dbContext.Entry(consumedEventForDb.Type).State = EntityState.Unchanged;

            //Есть какая-то возможность одним запросом обновить по условию, а не достать, обновить, сохранить?
            //Мне они в памяти не нужны, да и хочется одной транзакцией это сделать без создания транзакци
            //Вот так не работает, выдается ошибка во время выполнения The following 'SetProperty' failed to translate..
            //var newState = ConsumedEventStates.GetByCode(ConsumedEventStatesEnum.ToRepeatProcess);
            //_dbContext.ConsumedEvent
            //    .Where(e => e.RecieveDateTime >= consumedEvent.RecieveDateTime)
            //    .ExecuteUpdate(s => s.SetProperty(e => e.State, s => newState));

            var eventsToRepeatProcess = _dbContext.ConsumedEvent
                .Where(e => e.EventDateTime >= consumedEvent.EventDateTime)
                .ToList();

            foreach (var eventToRepeatProcess in eventsToRepeatProcess)
            {
                eventToRepeatProcess.State = ConsumedEventStates.Values.Single(s => s.Code == "ToRepeatProcess");
                _dbContext.Entry(eventToRepeatProcess.State).State = EntityState.Unchanged;
            }

            _dbContext.SaveChanges();
        }

        public ConsumedEvent TakeOldestEventToProcessAndSetProcessingState()
        {
            var consumedEventsForDb = _dbContext.ConsumedEvent
                .Include(e => e.Type)
                .Include(e => e.State)
                .Where(e => DomainConstants.toProcessConsumedEventStates.Contains(e.State))
                .OrderBy(e => e.EventDateTime);

            ConsumedEventDbModel consumedEventForDb;
            try { consumedEventForDb = consumedEventsForDb.First(); }
            catch (InvalidOperationException) { throw new ApplicationException("Can not take oldest event to process: there is no event to process in repository"); }

            consumedEventForDb.State = ConsumedEventStates.Values.Single(v => v.Code == ConsumedEventStatesEnum.Processing.ToString());
            _dbContext.Entry(consumedEventForDb.State).State = EntityState.Unchanged;
            _dbContext.SaveChanges();
            return consumedEventForDb.GetDomainEntity();
        }

        public void UpdateEvent(ConsumedEvent consumedEvent)
        {
            var consumedEventFromDb = _dbContext.ConsumedEvent.Single(e => e.Id == consumedEvent.Id);

            consumedEventFromDb.State = consumedEvent.State;
            consumedEventFromDb.ProcessedDateTime = consumedEvent.ProcessedDateTime;

            //Есть ли возможность изящнее сделать так, чтобы не нужно было справочниые значения доставать из базы,
            //и не ставить для них явно unchanged?
            _dbContext.Entry(consumedEventFromDb.State).State = EntityState.Unchanged;

            _dbContext.SaveChanges();
        }
    }
}