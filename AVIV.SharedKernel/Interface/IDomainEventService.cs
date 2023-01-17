namespace AVIV.SharedKernel.Interface
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
