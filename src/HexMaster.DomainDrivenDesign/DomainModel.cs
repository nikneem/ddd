using System.Collections.Generic;
using System.Linq;
using HexMaster.DomainDrivenDesign.Abstractions;
using HexMaster.DomainDrivenDesign.ChangeTracking;
using HexMaster.DomainDrivenDesign.Exceptions;

namespace HexMaster.DomainDrivenDesign;

public abstract class DomainModel<TId> : IDomainModel<TId>
{

    private List<IDomainEvent> _domainEvents;
    public TId Id { get; }
    public TrackingState TrackingState { get; private set; }
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        if (_domainEvents.All(evt => evt.EventId != domainEvent.EventId))
        {
            _domainEvents.Add(domainEvent);
        }
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        if (_domainEvents.Contains(domainEvent))
        {
            _domainEvents.Remove(domainEvent);
        }
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void SetState(TrackingState state)
    {
        if (TrackingState.CanSwitchTo(state))
        {
            TrackingState = state;
        }
    }

    protected DomainModel(TId id, TrackingState? state)
    {
        var initialState = state ?? TrackingState.Pristine;
        if (initialState != TrackingState.New && initialState != TrackingState.Pristine)
        {
            throw new DomainException(
                $"The initial state of a domain model must always be New or Pristine. The current value '{initialState.Key}' is not allowed.");
        }

        Id = id;
        TrackingState = initialState;
        _domainEvents = new List<IDomainEvent>();
    }
}