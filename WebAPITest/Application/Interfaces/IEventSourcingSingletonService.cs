namespace WebAPITest.Application.Interfaces
{
    public interface IEventSourcingSingletonService
    {
        void ProcessConsumedEvents();
    }
}