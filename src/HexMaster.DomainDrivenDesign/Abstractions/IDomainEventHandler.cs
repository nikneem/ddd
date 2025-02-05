using System.Threading;
using System.Threading.Tasks;

namespace HexMaster.DomainDrivenDesign.Abstractions;

public interface IDomainEventHandler<in TDomainEvent> : IDomainEventHandler where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}

public interface IDomainEventHandler
{
    Task Handle(IDomainEvent @event);
}