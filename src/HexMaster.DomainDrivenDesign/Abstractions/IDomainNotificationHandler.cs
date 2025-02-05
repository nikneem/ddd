using System.Threading;
using System.Threading.Tasks;

namespace HexMaster.DomainDrivenDesign.Abstractions;

public interface IDomainNotificationHandler<in TDomainEvent> where TDomainEvent : IDomainNotification
{
    Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}
