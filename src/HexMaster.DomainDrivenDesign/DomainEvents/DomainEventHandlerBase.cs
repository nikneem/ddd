using System.Threading.Tasks;
using HexMaster.DomainDrivenDesign.Abstractions;

namespace HexMaster.DomainDrivenDesign.DomainEvents;

public abstract class DomainEventHandlerBase<TEvent> : IDomainEventHandler<TEvent> where TEvent : class, IDomainEvent
{
    public abstract Task Handle(TEvent domainEvent);

    public Task Handle(IDomainEvent @event)
    {
        return Handle(@event as TEvent);
    }
}