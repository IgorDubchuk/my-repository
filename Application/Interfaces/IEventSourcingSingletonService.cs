namespace Application.Interfaces
{
    public interface IEventSourcingSingletonService
    {
        void ProcessConsumedEvents();
    }
}