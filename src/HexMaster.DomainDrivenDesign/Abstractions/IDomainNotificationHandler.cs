using System.Threading;
using System.Threading.Tasks;

namespace HexMaster.DomainDrivenDesign.Abstractions;

public interface IDomainNotificationHandler
{
    Task Handle(IDomainNotification domainEvent, CancellationToken cancellationToken = default);
}