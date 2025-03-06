using HexMaster.DomainDrivenDesign.ChangeTracking;
using HexMaster.DomainDrivenDesign.Notifications;
using System.Collections.Generic;

namespace HexMaster.DomainDrivenDesign.Abstractions;

public interface IDomainModel<out TId>
{
    TId Id { get; }
    TrackingState TrackingState { get; } 
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    ValidationNotification ValidationNotification { get; }

    void AddDomainEvent(IDomainEvent domainEvent);
    void RemoveDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
    void AddValidationError(string message);
}
